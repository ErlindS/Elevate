using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Linq;

namespace Elevate.ViewModels
{
    public partial class CombineTaskViewModel : ObservableObject
    {
        // 1. Removed [ObservableProperty] and new() - standard DI pattern
        private readonly ElevateTaskService _taskService;

        // Note: Renamed to standard camelCase for backing fields so the toolkit 
        // generates UnsortedTasks and SortedTasks with capital T's.
        [ObservableProperty]
        private ElevateTask _unsortedTasks = new();

        [ObservableProperty]
        private ElevateTask _sortedTasks = new();

        public CombineTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;

            // Use the capital properties to trigger any initial UI bindings
            UnsortedTasks = _taskService.unsortedTasks;
            SortedTasks = _taskService.sortedTasks;
            SortedTasks.Name = "Sorted Tasks";
        }

        [RelayCommand]
        private void AddItem()
        {
            // To be implemented
        }

        [RelayCommand]
        private void MoveTask(int id)
        {
            ElevateTask? task = UnsortedTasks.SubTasks?.FirstOrDefault(t => t.Id == id) as ElevateTask;

            if (task == null)
                return;

            if (SortedTasks.SubTasks == null)
            {
                // Ensure we instantiate the observable collection properly
                SortedTasks.SubTasks = new System.Collections.ObjectModel.ObservableCollection<IElevateTaskComponent>();
            }

            SortedTasks.SubTasks.Add(task);

            if (task is ElevateTask elevateTask)
            {
                // FIX: Set parent to the CURRENT SortedTasks view, not the root service
                elevateTask.ParentTask = SortedTasks;
            }

            UnsortedTasks.SubTasks.Remove(task);
        }

        [RelayCommand]
        private void Base(int id)
        {
            ElevateTask? task = SortedTasks.SubTasks?.FirstOrDefault(t => t.Id == id) as ElevateTask;

            if (task == null)
                return;

            SortedTasks = task;
        }

        [RelayCommand]
        private void Out()
        {
            if (SortedTasks.ParentTask != null)
            {
                SortedTasks = (ElevateTask)SortedTasks.ParentTask;
            }
        }

        [RelayCommand]
        private void RemoveTask(int id)
        {
            var task = SortedTasks.SubTasks?.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return;

            // Make sure the unsorted list can receive it
            if (UnsortedTasks.SubTasks == null)
            {
                UnsortedTasks.SubTasks = new System.Collections.ObjectModel.ObservableCollection<IElevateTaskComponent>();
            }

            UnsortedTasks.SubTasks.Add(task);

            // FIX: Remove from the current view, not the global root
            SortedTasks.SubTasks.Remove(task);

            // FIX: Update parent pointer back to unsorted tasks
            if (task is ElevateTask elevateTask)
            {
                elevateTask.ParentTask = UnsortedTasks;
            }
        }
    }
}