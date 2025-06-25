using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    /// <summary>
    /// Basic TaskTime SettingsModel
    /// </summary>
    public partial class TaskTimeSettingsModel : BaseTimeSettingsModel
    {
        public DayOfWeek Weekday { get; set; }
        public TaskTimeSettingsModel(TimeOnly startTimeOnly, TimeOnly endTimeOnly, DayOfWeek weekday) { 
            StartTime = startTimeOnly;
            EndTime = endTimeOnly;
            Weekday = weekday;
        }
        public TaskTimeSettingsModel() { }
        public TaskTimeSettingsModel(TimeOnly timeOnly1, TimeOnly timeOnly2)
        {
        }
    }
}