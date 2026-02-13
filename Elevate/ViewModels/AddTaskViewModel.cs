using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;

namespace Elevate.ViewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string newTodoText = string.Empty;

        [ObservableProperty]
        private int newTaskPriority = 1;

        [ObservableProperty]
        private string newTaskDescription = string.Empty;

        [ObservableProperty]
        private string newTaskCategory = string.Empty;

        [ObservableProperty]
        private ElevateTaskService taskService = new();

        [ObservableProperty]
        private ElevateTask tasks = new();

        public AddTaskViewModel(ElevateTaskService taskService)
        {
            TaskService = taskService;
            Tasks = taskService.unsortedTasks;
        }

        [RelayCommand]
        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(NewTodoText))
                return;

            ElevateTask newTask = new ElevateTask
            {
                Name = NewTodoText,
                Priority = NewTaskPriority,
                Description = NewTaskDescription,
                Category = NewTaskCategory,
                Id = UniqueIdGenerator.GenerateNewId()
            };

            if (Tasks.SubTasks == null)
            {
                Tasks.SubTasks = new();
            }
            Tasks.SubTasks.Add(newTask);

            // Reset form fields
            NewTodoText = string.Empty;
            NewTaskPriority = 1;
            NewTaskDescription = string.Empty;
            NewTaskCategory = string.Empty;
        }

        [RelayCommand]
        private void DeleteItem(int id)
        {
            var task = Tasks.SubTasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return;

            Tasks.SubTasks.Remove(task);
        }
    }
}
