using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Elevate.ViewModels
{
	
    public partial class CombineTaskViewModel : ObservableObject
    {
        

        [ObservableProperty]
        private ObservableCollection<BaseTaskModel> tasksToCombine = new();

        [ObservableProperty]
        private ObservableCollection<GroupTaskModel> availableProjects = new();

        [ObservableProperty]
        private GroupTaskModel selectedProject;

        [ObservableProperty]
        private GroupTaskModel selectedTask;

        public ElevateTaskService _taskService;
        public ElevateTimeService _timeService;
        public DataService _dataService;

        public CombineTaskViewModel(ElevateTaskService taskService, ElevateTimeService timeService, DataService dataService)
        {
            _taskService = taskService;
            _timeService = timeService;
            _dataService = dataService;
            TasksToCombine = new ObservableCollection<BaseTaskModel>(_taskService.GetUnassignedTasks());
            AvailableProjects = new ObservableCollection<GroupTaskModel>(_taskService.GetProjects());
            //LoadContent();
        }

        [RelayCommand]
        private void SaveContent()
        {
            _dataService.Save(_taskService, _timeService);
        }

        [RelayCommand]
        private void LoadContent()
        {
            _dataService.Load(_taskService, _timeService);
            AvailableProjects = new ObservableCollection<GroupTaskModel>(_taskService.GetProjects());
            TasksToCombine = new ObservableCollection<BaseTaskModel>(_taskService.GetUnassignedTasks());
        }

        [RelayCommand]
        private void AddTaskToSelectedProject()
        {
            if (SelectedProject == null || SelectedTask == null) return;

            SelectedProject.AddTask(SelectedTask);
            TasksToCombine.Remove(SelectedTask);
            _taskService.AddTaskToProject(SelectedTask, SelectedProject.Id);
            _taskService._unassignedTask.Remove(SelectedTask);
            //SaveContent();
            Debug.WriteLine($"{_taskService._projects[0].SubTasks[0].Name}");

        }
    }    
}