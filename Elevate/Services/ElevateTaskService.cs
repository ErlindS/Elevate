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
        public ElevateTask sortedTasks = new();

        public ElevateTask unsortedTasks = new();

        public ElevateTaskService()
        {
            sortedTasks.Name = "Sorted Tasks";
            unsortedTasks.Name = "Unsorted Tasks";
        }
    }
}