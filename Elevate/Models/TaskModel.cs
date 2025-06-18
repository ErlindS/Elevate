using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;
using Elevate.Models;
using Microsoft.VisualBasic;

namespace Elevate.Models
{
    public partial class TaskModel : BaseTaskModel
    {
        private static int _idCounter = 0;
        public override int Id { get; }

        public bool IsCompleted { get; set; }
        public TaskTimeSettingsModel TimeSettings { get; set; }

        public TaskModel(string name, string description)
        {
            Id = ++_idCounter;
            Name = name;
            Description = description;
        }

        public TaskModel(string name, string description, TimeOnly startTime, TimeOnly endTime, double duration)
            : this(name, description)
        {
            TimeSettings.StartTime = startTime;
            TimeSettings.EndTime = endTime;
            Duration = duration;
        }
    }
}