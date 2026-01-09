using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Elevate.ViewModels
{
    public partial class TodaysTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> tasks;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> routine2;

        [ObservableProperty]
        private bool _isDone = false;

        [ObservableProperty]
        private DayOfWeek weekday;
        public TodaysTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            Routine2 = new ObservableCollection<IElevateTaskComponent>(); // Initialize Routine2 as a *new* collection
            UpdateTodaysTask();
        }

        [RelayCommand]
        public void UpdateTodaysTask()
        {
            Debug.WriteLine("Does button work3");

            // Clear the Routine collection to populate it with fresh data
            Routine2.Clear(); // Also clear Routine2 if you want to repopulate it

        }
    }

}
