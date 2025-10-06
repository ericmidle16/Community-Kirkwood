/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/10
/// Summary:  Fake data needed for Task Methods
/// Last Updated By: Josh Nicholson
/// Last Updated: 2025/03/27
/// What was Changed: Compiled additional task tests 
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class TaskAccessorFakes : ITaskAccessor
    {
        List<TaskAssignedViewModel> _tasksAssigned;
        List<DataDomain.Task> _tasks;
        List<TaskTypeObject> _tasktypes;
        List<User> _users;

        public TaskAccessorFakes()
        {
            _tasks = new List<DataDomain.Task>();
            _tasktypes = new List<TaskTypeObject>();
            _tasksAssigned = new List<TaskAssignedViewModel>();
            _tasksAssigned.Add(new TaskAssignedViewModel()
            {
                GivenName = "Harold",
                FamilyName = "Burlington",
                State = "Nebraska",
                City = "Omaha",
                UserID = 1,
                TaskID = 1,
                TaskName = "Sell Goods",
                TaskDescription = "Sell the goods to the customers.",
                EventID = 1
            });
            _tasksAssigned.Add(new TaskAssignedViewModel()
            {
                GivenName = "Jon",
                FamilyName = "Debois",
                State = "Nebraska",
                City = "Omaha",
                UserID = 2,
                TaskID = 1,
                TaskName = "Sell Goods",
                TaskDescription = "Sell the goods to the customers.",
                EventID = 2
            });
            _tasksAssigned.Add(new TaskAssignedViewModel()
            {
                GivenName = "Henry",
                FamilyName = "Francis",
                State = "Nebraska",
                City = "Omaha",
                UserID = 3,
                TaskID = 2,
                TaskName = "Clean",
                TaskDescription = "Clean the stalls.",
                EventID = 1
            });
            _tasks.Add(new DataDomain.Task()
            {
                TaskID = 1,
                Name = "pick up trash",
                Description = "pick up the litter around",
                ProjectID = 1,
                TaskType = "cleaning",
                EventID = 1,
                Active = true
            });
            _tasks.Add(new DataDomain.Task()
            {
                TaskID = 2,
                Name = "bake sale",
                Description = "sell baked goods",
                ProjectID = 1,
                TaskType = "sales",
                EventID = 2,
                Active = true
            });
            _tasks.Add(new DataDomain.Task()
            {
                TaskID = 3,
                Name = "bake sale",
                Description = "sell baked goods",
                ProjectID = 2,
                TaskType = "sales",
                EventID = 2,
                Active = false
            });

            _users = new List<User>();
            _users.Add(new User()
            {
                UserID = 1,
                GivenName = "Bob",
                FamilyName = "Benson",
                PhoneNumber = "123456789",
                Email = "bob@email.com",
                City = "Cedar Rapids",
                State = "Iowa",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 2,
                GivenName = "Joe",
                FamilyName = "Johnson",
                PhoneNumber = "123456780",
                Email = "joe@email.com",
                City = "Cedar Rapids",
                State = "Iowa",
                Active = true
            });
            _users.Add(new User()
            {
                UserID = 3,
                GivenName = "Jill",
                FamilyName = "Johnson",
                PhoneNumber = "123456781",
                Email = "jill@email.com",
                City = "Cedar Rapids",
                State = "Iowa",
                Active = false
            });

            // Data for JN tests
            _tasks.Add(new DataDomain.Task()
            {
                TaskID = 100000,
                Name = "Task Name",
                Description = "Task Description",
                TaskDate = new DateTime(2025, 2, 2),
                ProjectID = 100000,
                TaskType = "Task Type",
                EventID = 100000,
                Active = true
            });
            _tasks.Add(new DataDomain.Task()
            {
                TaskID = 100001,
                Name = "Task Name 2",
                Description = "Task Description 2",
                TaskDate = new DateTime(2025, 2, 2),
                ProjectID = 100000,
                TaskType = "Task Type 2",
                EventID = 100000,
                Active = true
            });
            _tasktypes.Add(new TaskTypeObject()
            {
                TaskType = "Task Type",
                Description = "This is a task type"
            });
            _tasktypes.Add(new TaskTypeObject()
            {
                TaskType = "Task Type 2",
                Description = "This is a task type"
            });
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/10
        /// Summary:  A method that simulates the validation of inserting a task assignment in the database.
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public bool InsertTaskAssignment(int taskID, int userID)
        {
            bool result = false;

            // check to make sure the user is real
            foreach (User user in _users)
            {
                if (user.UserID == userID && user.Active)
                {
                    result = true;
                    break;
                }
            }
            if (result)
            {
                result = false;
                foreach (DataDomain.Task task in _tasks)
                {
                    if (task.TaskID == taskID && task.Active)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/10
        /// Summary:  Gets a list of tasks by eventID from the fake data
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> SelectTasksByEventID(int eventID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();
            foreach (DataDomain.Task task in _tasks)
            {
                if (task.EventID == eventID && task.Active)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/17
        /// Summary:  The fake data for volunteers assigned to events.
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/03/14
        /// What was Changed: Moved to TaskAccessorFake
        /// </summary>
        public List<TaskAssignedViewModel> SelectVolunteersAndTasksByEventID(int eventID)
        {
            List<TaskAssignedViewModel> taskAssignedViewModel = new List<TaskAssignedViewModel>();

            foreach (var task in _tasksAssigned)
            {
                if (task.EventID == eventID)
                {
                    taskAssignedViewModel.Add(task);
                }
            }

            return taskAssignedViewModel;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The test for GetAllTaskTypes()
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<TaskTypeObject> GetAllTaskTypes()
        {
            return _tasktypes;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The test for AddTask()
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddTask(DataDomain.Task newTask)
        {
            int oldCount = _tasks.Count;

            // lambda expression to check if a task with Name already exists
            DataDomain.Task task = _tasks.Find(p => p.Name == newTask.Name);

            if (task == null)
            {
                _tasks.Add(newTask);

                if (_tasks.Count != oldCount)
                {
                    return true;
                }
            }

            throw new Exception("Test Failed");
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The test for UpdateTaskByTaskID()
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool UpdateTaskByTaskID(DataDomain.Task newTask)
        {
            bool isNameTaken = _tasks.Any(p => p.Name == newTask.Name && p.TaskID != newTask.TaskID);

            if (isNameTaken)
            {
                return false;
            }

            DataDomain.Task task = _tasks.Find(p => p.TaskID == newTask.TaskID);
            if (task != null)
            {
                task.Name = newTask.Name;
                task.Description = newTask.Description;
                task.ProjectID = newTask.ProjectID;
                task.TaskType = newTask.TaskType;
                task.EventID = newTask.EventID;
                task.Active = newTask.Active;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The test for GetTaskByTaskID()
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public DataDomain.Task GetTaskByTaskID(int taskID)
        {
            DataDomain.Task task = _tasks.Find(p => p.TaskID == taskID);
            if (task != null)
            {
                return task;
            }

            throw new Exception("Test Failed");
        }

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/04
        /// Summary:  Gets a list of tasks by projectID from the fake data
        ///  Last Updated By: Josh Nicholson
        /// Last Updated: 2025/04/04
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> SelectTasksByProjectID(int projectID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();
            foreach (DataDomain.Task task in _tasks)
            {
                if (task.ProjectID == projectID && task.Active)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        /// <summary>
        /// Creator:  Brodie Pasker
        /// Created:  2025/04/13
        /// Summary:  Datafake method to get the tasks assigned to the user by their User ID
        ///  Last Updated By:
        /// Last Updated:
        /// What was Changed:
        public List<DataDomain.Task> SelectTasksByUserID(int UserID)
        {
            IEnumerable<DataDomain.Task> tasks = from t in _tasks
                                                 join ta in _tasksAssigned on t.TaskID equals ta.TaskID
                                                 where ta.UserID == UserID && t.Active
                                                 select t;
            return tasks.ToList();
        }

    }
}
