using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{
    public partial class TodaysTaskViewModel : ObservableObject
    {
        private readonly ElevateTaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> todaysTasks = new();

        [ObservableProperty]
        private string todayDate = string.Empty;

        [ObservableProperty]
        private string dayName = string.Empty;

        [ObservableProperty]
        private int totalTaskCount = 0;

        [ObservableProperty]
        private int completedTaskCount = 0;

        [ObservableProperty]
        private double completionPercentage = 0;

        public TodaysTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            LoadTodaysTasks();
        }

        [RelayCommand]
        private void LoadTodaysTasks()
        {
            TodaysTasks.Clear();
            DateTime today = DateTime.Now;
            
            DayName = today.ToString("dddd");
            TodayDate = today.ToString("dd.MM.yyyy");

            var allLeafTasks = GetAllLeafTasks(_taskService.unsortedTasks);

            foreach (var task in allLeafTasks)
            {
                TodaysTasks.Add(task);
            }

            CalculateStats();
        }

        [RelayCommand]
        private void RefreshTasks()
        {
            LoadTodaysTasks();
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

        private void CalculateStats()
        {
            TotalTaskCount = TodaysTasks.Count;
            CompletedTaskCount = TodaysTasks.Count(t => t.IsDone);
            CompletionPercentage = TotalTaskCount > 0 ? (double)CompletedTaskCount / TotalTaskCount * 100 : 0;
        }
    }
}
