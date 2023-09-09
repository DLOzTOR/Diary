using Storage;

namespace Diary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StorageController storageController = new StorageController();
            Console.WriteLine(storageController.OpenDB("Test/MyDataBase3.db"));
            storageController.Add("Test text for data base!");
        }
    }
}