using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class WeekModel : ObservableObject // Make it observable
    {
        public ObservableCollection<TaskTimeSettingsModel> Monday { get; set; } = new ObservableCollection<TaskTimeSettingsModel>();
        public ObservableCollection<TaskTimeSettingsModel> Tuesday { get; set; } = new ObservableCollection<TaskTimeSettingsModel>();
        public ObservableCollection<TaskTimeSettingsModel> Wednesday { get; set; } = new ObservableCollection<TaskTimeSettingsModel>();
        public ObservableCollection<TaskTimeSettingsModel> Thursday { get; set; } = new ObservableCollection<TaskTimeSettingsModel>();
        public ObservableCollection<TaskTimeSettingsModel> Friday { get; set; } = new ObservableCollection<TaskTimeSettingsModel>();
        public ObservableCollection<TaskTimeSettingsModel> Saturday { get; set; } = new ObservableCollection<TaskTimeSettingsModel>();
        public ObservableCollection<TaskTimeSettingsModel> Sunday { get; set; } = new ObservableCollection<TaskTimeSettingsModel>();
    }
}