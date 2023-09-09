using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Storage
{
    abstract class SqliteDB
    {
        protected SqliteConnection connection;
        protected string connectionString;
        public SqliteDB (string path, out StorageCreateError createStatus) { 
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                if(directory == null)
                {
                    createStatus = StorageCreateError.CreateError;
                    return;
                }
                Directory.CreateDirectory(directory);
            }
            connectionString = $"Data Source={path};";
            connection = new SqliteConnection (connectionString) ;
            try
            {
                connection.Open();
                connection.Close();
                createStatus = StorageCreateError.None;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                createStatus = StorageCreateError.CreateError;
            }
        }
        ~SqliteDB()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
