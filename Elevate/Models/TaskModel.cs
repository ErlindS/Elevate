using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Models
{
    class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
    }
}
