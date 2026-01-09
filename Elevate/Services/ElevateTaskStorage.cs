using Elevate.Models;
using System.Text.Json;

namespace Elevate.Services
{
    public static class ElevateTaskStorage
    {
        private static readonly string FilePath =
            Path.Combine(FileSystem.AppDataDirectory, "tasks.json");

        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            MaxDepth = 10_000
        };

        public static async Task SaveAsync(ElevateTask root)
        {
            var json = JsonSerializer.Serialize(root, Options);
            await File.WriteAllTextAsync(FilePath, json);
        }

        public static async Task<ElevateTask?> LoadAsync()
        {
            if (!File.Exists(FilePath))
                return null;

            var json = await File.ReadAllTextAsync(FilePath);
            var root = JsonSerializer.Deserialize<ElevateTask>(json, Options);

            if (root != null)
                FixParents(root);

            return root;
        }

        private static void FixParents(ElevateTask task, ElevateTask parent = null)
        {
            task.ParentTask = parent;

            foreach (var child in task.SubTasks)
            {
                FixParents((ElevateTask)child, task);
            }
        }
    }
}
