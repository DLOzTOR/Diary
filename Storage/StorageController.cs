using System.IO;

namespace Storage
{
    public class StorageController
    {
        DiaryDB db;
        bool isStorageLoad = false;
        public StorageCreateError CreateDB(string path)
        {
            if (isStorageLoad || File.Exists(path))
            {
                return StorageCreateError.StorageAllreadyExist;
            }
            else
            {
                isStorageLoad = true;
                db = new DiaryDB(path, out var createError);
                return createError;
            }
        }
        public StorageCreateError OpenDB(string path)
        {
            isStorageLoad = true;
            db = new DiaryDB(path, out var createError);
            return createError;
        }
        public bool Add(DateTime dateTime, string text)
        {
            return db.Add(dateTime, text);
        }
        public bool Add(string text)
        {
            return db.Add(DateTime.Now, text);
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