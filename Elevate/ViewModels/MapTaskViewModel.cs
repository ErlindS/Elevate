using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Layouts;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public partial class MapTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<DayOfWeek> weekdays;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> projects;
        public MapTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            Projects = new ObservableCollection<IElevateTaskComponent>(_taskService._projects);
            //InitializeComponent();
        }
    }

}
