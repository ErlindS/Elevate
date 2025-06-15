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

        // The currently selected project in the Picker
        [ObservableProperty]
        private GroupElevateTask selectedProject;

        public CombineTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;

            TasksToCombine = new ObservableCollection<IElevateTaskComponent>(_taskService._unassignedGroupTask);
            AvailableProjects = new ObservableCollection<IElevateTaskComponent>(_taskService._projects);
        }

        // Command to add a task to the selected project
        [RelayCommand]
        private void AddTaskToSelectedProject(IElevateTaskComponent taskToAdd)
        {
            if (SelectedProject != null && taskToAdd != null)
            {
                // Add the task to the selected project's sub-tasks
                SelectedProject.Add(taskToAdd);

                // If the task is an ElevateTask (simple task), mark it as sorted
                // This will trigger the DataTrigger in XAML to hide it.
                if (taskToAdd is GroupElevateTask GroupElevateTask)
                {
                    GroupElevateTask.IsSorted = true;
                }
                // If it's a GroupElevateTask being added, you might handle IsSorted differently
                // or just let it remain visible if you want to allow adding groups.

                // Optional: Provide user feedback
                Application.Current.MainPage.DisplayAlert("Success", $"'{taskToAdd.Name}' added to '{SelectedProject.Name}'.", "OK");

                // You might want to refresh the list or remove the item from TasksToCombine if you don't just hide it.
                // For this example, setting IsSorted=true makes it disappear via the DataTrigger.
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please select a project and ensure a task is available.", "OK");
            }
        }
    }    
}