using Storage;

namespace Diary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StorageController storageController = new StorageController();
            storageController.OpenDB("Test/MyDataBase3.db");
            storageController.Add("Test text for data base!");
            var data = storageController.GetAllData();
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}