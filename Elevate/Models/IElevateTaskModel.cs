using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Models
{
    public interface IElevateTaskModel
    {
        string Name { get; }
        string Description { get; }
        double Duration { get; }
        int id { get; }
    }
}
