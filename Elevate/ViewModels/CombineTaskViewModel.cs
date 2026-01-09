using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;

namespace Elevate.ViewModels
{

    public partial class CombineTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private ElevateTaskService _taskService = new();

        [ObservableProperty]
        private ElevateTask _unsortedtasks = new();

        [ObservableProperty]
        private ElevateTask _sortedtasks = new();

        //move should move a task
        //remove should remove a whole task
        //base should use a task as base
        //out takes the parent task as base

        public CombineTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            _unsortedtasks = taskService.unsortedTasks;
            _sortedtasks = taskService.sortedTasks;
            _sortedtasks.Name = "Sorted Tasks";
        }

        [RelayCommand]
        private void AddItem()
        {

        }

        //Moves a command
        [RelayCommand]
        private void MoveTask(int id)
        {
            ElevateTask? task = Unsortedtasks.SubTasks.FirstOrDefault(t => t.Id == id) as ElevateTask;

            if (task == null)
                return;

            if (Sortedtasks.SubTasks == null)
            {
                Sortedtasks.SubTasks = new();
            }

            Sortedtasks.SubTasks.Add(task);

            if (task is ElevateTask elevateTask)
            {
                elevateTask.ParentTask = TaskService.sortedTasks;
            }
            Unsortedtasks.SubTasks.Remove(task);
        }

        [RelayCommand]
        private void Base(int id)
        {
            ElevateTask? task = Sortedtasks.SubTasks.FirstOrDefault(t => t.Id == id) as ElevateTask;

            if (task == null)
                return;

            Sortedtasks = task;
        }

        [RelayCommand]
        private void Out()
        {
            if (Sortedtasks.ParentTask != null)
            {
                Sortedtasks = (ElevateTask)Sortedtasks.ParentTask;
            }
        }

        [RelayCommand]
        private void RemoveTask(int id)
        {
            var task = Sortedtasks.SubTasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return;
            TaskService.unsortedTasks.SubTasks.Add(task);
            TaskService.sortedTasks.SubTasks.Remove(task);
        }
    }
}