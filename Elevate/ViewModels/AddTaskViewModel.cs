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
        private ObservableCollection<IElevateTaskModel> tasks;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskModel> projects;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskModel> unassigendGroupTask;


        public AddTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            Tasks = new ObservableCollection<IElevateTaskModel>(_taskService._todaysTask);
            Projects = new ObservableCollection<IElevateTaskModel>(_taskService._projects);
            UnassigendGroupTask = new ObservableCollection<IElevateTaskModel>(_taskService._unassignedGroupTask);
        }

        [RelayCommand]
        private void AddItem()
        {
            if (IsToday) {
                var newTask = new TaskModel(_newTodoText, "placeholderdescription", TimeOnly.FromDateTime(DateTime.Now), new TimeOnly(23, 59), _newTodoHours);
                Tasks.Add(newTask);
                _taskService._todaysTask.Add(newTask);
            }
            else
            {
                if (IsProject)
                {
                    //This is a project
                    var newTask = new GroupTaskModel(NewTodoText, "placeholderdescription", IsProject);
                    newTask.IsSorted = true;
                    Projects.Add(newTask);
                    _taskService._projects.Add(newTask);
                }
                else {
                    //This is a groupTask
                    var newTask = new GroupTaskModel(NewTodoText, "placeholderdescription", IsProject);
                    UnassigendGroupTask.Add(newTask);
                    _taskService._unassignedGroupTask.Add(newTask);
                }
            }
        }

        [RelayCommand]
        private void DeleteTask(IElevateTaskModel itemToDelete) 
        {
            Projects.Remove(itemToDelete);
            if (itemToDelete is GroupTaskModel) {
                _taskService._projects.Remove((GroupTaskModel)itemToDelete);
            }
            if (itemToDelete is TaskModel)
            {
                _taskService._unassignedGroupTask.Remove((GroupTaskModel)itemToDelete);
            }
        }

    }

}
