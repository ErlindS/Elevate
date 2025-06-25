// Elevate.Models/AppData.cs (as previously recommended)
using System.Collections.Generic;

namespace Elevate.Models
{
    /// <summary>
    /// Data for Json
    /// </summary>
    public class AppData
    {
        public List<GroupTaskModel>? Projects { get; set; }
        public List<BaseTaskModel>? UnassignedTasks { get; set; }
        public List<BaseTaskModel>? TodaysTasks { get; set; }
        public List<BaseTaskModel>? TasksDone { get; set; }
        public WeekModel? Week { get; set; }
    }

}