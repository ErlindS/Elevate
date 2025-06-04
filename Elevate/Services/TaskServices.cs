using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevate.Models;

namespace Elevate.Services
{
    class TaskServices
    {
        public List<TaskModel> GetAllTasks() 
        {
            return new List<TaskModel> {
                new TaskModel { TaskId = 1, TaskName = "Einkaufen", Status = Models.TaskStatus.New},
                new TaskModel { TaskId = 2, TaskName = "Einkaufen", Status = Models.TaskStatus.InProgress},
                new TaskModel { TaskId = 3, TaskName = "Einkaufen", Status = Models.TaskStatus.Done},
            };
        }
    }
}
