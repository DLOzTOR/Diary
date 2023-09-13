using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Storage
{
    class RecentDB : SqliteDB
    {
        static readonly string recentLocation = "AppData/Recent.db";
        public RecentDB(out StorageCreateError createStatus, out StorageOpenError openStatus) :  base(recentLocation, out createStatus, out openStatus)
        {
            string createTableQuery = "CREATE TABLE IF NOT EXIST Recent(Id INTEGER PRIMARY KEY NOT NULL, Path TEXT NOT NULL)";
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
    }
}
