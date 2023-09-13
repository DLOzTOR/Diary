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
            if (File.Exists(path))
            {
                createStatus = StorageCreateError.StorageAllreadyExist;
                return;
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
        public SqliteDB(string path, out StorageOpenError openStatus)
        {
            if (!File.Exists(path))
            {
                openStatus = StorageOpenError.StorageDoesntExist;
                return;
            }
            connectionString = $"Data Source={path};";
            connection = new SqliteConnection(connectionString);
            try
            {
                connection.Open();
                connection.Close();
                openStatus = StorageOpenError.None;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                openStatus = StorageOpenError.OpenError;
            }
        }
        public SqliteDB(string path, out StorageCreateError createStatus, out StorageOpenError openStatus)
        {
            if (File.Exists(path))
            {
                createStatus = StorageCreateError.None;
                if (!File.Exists(path))
                {
                    openStatus = StorageOpenError.StorageDoesntExist;
                    return;
                }
                connectionString = $"Data Source={path};";
                connection = new SqliteConnection(connectionString);
                try
                {
                    connection.Open();
                    connection.Close();
                    openStatus = StorageOpenError.None;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    openStatus = StorageOpenError.OpenError;
                }
            }
            else
            {
                openStatus = StorageOpenError.None;
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    if (directory == null)
                    {
                        createStatus = StorageCreateError.CreateError;
                        return;
                    }
                    Directory.CreateDirectory(directory);
                }
                if (File.Exists(path))
                {
                    createStatus = StorageCreateError.StorageAllreadyExist;
                    return;
                }
                connectionString = $"Data Source={path};";
                connection = new SqliteConnection(connectionString);
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
