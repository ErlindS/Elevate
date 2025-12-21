using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Elevate.Models
{
    public interface IElevateTaskComponent
    {
        string Name { get; }
        int Id { get; }
        IElevateTaskComponent ParentTask { get; }
        ObservableCollection<IElevateTaskComponent> SubTasks { get; }
    }

}
