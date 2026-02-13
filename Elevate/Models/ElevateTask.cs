using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
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
        private bool isDone;

        [ObservableProperty]
        private int priority;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string category;

        [ObservableProperty]
        private DateTime scheduledDate = DateTime.Now;

        [ObservableProperty]
        [JsonIgnore]
        private IElevateTaskComponent parentTask;

        [ObservableProperty]
        private ObservableCollection<IElevateTaskComponent> subTasks = new();
    }
}