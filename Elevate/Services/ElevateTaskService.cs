using Elevate.Models;
using System.Collections.ObjectModel;


namespace Elevate.Services
{
    public class ElevateTaskService
    {

        public ElevateTask root = new() { Name = "Root Task" };
        public ElevateTask sortedTasks = new();

        public ElevateTask unsortedTasks = new();

        public ElevateTaskService()
        {
            sortedTasks.Name = "Sorted Tasks";
            unsortedTasks.Name = "Unsorted Tasks";

            root.SubTasks = new ObservableCollection<IElevateTaskComponent> { sortedTasks, unsortedTasks };
        }

        public ElevateTask? GetTaskById(int id)
        {
            // Start the recursive search from the root
            return FindTask(root, id);
        }

        // Recursive helper method
        private ElevateTask? FindTask(ElevateTask currentTask, int id)
        {
            if (currentTask.Id == id)
                return currentTask;

            if (currentTask.SubTasks != null)
            {
                foreach (var subTask in currentTask.SubTasks)
                {
                    // Assuming subTask can be cast to ElevateTask
                    if (subTask is ElevateTask childTask)
                    {
                        var found = FindTask(childTask, id);
                        if (found != null)
                            return found;
                    }
                }
            }
            return null;
        }
    }
}