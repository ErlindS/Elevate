using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Layouts;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;

namespace Elevate.ViewModels
{
    public partial class MapTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;
        private ElevateTimeService _weekService;

        [ObservableProperty]
        private WeekModel mappedWeek;

        [ObservableProperty]
        private ObservableCollection<string> weekdays;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> projects;

        [ObservableProperty]
        private string selectedWeekday;

        [ObservableProperty]
        private TimeSpan selectedStartingTime;

        [ObservableProperty]
        private TimeSpan selectedEndingTime;

        public MapTaskViewModel(ElevateTaskService taskService, ElevateTimeService weekService)
        {
            _taskService = taskService;
            _weekService = weekService;
            Projects = new ObservableCollection<IElevateTaskComponent>(_taskService._projects);
            Weekdays = new ObservableCollection<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            if (_weekService.MappedWeek == null)
            {
                _weekService.MappedWeek = new WeekModel();
            }
            MappedWeek = _weekService.MappedWeek;
        }

        [RelayCommand]
        void AddtoWeek(GroupElevateTask project)
        {

            if (project.StartingTime == default || project.EndingTime == default || string.IsNullOrEmpty(project.SelectedWeekdayForMapping))
            {
                System.Diagnostics.Debug.WriteLine("Please select a weekday, start time, and end time for the project.");
                return;
            }

            var newTimeSetting = new ElevateTaskTimeSettings
            {
                Weekday = project.SelectedWeekdayForMapping,
                StartTime = TimeOnly.FromTimeSpan(project.StartingTime),
                EndTime = TimeOnly.FromTimeSpan(project.EndingTime),
                Project = project // Reference the project itself
            };

            project.TimeSettings.Add(newTimeSetting);
            System.Diagnostics.Debug.WriteLine($"To project: {project.Name} following settings were added {newTimeSetting.Weekday} {newTimeSetting.StartTime} {newTimeSetting.EndTime}");

            switch (newTimeSetting.Weekday)
            {
                case "Monday":
                    //_weekService.MappedWeek.Monday.Add(newTimeSetting);
                    MappedWeek.Monday.Add(newTimeSetting);
                    break;
                case "Tuesday":
                    //_weekService.MappedWeek.Tuesday.Add(newTimeSetting);
                    MappedWeek.Tuesday.Add(newTimeSetting);
                    break;
                case "Wednesday":
                    //_weekService.MappedWeek.Wednesday.Add(newTimeSetting);
                    MappedWeek.Wednesday.Add(newTimeSetting);
                    break;
                case "Thursday":
                    //_weekService.MappedWeek.Thursday.Add(newTimeSetting);
                    MappedWeek.Thursday.Add(newTimeSetting);
                    break;
                case "Friday":
                    //_weekService.MappedWeek.Friday.Add(newTimeSetting);
                    MappedWeek.Friday.Add(newTimeSetting);
                    break;
                case "Saturday":
                    //_weekService.MappedWeek.Saturday.Add(newTimeSetting);
                    MappedWeek.Saturday.Add(newTimeSetting);
                    break;
                case "Sunday":
                    //_weekService.MappedWeek.Sunday.Add(newTimeSetting);
                    MappedWeek.Sunday.Add(newTimeSetting);
                    break;
            }

            System.Diagnostics.Debug.WriteLine($"Added {project.Name} to {newTimeSetting.Weekday} from {newTimeSetting.StartTime} to {newTimeSetting.EndTime}");
        }
    }
}