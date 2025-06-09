using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Models
{
    public interface IElevateTaskComponent
    {
        string Name { get; }
        string Description { get; }
        bool IsComposite { get; }
        bool IsDueToday { get; }
    }
}
