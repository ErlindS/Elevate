using Elevate.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elevate.Services
{
    /// <summary>
    /// Wrapper class to save both sorted and unsorted tasks together.
    /// </summary>
    public class AppState
    {
        public ElevateTask? SortedTasks { get; set; }
        public ElevateTask? UnsortedTasks { get; set; }
    }

    public static class ElevateTaskStorage
    {
        private static readonly string DefaultFilePath =
            Path.Combine(FileSystem.AppDataDirectory, "tasks.json");

        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            MaxDepth = 10_000,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        /// <summary>
        /// Saves app state to the default file location.
        /// </summary>
        public static async Task SaveAsync(ElevateTaskService taskService)
        {
            await SaveToFileAsync(DefaultFilePath, taskService);
        }

        /// <summary>
        /// Saves app state to a custom file location.
        /// </summary>
        public static async Task SaveToFileAsync(string filePath, ElevateTaskService taskService)
        {
            var appState = new AppState
            {
                SortedTasks = taskService.sortedTasks,
                UnsortedTasks = taskService.unsortedTasks
            };

            var json = JsonSerializer.Serialize(appState, Options);
            await File.WriteAllTextAsync(filePath, json);
        }

        /// <summary>
        /// Loads app state from the default file location.
        /// </summary>
        public static async Task<bool> LoadAsync(ElevateTaskService taskService)
        {
            return await LoadFromFileAsync(DefaultFilePath, taskService);
        }

        /// <summary>
        /// Loads app state from a custom file location.
        /// </summary>
        public static async Task<bool> LoadFromFileAsync(string filePath, ElevateTaskService taskService)
        {
            if (!File.Exists(filePath))
                return false;

            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                var appState = JsonSerializer.Deserialize<AppState>(json, Options);

                if (appState == null)
                    return false;

                if (appState.SortedTasks != null)
                {
                    FixParents(appState.SortedTasks);
                    taskService.sortedTasks = appState.SortedTasks;
                }

                if (appState.UnsortedTasks != null)
                {
                    FixParents(appState.UnsortedTasks);
                    taskService.unsortedTasks = appState.UnsortedTasks;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the default file path for display purposes.
        /// </summary>
        public static string GetDefaultFilePath() => DefaultFilePath;

        private static void FixParents(ElevateTask task, ElevateTask? parent = null)
        {
            task.ParentTask = parent;

            if (task.SubTasks == null)
            {
                task.SubTasks = new ObservableCollection<IElevateTaskComponent>();
                return;
            }

            foreach (var child in task.SubTasks)
            {
                if (child is ElevateTask elevateTask)
                {
                    FixParents(elevateTask, task);
                }
            }
        }
    }
}
