using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Layouts;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;


namespace Elevate.ViewModels
{
    public partial class MapTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;
        private ElevateTimeService _weekService;

        public ObservableCollection<WeekTimeSettingsModel> Monday { get; set; } = new();
        public ObservableCollection<WeekTimeSettingsModel> Tuesday { get; set; } = new();
        public ObservableCollection<WeekTimeSettingsModel> Wednesday { get; set; } = new();
        public ObservableCollection<WeekTimeSettingsModel> Thursday { get; set; } = new();
        public ObservableCollection<WeekTimeSettingsModel> Friday { get; set; } = new();
        public ObservableCollection<WeekTimeSettingsModel> Saturday { get; set; } = new();
        public ObservableCollection<WeekTimeSettingsModel> Sunday { get; set; } = new();

        [ObservableProperty]
        private List<string> weekdays = new()
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };

        [ObservableProperty]
        private ObservableCollection<BaseTaskModel> projects;

        [ObservableProperty]
        private BaseTaskModel selectedProject;

        [ObservableProperty]
        private string selectedWeekday;

        [ObservableProperty]
        private TimeSpan selectedStartingTime = TimeSpan.Zero;

        [ObservableProperty]
        private TimeSpan selectedEndingTime = TimeSpan.Zero;

        [ObservableProperty]
        private WeekTimeSettingsModel displayedWeek;

        public MapTaskViewModel(ElevateTaskService taskService, ElevateTimeService weekService)
        {
            
            _taskService = taskService;
            _weekService = weekService;
            Projects = new ObservableCollection<BaseTaskModel>(_taskService._projects);
            Monday = new ObservableCollection<WeekTimeSettingsModel>(_weekService.GetWeek().Monday);
            Tuesday = new ObservableCollection<WeekTimeSettingsModel>(_weekService.GetWeek().Tuesday);
            Wednesday = new ObservableCollection<WeekTimeSettingsModel>(_weekService.GetWeek().Wednesday);
            Thursday = new ObservableCollection<WeekTimeSettingsModel>(_weekService.GetWeek().Thursday);
            Friday = new ObservableCollection<WeekTimeSettingsModel>(_weekService.GetWeek().Friday);
            Saturday = new ObservableCollection<WeekTimeSettingsModel>(_weekService.GetWeek().Saturday);
            Sunday = new ObservableCollection<WeekTimeSettingsModel>(_weekService.GetWeek().Sunday);
        }

        [RelayCommand]
        void AddtoWeek()
        {
            Debug.WriteLine("Adding task to week");

            var newTimeSetting = new WeekTimeSettingsModel(TimeOnly.FromTimeSpan(SelectedStartingTime), TimeOnly.FromTimeSpan(SelectedEndingTime), SelectedProject.Name);
            DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), SelectedWeekday);
            var newProjectTime = new TaskTimeSettingsModel(TimeOnly.FromTimeSpan(SelectedStartingTime), TimeOnly.FromTimeSpan(SelectedEndingTime), day);
            _taskService.GetProjectById(SelectedProject.Id, _taskService._projects).TimeSettings.Add(newProjectTime);

            switch (SelectedWeekday)
            {
                case "Monday":
                    Monday.Add(newTimeSetting);
                    _weekService.GetWeek().Monday.Add(newTimeSetting);
                    break;
                case "Tuesday":
                    Tuesday.Add(newTimeSetting);
                    _weekService.GetWeek().Tuesday.Add(newTimeSetting);
                    break;
                case "Wednesday":
                    Wednesday.Add(newTimeSetting);
                    _weekService.GetWeek().Wednesday.Add(newTimeSetting);
                    break;
                case "Thursday":
                    Thursday.Add(newTimeSetting);
                    _weekService.GetWeek().Thursday.Add(newTimeSetting);
                    break;
                case "Friday":
                    Friday.Add(newTimeSetting);
                    _weekService.GetWeek().Friday.Add(newTimeSetting);
                    break;
                case "Saturday":
                    Saturday.Add(newTimeSetting);
                    _weekService.GetWeek().Saturday.Add(newTimeSetting);
                    break;
                case "Sunday":
                    Sunday.Add(newTimeSetting);
                    _weekService.GetWeek().Sunday.Add(newTimeSetting);
                    break;
            }
        }
    }
}