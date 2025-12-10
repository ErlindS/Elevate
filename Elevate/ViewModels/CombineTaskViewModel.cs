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

        // Collection of tasks that can be added to a project (displayed in CollectionView)
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> tasksToCombine;

        // Collection of available projects (displayed in the Picker)
        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> availableProjects;



        public CombineTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;

            //TasksToCombine = new ObservableCollection<IElevateTaskComponent>(_taskService._unassignedGroupTask);
        }

        // Command to add a task to the selected project
        [RelayCommand]
        private void AddTaskToSelectedProject(IElevateTaskComponent taskToAdd)
        {

        }
    }    
}