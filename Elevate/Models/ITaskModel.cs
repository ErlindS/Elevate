using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Models
{
    /// <summary>
    /// Inteface for tasks
    /// </summary>
    public interface ITaskModel
    {
        string Name { get; }
        string Description { get; }
        double Duration { get; }
        int Id { get; }

        string GetName();
        string GetDescription();
        double GetDuration();
        int GetId();
    }
}
