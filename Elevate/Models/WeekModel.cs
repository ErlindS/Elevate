using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace Elevate.Models
{
    public partial class WeekModel : ObservableObject // Make it observable
    {
        List<GroupElevateTask> Monday {  get; set; }
        List<GroupElevateTask> Tuesday { get; set; }
        List<GroupElevateTask> Wednesday { get; set; }
        List<GroupElevateTask> Thursday { get; set; }
        List<GroupElevateTask> Friday { get; set; }
        List<GroupElevateTask> Saturday { get; set; }
        List<GroupElevateTask> Sonday { get; set; }
    }
}