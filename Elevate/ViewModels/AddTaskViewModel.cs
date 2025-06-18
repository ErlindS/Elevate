using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _newTodoText = string.Empty;

        [ObservableProperty]
        private double _newTodoHours = 0; 

        [ObservableProperty]
        private bool _isToday = false;

        [ObservableProperty]
        private bool _isProject = false;

        [ObservableProperty]
        private ObservableCollection<BaseTaskModel> todaystasks = new();

        [ObservableProperty]
        private ObservableCollection<GroupTaskModel> projects = new();

        [ObservableProperty]
        private ObservableCollection<BaseTaskModel> unassigendGroupTask = new();

        private readonly ElevateTaskService _taskService;
        private readonly ElevateTimeService _timeService;
        private DataService _dataService;
        public AddTaskViewModel(ElevateTaskService taskService, ElevateTimeService timeService, DataService dataService)
        {
            _taskService = taskService;
            _timeService = timeService;
            _dataService = dataService;
            Todaystasks = new ObservableCollection<BaseTaskModel>(_taskService.GetTodaysTask());
            Projects = new ObservableCollection<GroupTaskModel>(_taskService.GetProjects());
            UnassigendGroupTask = new ObservableCollection<BaseTaskModel>(_taskService.GetUnassignedTasks());
        }

        [RelayCommand]
        private void SaveContent() {
            _dataService.Save(_taskService, _timeService);
        }

        [RelayCommand]
        private void LoadContent()
        {
            _dataService.Load(_taskService, _timeService);
            Todaystasks = new ObservableCollection<BaseTaskModel>(_taskService.GetTodaysTask());
            Projects = new ObservableCollection<GroupTaskModel>(_taskService.GetProjects());
            UnassigendGroupTask = new ObservableCollection<BaseTaskModel>(_taskService.GetUnassignedTasks());
        }

        [RelayCommand]
        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(NewTodoText)) return;

            try
            {
                if (IsToday)
                {
                    var newTask = new TaskModel(NewTodoText, "placeholderdescription", TimeOnly.FromDateTime(DateTime.Now), new TimeOnly(23, 59), NewTodoHours);
                    Todaystasks.Add(newTask);
                    _taskService.GetTodaysTask().Add(newTask);
                }
                else
                {
                    if (IsProject)
                    {
                        var newTask = new GroupTaskModel(NewTodoText, "placeholderdescription");
                        Projects.Add(newTask);
                        _taskService.GetProjects().Add(newTask);
                    }
                    else
                    {
                        var newTask = new GroupTaskModel(NewTodoText, "placeholderdescription");
                        UnassigendGroupTask.Add(newTask);
                        _taskService.GetUnassignedTasks().Add(newTask);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [RelayCommand]
        private void DeleteTask(BaseTaskModel itemToDelete) 
        {
            try
            {
                if (Projects.Contains(itemToDelete))
                {
                    Projects.Remove((GroupTaskModel)itemToDelete);
                    _taskService.GetProjects().Remove((GroupTaskModel)itemToDelete);
                }
                if (UnassigendGroupTask.Contains(itemToDelete))
                {
                    UnassigendGroupTask.Remove(itemToDelete);
                    _taskService.GetUnassignedTasks().Remove(itemToDelete);
                }

            }
            catch (Exception ex) 
            {
                throw;
            }
        }

    }

}
