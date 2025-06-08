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
        public bool IsComposite => true;

        private readonly List<IElevateTaskComponent> _children = new();

        public GroupElevateTask(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Add(IElevateTaskComponent ElevateTask)
        {
            _children.Add(ElevateTask);
        }

        public IReadOnlyList<IElevateTaskComponent> GetChildren() => _children.AsReadOnly();
    }
}
