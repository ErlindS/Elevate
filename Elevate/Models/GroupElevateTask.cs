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
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public double Duration { get; set; }
        public bool IsComposite => true;

        public bool IsProject { get; set; }

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
