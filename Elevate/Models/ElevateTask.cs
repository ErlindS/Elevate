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

        public bool IsDueToday { get; set; }
        public bool IsComposite => false;

        public ElevateTask(string name, bool IsDueToday)
        {
            Name = name;
            IsDueToday = IsDueToday;
        }
    }
}
