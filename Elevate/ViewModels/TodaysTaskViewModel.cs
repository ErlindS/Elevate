using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using System.Collections.ObjectModel;
using Elevate.Services;

namespace Elevate.ViewModels
{
    public partial class TodaysTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> tasks;

        [ObservableProperty]
        private bool _isDone = false;
        public TodaysTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            Tasks = new ObservableCollection<IElevateTaskComponent>(_taskService._todaysTask);
        }

    }

}
