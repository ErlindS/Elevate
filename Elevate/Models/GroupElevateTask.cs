using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevate.Models;

namespace Elevate.Models
{
    public partial class GroupElevateTask : ObservableObject, IElevateTaskComponent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDueToday { get; set; }
        public double Duration { get; set; }
        public bool IsComposite => true;
        public bool IsProject { get; set; }

        [ObservableProperty]
        private string selectedWeekdayForMapping; // For the Picker within CollectionView

        [ObservableProperty]
        private TimeSpan startingTime; // For the first TimePicker

        [ObservableProperty]
        private TimeSpan endingTime; // For the second TimePicker

        public List<ElevateTaskTimeSettings> TimeSettings { get; set; } = new List<ElevateTaskTimeSettings>();

        [ObservableProperty]
        public bool isSorted;

        public List<IElevateTaskComponent> _task = new();

        public GroupElevateTask(string name, string description, bool isproject)
        {
            Name = name;
            Description = description;
            IsProject = isproject;
        }

        public void Add(IElevateTaskComponent task)
        {
            _task.Add(task);
        }
    }
}
