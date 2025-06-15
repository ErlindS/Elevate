using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class ElevateTaskTimeSettings
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public string Weekday { get; set; }

        public IElevateTaskComponent Project { get; set; }
        public ElevateTaskTimeSettings() { }
    }
}