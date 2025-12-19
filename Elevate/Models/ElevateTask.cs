using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace Elevate.Models
{
    //F1F3E0
    //D2DCB6
    //A1BC98
    //778873
    public partial class ElevateTask : ObservableObject, IElevateTaskComponent
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int id;

        public ObservableCollection<IElevateTaskComponent> SubTasks { get; }
            = new ObservableCollection<IElevateTaskComponent>();
    }

}