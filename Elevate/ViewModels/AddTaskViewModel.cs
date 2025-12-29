using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Elevate.ViewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _newTodoText = "string.Empty"; // Holds the text for the new task entry

        [ObservableProperty]
        private ElevateTaskService _taskService = new();

        [ObservableProperty]
        private ElevateTask _tasks = new();

        public AddTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            _tasks = _taskService.unsortedTasks;
        }

        [RelayCommand]
        private void AddItem()
        {
            ElevateTask newTask = new ElevateTask
            {
                Name = _newTodoText,
                Id = UniqueIdGenerator.GenerateNewId()
            };

            if(Tasks.SubTasks == null)
            {
                Tasks.SubTasks = new();
            }
            Tasks.SubTasks.Add(newTask);
        }

        [RelayCommand]
        private void DeleteItem(int id)
        {
            var task = _tasks.SubTasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return; 

            _tasks.SubTasks.Remove(task);
        }

    }

}
