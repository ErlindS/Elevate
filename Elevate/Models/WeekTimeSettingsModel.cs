using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Handlers.Items;
using System.Xml.Linq;
using Elevate.Models;

namespace Elevate.Models
{
    public class WeekTimeSettingsModel : BaseTimeSettingsModel
    {
        public string ProjectName { get; set; } = "No Projectname set";
        public WeekTimeSettingsModel(TimeOnly startTimeOnly, TimeOnly endTimeOnly, string projektname)
        {
            StartTime = startTimeOnly;
            EndTime = endTimeOnly;
            ProjectName = projektname;
        }
    }
}