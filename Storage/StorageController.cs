using System.IO;

namespace Storage
{
    public class StorageController
    {
        DiaryDB db;
        RecentDB recentb;
        bool isStorageLoad = false;
        public StorageController()
        {
            //recentb = new RecentDB( out var cr, out var or);
            //Console.WriteLine($"{cr} + {or}");
        }
        public StorageCreateError CreateDB(string path)
        {
            if (isStorageLoad || File.Exists(path))
            {
                return StorageCreateError.StorageAllreadyExist;
            }
            else
            {
                isStorageLoad = true;
                db = new DiaryDB(path, out StorageCreateError createError);
                return createError;
            }
        }
        public StorageOpenError OpenDB(string path)
        {
            isStorageLoad = true;
            db = new DiaryDB(path, out StorageOpenError openError);
            return openError;
        }
        public bool Add(DateTime dateTime, string text)
        {
            return db.Add(dateTime, text);
        }
        public bool Add(string text)
        {
            return db.Add(DateTime.Now, text);
        }
        public List<DiaryRecord> GetAllData()
        {
            return db.GetAllData();
        }
    }

    public enum StorageCreateError
    {
        None,
        StorageAllreadyExist,
        CreateError,
        TableCreateError
    }
    public enum StorageOpenError
    {
        None,
        StorageDoesntExist,
        OpenError
    }
}