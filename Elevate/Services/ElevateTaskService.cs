using Elevate.Models;


namespace Elevate.Services
{
    public class ElevateTaskService
    {
        public ElevateTask sortedTasks = new();

        public ElevateTask unsortedTasks = new();

        public ElevateTaskService()
        {
            sortedTasks.Name = "Sorted Tasks";
            unsortedTasks.Name = "Unsorted Tasks";
        }
    }
}