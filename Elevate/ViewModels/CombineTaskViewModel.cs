using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
	
    public partial class CombineTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private ElevateTaskService _taskService = new();

        [ObservableProperty]
        private ElevateTask _unsortedtasks = new();

        [ObservableProperty]
        private ElevateTask _sortedtasks = new();

        public CombineTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            _unsortedtasks = taskService.unsortedTasks;
            _sortedtasks = taskService.sortedTasks;
        }

        [RelayCommand]
        private void AddItem()
        {
            
        }

        [RelayCommand]
        private void MoveTask(int id) 
        {
            Console.WriteLine("Hallo");
            var task = _unsortedtasks.SubTasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return;
            //_taskService.unsortedTasks.SubTasks.RemoveAll(t => t.id == id);
            _taskService.sortedTasks.SubTasks.Add(task);
        }
    }    
}