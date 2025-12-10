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
            Weekdays = new ObservableCollection<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            if (_weekService.MappedWeek == null)
            {
                _weekService.MappedWeek = new WeekModel();
            }
            MappedWeek = _weekService.MappedWeek;
        }
    }
}