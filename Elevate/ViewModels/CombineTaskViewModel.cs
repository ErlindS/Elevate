using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
	
    public partial class CombineTaskViewModel : ObservableObject
    {
        private readonly ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<BaseTaskModel> tasksToCombine = new();

        [ObservableProperty]
        private ObservableCollection<GroupTaskModel> availableProjects = new();

        [ObservableProperty]
        private GroupTaskModel selectedProject;

        [ObservableProperty]
        private GroupTaskModel selectedTask;

        public CombineTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;

            TasksToCombine = new ObservableCollection<BaseTaskModel>(_taskService.GetUnassignedTasks());
            AvailableProjects = new ObservableCollection<GroupTaskModel>(_taskService.GetProjects());
        }

        [RelayCommand]
        private void AddTaskToSelectedProject()
        {
            if (SelectedProject == null || SelectedTask == null) return;

            SelectedProject.AddTask(SelectedTask);
            TasksToCombine.Remove(SelectedTask);
            _taskService.AddTaskToProject(SelectedTask, SelectedProject.Id);
            _taskService.GetUnassignedTasks().Remove(SelectedTask);

        }
    }    
}