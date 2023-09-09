using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Storage
{
    class DiaryDB : SqliteDB
    {
        public DiaryDB(string path, out StorageCreateError createStatus) : base(path, out createStatus)
        {
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Records(Id INTEGER PRIMARY KEY NOT NULL, Date DATETIME NOT NULL, Text TEXT NOT NULL)";
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
        public bool Add(DateTime dateTime, string text)
        {
            connection.Open();
            using (SqliteCommand cmd = new SqliteCommand($"INSERT INTO Records ('Date', 'Text') VALUES ('{dateTime.ToString("yyyy-MM-ddTHH:mm:ss")}', '{text}')", connection))
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
