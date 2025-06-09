using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Elevate.Models;
using System.Collections.ObjectModel;
namespace Elevate.Services
{
    public class ElevateTaskService
    {
        public ObservableCollection<IElevateTaskComponent> Tasks { get; } = new ObservableCollection<IElevateTaskComponent>();
        public ObservableCollection<IElevateTaskComponent> Projects { get; } = new ObservableCollection<IElevateTaskComponent>();

        public GroupElevateTask RootElevateTaskGroup { get; private set; }

        public ElevateTaskService()
        {
            // Initialize or load from storage
            RootElevateTaskGroup = new GroupElevateTask("My Root ElevateTask", "Top level group ElevateTask");

            // Add example child ElevateTask
            //RootElevateTaskGroup.Add(new ElevateTask("Buy Groceries", "Milk, Eggs, Bread"));
        }

        // You can also expose methods to manipulate ElevateTasks
        public void AddElevateTaskToRoot(IElevateTaskComponent ElevateTask)
        {
            RootElevateTaskGroup.Add(ElevateTask);
        }
    }
}