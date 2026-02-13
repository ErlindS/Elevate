using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public class DayTasksGroup
    {
        public string DayName { get; set; }
        public string DayDate { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<IElevateTaskComponent> Tasks { get; set; } = new();
        public int TaskCount => Tasks.Count;
        public int CompletedCount => Tasks.Count(t => t.IsDone);
    }

    public partial class WeeklyCalendarViewModel : ObservableObject
    {
        private readonly ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<DayTasksGroup> weekDays = new();

        [ObservableProperty]
        private string weekTitle = string.Empty;

        [ObservableProperty]
        private DateTime currentWeekStart;

        public WeeklyCalendarViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            CurrentWeekStart = GetWeekStart(DateTime.Now);
            LoadWeeklyTasks();
        }

        [RelayCommand]
        private void LoadWeeklyTasks()
        {
            WeekDays.Clear();

            for (int i = 0; i < 7; i++)
            {
                DateTime date = CurrentWeekStart.AddDays(i);
                string dayName = date.ToString("dddd");
                string dayDate = date.ToString("dd.MM");

                var dayTasks = GetLeafTasksForDate(date);

                WeekDays.Add(new DayTasksGroup
                {
                    DayName = dayName,
                    DayDate = dayDate,
                    Date = date,
                    Tasks = dayTasks
                });
            }

            UpdateWeekTitle();
        }

        [RelayCommand]
        private void PreviousWeek()
        {
            CurrentWeekStart = CurrentWeekStart.AddDays(-7);
            LoadWeeklyTasks();
        }

        [RelayCommand]
        private void NextWeek()
        {
            CurrentWeekStart = CurrentWeekStart.AddDays(7);
            LoadWeeklyTasks();
        }

        [RelayCommand]
        private void GoToToday()
        {
            CurrentWeekStart = GetWeekStart(DateTime.Now);
            LoadWeeklyTasks();
        }

        private ObservableCollection<IElevateTaskComponent> GetLeafTasksForDate(DateTime date)
        {
            var leafTasks = new ObservableCollection<IElevateTaskComponent>();
            var allTasks = GetAllLeafTasks(_taskService.unsortedTasks);

            foreach (var task in allTasks)
            {
                if (task is ElevateTask elevateTask &&
                    elevateTask.ScheduledDate.Date == date.Date &&
                    elevateTask.SubTasks.Count == 0)
                {
                    leafTasks.Add(task);
                }
            }

            return leafTasks;
        }

        private List<IElevateTaskComponent> GetAllLeafTasks(IElevateTaskComponent root)
        {
            var leafTasks = new List<IElevateTaskComponent>();

            if (root == null) return leafTasks;

            if (root.SubTasks == null || root.SubTasks.Count == 0)
            {
                leafTasks.Add(root);
            }
            else
            {
                foreach (var subTask in root.SubTasks)
                {
                    leafTasks.AddRange(GetAllLeafTasks(subTask));
                }
            }

            return leafTasks;
        }

        private DateTime GetWeekStart(DateTime date)
        {
            int days = (int)date.DayOfWeek;
            return date.AddDays(-days);
        }

        private void UpdateWeekTitle()
        {
            DateTime weekEnd = CurrentWeekStart.AddDays(6);
            WeekTitle = $"{CurrentWeekStart:dd.MM.yyyy} - {weekEnd:dd.MM.yyyy}";
        }
    }
}