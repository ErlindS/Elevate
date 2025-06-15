using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class WeekModel : ObservableObject // Make it observable
    {
        public ObservableCollection<ElevateTaskTimeSettings> Monday { get; set; } = new ObservableCollection<ElevateTaskTimeSettings>();
        public ObservableCollection<ElevateTaskTimeSettings> Tuesday { get; set; } = new ObservableCollection<ElevateTaskTimeSettings>();
        public ObservableCollection<ElevateTaskTimeSettings> Wednesday { get; set; } = new ObservableCollection<ElevateTaskTimeSettings>();
        public ObservableCollection<ElevateTaskTimeSettings> Thursday { get; set; } = new ObservableCollection<ElevateTaskTimeSettings>();
        public ObservableCollection<ElevateTaskTimeSettings> Friday { get; set; } = new ObservableCollection<ElevateTaskTimeSettings>();
        public ObservableCollection<ElevateTaskTimeSettings> Saturday { get; set; } = new ObservableCollection<ElevateTaskTimeSettings>();
        public ObservableCollection<ElevateTaskTimeSettings> Sunday { get; set; } = new ObservableCollection<ElevateTaskTimeSettings>();
    }
}