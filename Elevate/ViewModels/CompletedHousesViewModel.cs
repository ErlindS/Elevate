using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public partial class CompletedHousesViewModel : ObservableObject
    {
        private readonly ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> completedTasks = new();

        [ObservableProperty]
        private int totalCompletedCount = 0;

        [ObservableProperty]
        private string completionSummary = string.Empty;

        public CompletedHousesViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            LoadCompletedTasks();
        }

        [RelayCommand]
        private void LoadCompletedTasks()
        {
            CompletedTasks.Clear();
            var allLeafTasks = GetAllLeafTasks(_taskService.unsortedTasks);

            foreach (var task in allLeafTasks.Where(t => t.IsDone))
            {
                CompletedTasks.Add(task);
            }

            UpdateSummary();
        }

        [RelayCommand]
        private void RefreshHouses()
        {
            LoadCompletedTasks();
        }

        private List<IElevateTaskComponent> GetAllLeafTasks(IElevateTaskComponent root)
        {
            var leafTasks = new List<IElevateTaskComponent>();

            if (root == null) return leafTasks;

            if (root.SubTasks == null || root.SubTasks.Count == 0)
            {
                leafTasks.Add(root);
            }
            else
            {
                foreach (var subTask in root.SubTasks)
                {
                    leafTasks.AddRange(GetAllLeafTasks(subTask));
                }
            }

            return leafTasks;
        }

        private void UpdateSummary()
        {
            TotalCompletedCount = CompletedTasks.Count;
            CompletionSummary = $"Du hast {TotalCompletedCount} Aufgabe{(TotalCompletedCount != 1 ? "n" : "")} abgeschlossen!";
        }
    }
}