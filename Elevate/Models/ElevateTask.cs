using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Models
{
    public class ElevateTask : IElevateTaskComponent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsDueToday { get; set; }
        public bool IsComposite => false;

        public ElevateTask(string name, bool IsDueToday, string t1, string t2)
        {
            Name = name;
            IsDueToday = IsDueToday;
            StartTime = t1;
            EndTime = t2;
        }
    }
}
