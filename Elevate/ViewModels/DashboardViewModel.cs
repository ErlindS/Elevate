using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elevate.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly ElevateTaskService _taskService;

        [ObservableProperty]
        private string jsonOutput = string.Empty;

        [ObservableProperty]
        private ElevateTask tasks = new();

        [ObservableProperty]
        private int totalTaskCount = 0;

        [ObservableProperty]
        private int completedTaskCount = 0;

        [ObservableProperty]
        private int pendingTaskCount = 0;

        [ObservableProperty]
        private double completionPercentage = 0;

        [ObservableProperty]
        private string selectedJsonIndentation = "2";

        public DashboardViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            Tasks = taskService.unsortedTasks;
            UpdateDashboard();
        }

        [RelayCommand]
        private void UpdateDashboard()
        {
            Tasks = _taskService.unsortedTasks;
            GenerateJson();
            CalculateStats();
        }

        [RelayCommand]
        private void GenerateJson()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                string json = JsonSerializer.Serialize(Tasks, options);
                JsonOutput = json;
            }
            catch (Exception ex)
            {
                JsonOutput = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        private void CalculateStats()
        {
            TotalTaskCount = CountAllTasks(Tasks);
            CompletedTaskCount = CountCompletedTasks(Tasks);
            PendingTaskCount = TotalTaskCount - CompletedTaskCount;
            CompletionPercentage = TotalTaskCount > 0 ? (double)CompletedTaskCount / TotalTaskCount * 100 : 0;
        }

        private int CountAllTasks(IElevateTaskComponent task)
        {
            if (task == null) return 0;
            int count = 1;
            if (task.SubTasks != null)
            {
                foreach (var subTask in task.SubTasks)
                {
                    count += CountAllTasks(subTask);
                }
            }
            return count;
        }

        private int CountCompletedTasks(IElevateTaskComponent task)
        {
            if (task == null) return 0;
            int count = task.IsDone ? 1 : 0;
            if (task.SubTasks != null)
            {
                foreach (var subTask in task.SubTasks)
                {
                    count += CountCompletedTasks(subTask);
                }
            }
            return count;
        }

        [RelayCommand]
        public async Task CopyJsonToClipboard()
        {
            try
            {
                await Clipboard.Default.SetTextAsync(JsonOutput);
            }
            catch
            {
                // Handle copy error
            }
        }

        [RelayCommand]
        public async Task RefreshData()
        {
            UpdateDashboard();
            await Task.Delay(300); // Small delay for visual feedback
        }
    }
}