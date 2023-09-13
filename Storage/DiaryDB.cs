using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Storage
{
    class DiaryDB : SqliteDB
    {
        public DiaryDB(string path, out StorageCreateError createStatus) : base(path, out createStatus)
        {
            string createTableQuery = "CREATE TABLE Records(Id INTEGER PRIMARY KEY NOT NULL, Date DATETIME NOT NULL, Text TEXT NOT NULL)";
            Console.WriteLine(createStatus);
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
        public DiaryDB(string path, out StorageOpenError openStatus) : base(path, out openStatus)
        {
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
        public List<DiaryRecord> GetAllData()
        {
            var data = new List<DiaryRecord>();
            string select = "SELECT * FROM Records";
            connection.Open();
            using (SqliteCommand cmd = new SqliteCommand(select, connection))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("Date"));
                        string text = reader.GetString(reader.GetOrdinal("Text"));
                        data.Add(new DiaryRecord(id, dateTime, text));
                    }
                }
            }
            return data;
        }
    }
}
