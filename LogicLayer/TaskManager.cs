/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/10
/// Summary:  Manager for task methods
///  Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Added comments
/// </summary>

using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class TaskManager : ITaskManager
    {
        ITaskAccessor _taskAccessor;

        public TaskManager()
        {
            _taskAccessor = new TaskAccessor();
        }

        public TaskManager(ITaskAccessor taskAccessor)
        {
            _taskAccessor = taskAccessor;
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/10
        /// Summary:  Gets a list of tasks by eventID
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> GetTasksByEventID(int eventID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();
            try
            {
                tasks = _taskAccessor.SelectTasksByEventID(eventID);
            }
            catch (Exception)
            {
                throw;
            }
            return tasks;
        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/10
        /// Summary:  Sends a new task assignment using taskID and userID, 
        ///           then returns a bool if the assignment was successful.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public bool TaskAssignment(int taskID, int userID)
        {
            bool result = false;

            try
            {
                result = _taskAccessor.InsertTaskAssignment(taskID, userID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/18
        /// Summary:  This method gets the tasks and the volunteers assigned to the event.
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/03/14
        /// What was Changed: Moved to TaskManager
        /// </summary>
        public List<TaskAssignedViewModel> GetVolunteersAndTasksByEventID(int eventID)
        {
            List<TaskAssignedViewModel> tasksAssigned = new List<TaskAssignedViewModel>();

            try
            {
                tasksAssigned = _taskAccessor.SelectVolunteersAndTasksByEventID(eventID);
            }
            catch (Exception)
            {

                throw;
            }

            return tasksAssigned;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: This method adds a task to the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddTask(DataDomain.Task task)
        {
            try
            {
                _taskAccessor.AddTask(task);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return true;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: This method gets all the taskTypes from the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<TaskTypeObject> GetAllTaskTypes()
        {
            try
            {
                return _taskAccessor.GetAllTaskTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: This method updates a task by the task.TaskID given.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool UpdateTaskByTaskID(DataDomain.Task task)
        {
            try
            {
                _taskAccessor.UpdateTaskByTaskID(task);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return true;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: This method gets a task by the taskID from the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public DataDomain.Task GetTaskByTaskID(int taskID)
        {
            try
            {
                return _taskAccessor.GetTaskByTaskID(taskID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }
        }

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/04
        /// Summary:  Gets a list of tasks by projectID
        ///  Last Updated By: Josh Nicholson
        /// Last Updated: 2025/04/04
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> GetTasksByProjectID(int projectID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();
            try
            {
                tasks = _taskAccessor.SelectTasksByProjectID(projectID);
            }
            catch (Exception)
            {
                throw;
            }
            return tasks;
        }

        /// <summary>
        /// Creator:  Brodie Pasker
        /// Created:  2025/04/18
        /// Summary:  Gets a list of tasks assigned to a UserID
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> GetTasksByUserID(int UserID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();
            try
            {
                tasks = _taskAccessor.SelectTasksByUserID(UserID);
            }
            catch (Exception)
            {
                throw;
            }
            return tasks;
        }
    }
}
