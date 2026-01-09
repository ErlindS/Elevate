using CommunityToolkit.Mvvm.ComponentModel;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public partial class MapTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;

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

        public MapTaskViewModel(ElevateTaskService taskService)
        {
        }
    }
}