using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;
using SQLite;

namespace Elevate.Models
{
    //F1F3E0
    //D2DCB6
    //A1BC98
    //778873
    public partial class ElevateTask : ObservableObject, IElevateTaskComponent // Make it observable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDueToday { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public  double Duration { get; set; }
        public bool IsComposite => true;

        public ElevateTask(string name, string description) {
            Name = name;
            Description = description;
        }

        public ElevateTask(string name, string description, TimeOnly startTime, TimeOnly endTime, double duration)
        {
            Name = name;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
        }
    }
}