using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

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

        [ObservableProperty]
        [JsonIgnore]
        private IElevateTaskComponent parentTask;

        [ObservableProperty]
        private List<IElevateTaskComponent> subTasks;
    }
}