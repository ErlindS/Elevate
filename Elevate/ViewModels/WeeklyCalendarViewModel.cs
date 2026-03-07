using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public partial class TimeSlot : ObservableObject
    {
        public int Hour { get; set; }
        public string DisplayTime { get; set; }

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> day0Tasks = new();
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> day1Tasks = new();
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> day2Tasks = new();
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> day3Tasks = new();
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> day4Tasks = new();
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> day5Tasks = new();
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> day6Tasks = new();
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
            
            foreach (var slot in TimeSlots)
            {
                slot.Day0Tasks.Clear();
                slot.Day1Tasks.Clear();
                slot.Day2Tasks.Clear();
                slot.Day3Tasks.Clear();
                slot.Day4Tasks.Clear();
                slot.Day5Tasks.Clear();
                slot.Day6Tasks.Clear();
            }

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

                foreach (ElevateTask task in dayTasks)
                {
                    var slot = TimeSlots.FirstOrDefault(ts => ts.Hour == task.ScheduledDate.Hour);
                    if (slot != null)
                    {
                        if (i == 0) slot.Day0Tasks.Add(task);
                        else if (i == 1) slot.Day1Tasks.Add(task);
                        else if (i == 2) slot.Day2Tasks.Add(task);
                        else if (i == 3) slot.Day3Tasks.Add(task);
                        else if (i == 4) slot.Day4Tasks.Add(task);
                        else if (i == 5) slot.Day5Tasks.Add(task);
                        else if (i == 6) slot.Day6Tasks.Add(task);
                    }
                }
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

        [RelayCommand]
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

            // Remove from SortedProjects list if it's there (so it disappears from the bottom horizontal bar)
            if (SortedProjects.Contains(_draggedTask))
            {
                SortedProjects.Remove(_draggedTask);
                // Also move it from sortedTasks root to unsortedTasks root so the calendar can find it if needed,
                // or just leave it in sortedTasks. For now, since the calendar loads from both, it just needs the date updated.
            }

            // Füge zur neuen Position hinzu
            dayColumn.Tasks.Add(_draggedTask);

            _draggedTask = null;
            LoadWeeklyTasks();
        }

        private ObservableCollection<IElevateTaskComponent> GetLeafTasksForDate(DateTime date)
        {
            var leafTasks = new ObservableCollection<IElevateTaskComponent>();
            
            // Collect from both unsorted and sorted to find scheduled items
            var allUnsorted = GetAllLeafTasks(_taskService.unsortedTasks);
            var allSorted = GetAllLeafTasks(_taskService.sortedTasks);
            
            var allTasks = allUnsorted.Concat(allSorted).ToList();

            foreach (var task in allTasks)
            {
                if (task is ElevateTask elevateTask &&
                    elevateTask.ScheduledDate.Date == date.Date)
                {
                    // Include leaf tasks OR tasks that are directly dragged into the calendar
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