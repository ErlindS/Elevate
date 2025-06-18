using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class WeekModel// Make it observable
    {
        public List<WeekTimeSettingsModel> Monday { get; set; } = new List<WeekTimeSettingsModel>();
        public List<WeekTimeSettingsModel> Tuesday { get; set; } = new List<WeekTimeSettingsModel>();
        public List<WeekTimeSettingsModel> Wednesday { get; set; } = new List<WeekTimeSettingsModel>();
        public List<WeekTimeSettingsModel> Thursday { get; set; } = new List<WeekTimeSettingsModel>();
        public List<WeekTimeSettingsModel> Friday { get; set; } = new List<WeekTimeSettingsModel>();
        public List<WeekTimeSettingsModel> Saturday { get; set; } = new List<WeekTimeSettingsModel>();
        public List<WeekTimeSettingsModel> Sunday { get; set; } = new List<WeekTimeSettingsModel>();
    }
}