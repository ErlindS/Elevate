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

        public List<IElevateTaskComponent> _projects = new();
        public List<IElevateTaskComponent> _todaysTask = new();

        public ElevateTaskService()
        {
            //RootElevateTaskGroup = new GroupElevateTask("My Root ElevateTask", "Top level group ElevateTask");
            _todaysTask.Add(new ElevateTask("name1", "name2"));
        }
    }
}