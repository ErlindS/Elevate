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