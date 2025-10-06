/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/10
/// Summary:  This is the data access interface for tasks.
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

namespace DataAccessInterfaces
{
    public interface ITaskAccessor
    {
        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/10
        /// Summary:  Interface method for inserting a new task assignment using taskID and userID
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public bool InsertTaskAssignment(int taskID, int userID);

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  Interface method for getting a list of tasks by eventID from the database
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> SelectTasksByEventID(int eventID);

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  The data access interface for methods with volunteers assigned to events.
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/03/14
        /// What was Changed: Moved to ITaskAccessor
        /// </summary>
        public List<TaskAssignedViewModel> SelectVolunteersAndTasksByEventID(int eventID);

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the GetAllTaskTypes stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        List<TaskTypeObject> GetAllTaskTypes();

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the AddTask stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        bool AddTask(DataDomain.Task task);

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the UpdateTaskByTaskID stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        bool UpdateTaskByTaskID(DataDomain.Task task);

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor interface for the GetTaskByTaskID stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        DataDomain.Task GetTaskByTaskID(int taskID);

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/04
        /// Summary:  Interface method for getting a list of tasks by projectID from the database
        ///  Last Updated By: Josh Nicholson
        /// Last Updated: 2025/04/04
        /// What was Changed: creation
        /// </summary>
        public List<DataDomain.Task> SelectTasksByProjectID(int projectID);
        /// <summary>
        /// Creator:  Brodie Pasker
        /// Created:  2025/04/13
        /// Summary:  Interface method for getting a list of tasks assined to a User from the database
        ///  Last Updated By:
        /// Last Updated:
        /// What was Changed:
        /// </summary>
        public List<DataDomain.Task> SelectTasksByUserID(int UserID);
    }
}
