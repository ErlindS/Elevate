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
        private string _newTodoText = string.Empty; // Holds the text for the new task entry

        [ObservableProperty]
        private double _newTodoHours = 0; 

        [ObservableProperty]
        private bool _isToday = false;

        [ObservableProperty]
        private bool _isProject = false;

        private ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> tasks;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> projects;

        public AddTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            Tasks = new ObservableCollection<IElevateTaskComponent>(_taskService._todaysTask);
            Projects = new ObservableCollection<IElevateTaskComponent>(_taskService._projects);
        }

        [RelayCommand]
        private void AddItem()
        {
            if (_isToday) {
                var newTask = new ElevateTask(_newTodoText, "placeholderdescription", TimeOnly.FromDateTime(DateTime.Now), new TimeOnly(23, 59), _newTodoHours);
                Tasks.Add(newTask);
                _taskService._todaysTask.Add(newTask);
            }
            else
            {
                var newTask = new GroupElevateTask(_newTodoText, "placeholderdescription", _isProject);
                Projects.Add(newTask);
                _taskService._projects.Add(newTask);
            }
        }

    }

}
