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

        public ObservableCollection<ElevateTask> tasks { get; set; } = new();

        [ObservableProperty]
        private ObservableCollection<ElevateTask> tasksnewTask;

        public AddTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            LoadTasks();
        }

        private void LoadTasks()
        {
            // Populate the collection from your service
            var data = _taskService;
            foreach (var task in data.AllTasks)
            {
                tasks.Add((ElevateTask)task);
            }
        }

        [RelayCommand]
        private void AddItem()
        {
            ElevateTask newTask = new ElevateTask
            {
                Name = "test"
            };

            _taskService.AllTasks.Add(newTask);
            tasks.Add(newTask);
            Debug.WriteLine(newTask.Name);
            Console.WriteLine(newTask.Name);
        }

    }

}
