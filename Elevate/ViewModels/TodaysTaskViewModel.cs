using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using MindFusion.Scheduling.WinForms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Elevate.ViewModels
{
    public partial class TodaysTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;
        private ElevateTimeService _timeService;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> tasks;

        [ObservableProperty]
        private ObservableCollection<GroupElevateTask> projecttasks;


        [ObservableProperty]
        private ObservableCollection<GroupElevateTask> routine;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> routine2;

        [ObservableProperty]
        private bool _isDone = false;

        [ObservableProperty]
        private DayOfWeek weekday;
        public TodaysTaskViewModel(ElevateTaskService taskService, ElevateTimeService taskService1)
        {
            _timeService = taskService1;
            _taskService = taskService;
            Tasks = new ObservableCollection<IElevateTaskComponent>(_taskService._todaysTask);
            Projecttasks = new ObservableCollection<GroupElevateTask>(_taskService._projects);
            Weekday = taskService1.GetDayOfTheWeek();
            Routine = new ObservableCollection<GroupElevateTask>(); // Initialize Routine as a *new* collection
            Routine2 = new ObservableCollection<IElevateTaskComponent>(); // Initialize Routine2 as a *new* collection

            Debug.WriteLine($"TodaysTaskViewModel: Initializing. Current DayOfWeek: {Weekday}");
            Debug.WriteLine($"TodaysTaskViewModel: Number of tasks in _taskService._todaysTask: {_taskService._todaysTask.Count}");
            Debug.WriteLine($"TodaysTaskViewModel: Number of tasks in _taskService._todaysTask: {_taskService._projects.Count}");
            Debug.WriteLine($"TodaysTaskViewModel: Number of tasks in Tasks collection: {Tasks.Count}");
            UpdateTodaysTask();
        }

        [RelayCommand]
        public void UpdateTodaysTask() {
            Debug.WriteLine("Does button work3");

            // Clear the Routine collection to populate it with fresh data
            Routine.Clear();
            Routine2.Clear(); // Also clear Routine2 if you want to repopulate it

            // Filter tasks based on the current Weekday
            // It's safer to iterate over a copy or use LINQ if you're modifying the collection you're iterating over,
            // but in this case, Projecttasks isn't being modified, only read from.
            foreach (var task in Projecttasks)
            {
                Debug.WriteLine("Does button work2");
                foreach (var time in task.TimeSettings)
                {
                    // Ensure consistency: If Weekday is a DayOfWeek enum, time.Weekday should be compared as such.
                    // If time.Weekday is a string, make sure it matches the string representation of Weekday.
                    if (time.Weekday == Weekday.ToString())
                    {
                        Debug.WriteLine("do we reach the Routine point");
                        Routine.Add(task);
                        // Once a task is added to Routine, you might not need to check other time settings for it.
                        // Consider breaking here if a task should only be added once based on any matching time setting.
                        break;
                    }
                }
            }

            foreach (var task in Routine)
            {
                // Iterate through a copy of _task if it might be modified during iteration, though unlikely here.
                foreach (var subTask in task._task.ToList()) // Using .ToList() to iterate over a copy
                {
                    Debug.WriteLine("do we reach the Routine2 point");
                    Routine2.Add(subTask); // Add each individual sub-task
                }
            }

        }
    }

}
