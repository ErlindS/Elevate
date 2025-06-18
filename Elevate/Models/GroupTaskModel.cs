using CommunityToolkit.Mvvm.ComponentModel;
using Elevate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elevate.Models
{
    /// <summary>
    /// Represents a group task model that can contain multiple tasks and associated settings.
    /// </summary>
    /// <remarks>This class is used to manage a collection of tasks as a group, with properties for task
    /// metadata such as name,  description, duration, priority, and project status. It provides methods to add, remove,
    /// and modify tasks within the group.</remarks>
    public partial class GroupTaskModel : BaseTaskModel
    {
        private static int _idCounter = 0;
        public override int Id { get; }

        public int Priority { get; set; }
        public List<TaskTimeSettingsModel> TimeSettings { get; set; } = new();
        public List<BaseTaskModel> SubTasks { get; } = new();

        public GroupTaskModel(string name, string description)
        {
            Id = ++_idCounter;
            Name = name;
            Description = description;
        }

        public void AddTask(BaseTaskModel task) => SubTasks.Add(task);
        public void RemoveTask(BaseTaskModel task) => SubTasks.Remove(task);
        public void ChangeName(string newName) => Name = newName;
        public void ChangeDescription(string newDescription) => Description = newDescription;

        public string GetFirstTaskName() {
            return SubTasks?.FirstOrDefault()?.Name ?? "No tasks";
        }

        public BaseTaskModel? this[string name] {
            get => SubTasks.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerable<BaseTaskModel> GetIncompleteTasks() {
            foreach (var task in SubTasks) { 
                if(task is TaskModel taskModel && !taskModel.IsCompleted)
                    yield return taskModel;
            }
        }


    }
}
