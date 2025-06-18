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
        private ObservableCollection<ITaskModel> tasks;

        [ObservableProperty]
        private ObservableCollection<GroupTaskModel> projecttasks;


        [ObservableProperty]
        private ObservableCollection<GroupTaskModel> routine;

        [ObservableProperty]
        private ObservableCollection<ITaskModel> routine2;

        [ObservableProperty]
        private bool _isDone = false;

        [ObservableProperty]
        private DayOfWeek weekday;
        public TodaysTaskViewModel(ElevateTaskService taskService, ElevateTimeService taskService1)
        {
            _timeService = taskService1;
            _taskService = taskService;
            Tasks = new ObservableCollection<ITaskModel>(_taskService._todaysTask);
            Projecttasks = new ObservableCollection<GroupTaskModel>(_taskService._projects);
            Weekday = taskService1.GetDayOfTheWeek();
            Routine = new ObservableCollection<GroupTaskModel>(); 
            Routine2 = new ObservableCollection<ITaskModel>(); 

            Debug.WriteLine($"TodaysTaskViewModel: Initializing. Current DayOfWeek: {Weekday}");
            Debug.WriteLine($"TodaysTaskViewModel: Number of tasks in _taskService._todaysTask: {_taskService._todaysTask.Count}");
            Debug.WriteLine($"TodaysTaskViewModel: Number of tasks in _taskService._todaysTask: {_taskService._projects.Count}");
            Debug.WriteLine($"TodaysTaskViewModel: Number of tasks in Tasks collection: {Tasks.Count}");
            UpdateTodaysTask();
        }

        [RelayCommand]
        public void UpdateTodaysTask() {
            
            Debug.WriteLine("Does button work3");

            Routine.Clear();
            Routine2.Clear(); 

            foreach (var task in Projecttasks)
            {
                Debug.WriteLine("Does button work2");
                foreach (var time in task.TimeSettings)
                {
                    if (time.Weekday == Weekday)
                    {
                        Debug.WriteLine("do we reach the Routine point");
                        Routine.Add(task);
                        break;
                    }
                }
            }

            foreach (var task in Routine)
            {
                foreach (var subTask in task.SubTasks.ToList())
                {
                    Debug.WriteLine("do we reach the Routine2 point");
                    Routine2.Add(subTask);
                }
            }

        }
    }

}
