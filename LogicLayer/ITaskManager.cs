/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/10
/// Summary:  This is the logic layer interface for tasks.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Added comments
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ITaskManager
    {
        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/10
        /// Summary:  Interface method for task assignment
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public bool TaskAssignment(int taskID, int userID);

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  Interface method for getting tasks with eventID
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> GetTasksByEventID(int eventID);

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  The manager interface for volunteers assigned to events.
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/03/14
        /// What was Changed: Moved to ITaskManager
        /// </summary>
        public List<TaskAssignedViewModel> GetVolunteersAndTasksByEventID(int eventID);

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: The manager interface for adding a task to the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddTask(DataDomain.Task task);

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: The manager interface for getting the taskTypes from the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<TaskTypeObject> GetAllTaskTypes();

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: The manager interface for updating a task by the taskID.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool UpdateTaskByTaskID(DataDomain.Task task);

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: The manager interface for getting a task by the taskID.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public DataDomain.Task GetTaskByTaskID(int taskID);

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/04
        /// Summary:  Interface method for getting tasks with projectID
        ///  Last Updated By: Josh Nicholson
        /// Last Updated: 2025/04/04
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> GetTasksByProjectID(int projectID);

        /// <summary>
        /// Creator:  Brodie Pasker
        /// Created:  2025/04/20
        /// Summary:  Interface method for getting tasks with projectID
        ///  Last Updated By:
        /// Last Updated:
        /// What was Changed:
        /// </summary>
        public List<DataDomain.Task> GetTasksByUserID(int UserID);
    }
}
