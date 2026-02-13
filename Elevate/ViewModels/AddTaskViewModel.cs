using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

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
        private string selectedDay = "Monday";

        [ObservableProperty]
        private ObservableCollection<string> availableDays = new();

        [ObservableProperty]
        private ElevateTaskService taskService = new();

        [ObservableProperty]
        private ElevateTask tasks = new();

        public AddTaskViewModel(ElevateTaskService taskService)
        {
            TaskService = taskService;
            Tasks = taskService.unsortedTasks;
            InitializeDays();
        }

        private void InitializeDays()
        {
            AvailableDays.Clear();
            AvailableDays.Add("Monday");
            AvailableDays.Add("Tuesday");
            AvailableDays.Add("Wednesday");
            AvailableDays.Add("Thursday");
            AvailableDays.Add("Friday");
            AvailableDays.Add("Saturday");
            AvailableDays.Add("Sunday");
        }

        [RelayCommand]
        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(NewTodoText))
                return;

            DateTime scheduledDate = GetDateForDay(SelectedDay);

            ElevateTask newTask = new ElevateTask
            {
                Name = NewTodoText,
                Priority = NewTaskPriority,
                Description = NewTaskDescription,
                Category = NewTaskCategory,
                ScheduledDate = scheduledDate,
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
            SelectedDay = "Monday";
        }

        [RelayCommand]
        private void DeleteItem(int id)
        {
            var task = Tasks.SubTasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return;

            Tasks.SubTasks.Remove(task);
        }

        private DateTime GetDateForDay(string dayName)
        {
            DateTime today = DateTime.Now;
            DayOfWeek targetDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayName);

            int daysAhead = (int)targetDay - (int)today.DayOfWeek;
            if (daysAhead <= 0)
            {
                daysAhead += 7;
            }

            return today.AddDays(daysAhead);
        }
    }
}
