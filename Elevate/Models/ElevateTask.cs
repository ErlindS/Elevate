using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;
using SQLite;

namespace Elevate.Models
{
    //F1F3E0
    //D2DCB6
    //A1BC98
    //778873
    public partial class ElevateTask : ObservableObject, IElevateTaskComponent // Make it observable
    {

        public string Name { get; set; }
    }
}