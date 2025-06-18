using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class TaskTimeSettingsModel : BaseTimeSettingsModel
    {
        public DayOfWeek Weekday { get; set; }
        public TaskTimeSettingsModel(TimeOnly startTimeOnly, TimeOnly endTimeOnly, DayOfWeek weekday) { 
            StartTime = startTimeOnly;
            EndTime = endTimeOnly;
            Weekday = weekday;
        }
    }
}