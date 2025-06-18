using Elevate.Models;
using MindFusion.Json; 
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata; 

namespace Elevate.Services
{
    public class DataService
    {
        private static string FilePath = "data.json";

        public void Save(ElevateTaskService taskService, ElevateTimeService timeService)
        {
            var dataToSave = new AppData
            {
                Projects = taskService._projects,
                UnassignedTasks = taskService._unassignedTask,
                TodaysTasks = taskService._todaysTask,
                TasksDone = taskService._tasksDone,
                Week = timeService.GetWeek()
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            };

            var json = JsonSerializer.Serialize(dataToSave, options);
            File.WriteAllText(FilePath, json);

            System.Diagnostics.Debug.WriteLine($"Saved data to: {Path.GetFullPath(FilePath)}");
        }

        public void Load(ElevateTaskService taskService, ElevateTimeService timeService)
        {
            if (!File.Exists(FilePath))
            {
                System.Diagnostics.Debug.WriteLine($"Load: No data file found at {Path.GetFullPath(FilePath)}");
                return;
            }
                string json = File.ReadAllText(FilePath);
                System.Diagnostics.Debug.WriteLine($"Load: Read JSON content ({json.Length} chars)");

                var loadedData = JsonSerializer.Deserialize<AppData>(json);

                if (loadedData == null)
                {
                    System.Diagnostics.Debug.WriteLine("Load: Deserialized data is null.");
                    return;
                }

                // Replace lists in taskService
                taskService._projects = loadedData.Projects ?? new List<GroupTaskModel>();
                taskService._unassignedTask = loadedData.UnassignedTasks ?? new List<BaseTaskModel>();
                taskService._todaysTask = loadedData.TodaysTasks ?? new List<BaseTaskModel>();
                taskService._tasksDone = loadedData.TasksDone ?? new List<BaseTaskModel>();

                if (loadedData.Week != null)
                {
                    var currentWeek = timeService.GetWeek();

                    currentWeek.Monday = loadedData.Week.Monday ?? new List<WeekTimeSettingsModel>();
                    currentWeek.Tuesday = loadedData.Week.Tuesday ?? new List<WeekTimeSettingsModel>();
                    currentWeek.Wednesday = loadedData.Week.Wednesday ?? new List<WeekTimeSettingsModel>();
                    currentWeek.Thursday = loadedData.Week.Thursday ?? new List<WeekTimeSettingsModel>();
                    currentWeek.Friday = loadedData.Week.Friday ?? new List<WeekTimeSettingsModel>();
                    currentWeek.Saturday = loadedData.Week.Saturday ?? new List<WeekTimeSettingsModel>();
                    currentWeek.Sunday = loadedData.Week.Sunday ?? new List<WeekTimeSettingsModel>();
                }
            }
    }
}