using Elevate.Models;
using LiteDB;

namespace Elevate.Services
{
    public class LiteDbService
    {
        private readonly string _dbPath = "Database.db3";
        //C:\Users\erlin\source\repos\Elevate\Elevate\Database.db3

        public void SaveTaskTree(ElevateTask rootTask)
        {
            using var db = new LiteDatabase(_dbPath);
            var col = db.GetCollection<ElevateTask>("tasks");

            // Upsert: Updates if Id exists, Inserts if it doesn't
            col.Upsert(rootTask);
        }

        public List<ElevateTask> GetAllRootTasks()
        {
            using var db = new LiteDatabase(_dbPath);
            var col = db.GetCollection<ElevateTask>("tasks");

            // You can use LINQ queries!
            return col.FindAll().ToList();
        }

        public void DeleteTask(int id)
        {
            using var db = new LiteDatabase(_dbPath);
            var col = db.GetCollection<ElevateTask>("tasks");
            col.Delete(id);
        }
    }
}
