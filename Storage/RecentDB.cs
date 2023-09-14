using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Storage
{
    class RecentDB : SqliteDB
    {
        static readonly string recentLocation = "AppData/Recent.db";
        public RecentDB(out StorageCreateError createStatus, out StorageOpenError openStatus) :  base(recentLocation, out createStatus, out openStatus)
        {
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Recent(Id INTEGER PRIMARY KEY NOT NULL, Path TEXT NOT NULL)";
            using (SqliteCommand command = new SqliteCommand(createTableQuery, connection))
            {
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.Message);
#endif
                }
            }
        }
        public bool Add(string path)
        {
            connection.Open();
            using (SqliteCommand cmd = new SqliteCommand($"SELECT COUNT(*) FROM Recent WHERE Path = '{path}'", connection))
            {
                long result = (long)cmd.ExecuteScalar();
                if (result > 0)
                {
                    using (SqliteCommand delete = new SqliteCommand($"DELETE FROM Recent WHERE Path = @Path", connection))
                    {
                        delete.Parameters.AddWithValue("@Path", path);
                        int deleteResult = delete.ExecuteNonQuery();
                        if (deleteResult == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            using (SqliteCommand cmd = new SqliteCommand($"INSERT INTO Recent ('Path') VALUES ('{path}')", connection))
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }
    }
}
