using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;
using Elevate.Models;
using Microsoft.VisualBasic;

namespace Elevate.Models
{
    /// <summary>
    /// Basic Model for Daily tasks
    /// </summary>
    public partial class TaskModel : BaseTaskModel
    {
        private static int _idCounter = 0;
        public override int Id { get; }

        public bool IsCompleted { get; set; }
        public TaskTimeSettingsModel TimeSettings { get; set; } = new(TimeOnly.FromDateTime(DateTime.Now), new TimeOnly(23, 59));

        public TaskModel(string name, string description)
        {
            Id = ++_idCounter;
            Name = name;
            Description = description;
        }
        public TaskModel() { }

        public TaskModel(string name, string description, TimeOnly? startTime, TimeOnly? endTime, double duration)
            : this(name, description)
        {
            TimeSettings.StartTime =  TimeOnly.FromDateTime(DateTime.Now);
            TimeSettings.EndTime = new TimeOnly(23, 59);

            Duration = duration;
        }
    }
}