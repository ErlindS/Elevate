using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class ElevateTask : ObservableObject, IElevateTaskComponent // Make it observable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDueToday { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsComposite => true;

        public ElevateTask(string name) {
            Name = name;
        }

        //public ElevateTask(string name, bool isDone, string startTime, string estimatedHours)
        //{
        //    Name = name;
        //    IsDone = isDone;
        //    StartTime = startTime;
        //    EstimatedHours = estimatedHours;
        //}
    }
}