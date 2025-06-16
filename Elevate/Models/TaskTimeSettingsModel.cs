using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class TaskTimeSettingsModel
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Weekday { get; set; }
        public TaskTimeSettingsModel() { }
    }
}