using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
	
    public partial class CombineTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> projects;

        public CombineTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            Projects = new ObservableCollection<IElevateTaskComponent>(_taskService._projects);
        }
    }
}