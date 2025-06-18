using Elevate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Elevate.Services
{
    public class ElevateTaskService
    {

        public List<GroupTaskModel> _projects = new();
        public List<BaseTaskModel> _unassignedTask = new();
        public List<BaseTaskModel> _todaysTask = new();
        public List<BaseTaskModel> _tasksDone = new();

        public ElevateTaskService()
        {

        }

        public List<GroupTaskModel> GetProjects() { 
            return _projects;
        }

        public GroupTaskModel? GetProjectById(int id, List<GroupTaskModel> projects)
        {
            try
            {
                foreach (var project in projects)
                {
                    if (project.Id == id)
                        return project;
                    var result = GetProjectById(id, project.SubTasks.OfType<GroupTaskModel>().ToList());
                    if (result != null)
                        return result;
                }
                return null;
            }
            catch (Exception ex) {
                throw;
            }
        }

        public bool AddTaskToProject(BaseTaskModel task, int projectId)
        {
            var targetProject = GetProjectById(projectId, _projects);
            if (targetProject != null)
            {
                targetProject.AddTask(task);
                return true;
            }
            return false;
        }

        public List<BaseTaskModel> GetUnassignedTasks() {
            return _unassignedTask;
        }

        public List<BaseTaskModel> GetTodaysTask()
        {
            return _todaysTask;
        }

        public List<BaseTaskModel> GetTasksDone()
        {
            return _tasksDone;
        }
    }
}