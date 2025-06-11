using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Elevate.Models;
namespace Elevate.Services
{
    public class ElevateTaskService
    {
        public GroupElevateTask RootElevateTaskGroup { get; private set; }

        public ElevateTaskService()
        {
            RootElevateTaskGroup = new GroupElevateTask("My Root ElevateTask", "Top level group ElevateTask");
            RootElevateTaskGroup.Add(new ElevateTask("name1"));
        }

        public void AddElevateTaskToRoot(IElevateTaskComponent ElevateTask)
        {
            RootElevateTaskGroup.Add(ElevateTask);
        }
    }
}