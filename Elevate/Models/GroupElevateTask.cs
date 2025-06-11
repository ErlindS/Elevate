using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Models
{
    public class GroupElevateTask : IElevateTaskComponent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDueToday { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsComposite => true;

        public GroupElevateTask(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
