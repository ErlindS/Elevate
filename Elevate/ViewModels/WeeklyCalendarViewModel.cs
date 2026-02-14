using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public class TimeSlot
    {
        public int Hour { get; set; }
        public string DisplayTime { get; set; }
    }

    public class DayColumn
    {
        public string DayName { get; set; }
        public string DayDate { get; set; }
        public DateTime Date { get; set; }
        public List<IElevateTaskComponent> Tasks { get; set; } = new();
    }

    public class DropTaskParameter
    {
        public DayColumn DayColumn { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }

    public partial class WeeklyCalendarViewModel : ObservableObject
    {
        private readonly ElevateTaskService _taskService;
        private IElevateTaskComponent _draggedTask;

        [ObservableProperty]
        private ObservableCollection<DayColumn> weekDays = new();

        [ObservableProperty]
        private ObservableCollection<TimeSlot> timeSlots = new();

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> sortedProjects = new();

        [ObservableProperty]
        private string weekTitle = string.Empty;

        [ObservableProperty]
        private DateTime currentWeekStart;

        public WeeklyCalendarViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            CurrentWeekStart = GetWeekStart(DateTime.Now);
            InitializeTimeSlots();
            LoadWeeklyTasks();
            LoadSortedProjects();
        }

        private void InitializeTimeSlots()
        {
            TimeSlots.Clear();
            for (int hour = 6; hour <= 24; hour++)
            {
                string displayTime = hour < 24 ? $"{hour:D2}:00" : "00:00";
                TimeSlots.Add(new TimeSlot
                {
                    Hour = hour,
                    DisplayTime = displayTime
                });
            }
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

                WeekDays.Add(new DayColumn
                {
                    DayName = dayName,
                    DayDate = dayDate,
                    Date = date,
                    Tasks = dayTasks.ToList()
                });
            }

            UpdateWeekTitle();
        }

        private void LoadSortedProjects()
        {
            SortedProjects.Clear();
            if (_taskService.sortedTasks?.SubTasks != null)
            {
                foreach (var project in _taskService.sortedTasks.SubTasks)
                {
                    SortedProjects.Add(project);
                }
            }
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

        public void StartDrag(IElevateTaskComponent task)
        {
            _draggedTask = task;
        }

        [RelayCommand]
        private void DropTaskOnTimeSlot(DropTaskParameter parameter)
        {
            if (_draggedTask == null || parameter?.DayColumn == null || parameter?.TimeSlot == null)
                return;

            var dayColumn = parameter.DayColumn;
            var timeSlot = parameter.TimeSlot;

            // Entferne aus alter Position
            var sourceDay = WeekDays.FirstOrDefault(d => d.Tasks.Contains(_draggedTask));
            if (sourceDay != null)
            {
                sourceDay.Tasks.Remove(_draggedTask);
            }

            // Aktualisiere Datum und Zeit
            if (_draggedTask is ElevateTask elevateTask)
            {
                DateTime newDateTime = dayColumn.Date.Date.AddHours(timeSlot.Hour);
                elevateTask.ScheduledDate = newDateTime;
            }

            // Füge zur neuen Position hinzu
            dayColumn.Tasks.Add(_draggedTask);

            _draggedTask = null;
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