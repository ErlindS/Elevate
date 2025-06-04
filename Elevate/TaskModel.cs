using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate
{
    public class TaskModel
    {
        public string TaskName { get; set; }
        public int TaskStatus { get; set; }
        public int TaskId { get; set; }
        public Models.TaskStatus Status { get; internal set; }
        public object Id { get; internal set; }
        public object Name { get; internal set; }
    }

    public enum TaskStatusOption
    {
        NewTask,
        InProgress,
        InReview,
        Done
    }
}
