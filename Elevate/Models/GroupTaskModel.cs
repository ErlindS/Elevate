using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevate.Models;

namespace Elevate.Models
{
    public partial class GroupTaskModel : ObservableObject, IElevateTaskModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Duration { get; set; }
        public bool IsProject { get; set; }

        [ObservableProperty]
        private string selectedWeekdayForMapping; // For the Picker within CollectionView

        [ObservableProperty]
        private TimeSpan startingTime;

        [ObservableProperty]
        private TimeSpan endingTime;

        public List<TaskTimeSettingsModel> TimeSettings { get; set; } = new List<TaskTimeSettingsModel>();

        [ObservableProperty]
        public bool isSorted;

        public List<IElevateTaskModel> _task = new();

        public GroupTaskModel(string name, string description, bool isproject)
        {
            Name = name;
            Description = description;
            IsProject = isproject;
        }

        public void Add(IElevateTaskModel task)
        {
            _task.Add(task);
        }
    }
}
