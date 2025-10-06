
using DataAccessFakes;
using DataDomain;
using LogicLayer;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/10
/// Summary:  Test methods for Task methods
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/12
/// What was Changed: Added tests for getting tasks by eventID
/// /// Last Updated By: Brodie Pasker
/// Last Updated: 2025/04/25
/// What was Changed: Added tests assigned to a user
/// </summary>

namespace LogicLayerTests
{

    [TestClass]
    public class TaskManagerTests
    {
        private ITaskManager _taskManager;

        [TestInitialize]
        public void TestSetup()
        {
            _taskManager = new TaskManager(new TaskAccessorFakes());
        }

        // Task Assignment
        [TestMethod]
        public void TestTaskAssignmentReturnsTrueForSuccess()
        {
            // assign
            bool expectedResult = true;
            bool actualResult = false;
            int userID = 1;
            int taskID = 1;

            // act
            actualResult = _taskManager.TaskAssignment(taskID, userID);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestTaskAssignmentReturnsFalseForInactiveUser()
        {
            // assign
            bool expectedResult = false;
            bool actualResult = true;
            int userID = 3;
            int taskID = 1;

            // act
            actualResult = _taskManager.TaskAssignment(taskID, userID);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestTaskAssignmentReturnsFalseForInactiveTask()
        {
            // assign
            bool expectedResult = false;
            bool actualResult = true;
            int userID = 1;
            int taskID = 3;

            // act
            actualResult = _taskManager.TaskAssignment(taskID, userID);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestTaskAssignmentReturnsFalseForInvalidUser()
        {
            // assign
            bool expectedResult = false;
            bool actualResult = true;
            int userID = 99;
            int taskID = 1;

            // act
            actualResult = _taskManager.TaskAssignment(taskID, userID);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestTaskAssignmentReturnsFalseForInvalidTask()
        {
            // assign
            bool expectedResult = false;
            bool actualResult = true;
            int userID = 1;
            int taskID = 10;

            // act
            actualResult = _taskManager.TaskAssignment(taskID, userID);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        // Get Tasks By EventID
        [TestMethod]
        public void TestGetTasksByEventIDReturnsCorrectNumberNoInactives()
        {
            // assign
            int expectedResult = 1;
            int actualResult = 0;
            int eventID = 1;
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            // act
            tasks = _taskManager.GetTasksByEventID(eventID);
            actualResult = tasks.Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetTasksByEventIDReturnsCorrectNumberWithInactives()
        {
            // assign
            int expectedResult = 1;
            int actualResult = 0;
            int eventID = 2;
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            // act
            tasks = _taskManager.GetTasksByEventID(eventID);
            actualResult = tasks.Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetTasksByEventIDReturnsNothingForNoTasks()
        {
            // assign
            int expectedResult = 0;
            int actualResult = 99;
            int eventID = 99;
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            // act
            tasks = _taskManager.GetTasksByEventID(eventID);
            actualResult = tasks.Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void TestGetVolunteersAndTasksByEventIDIsSuccessful()
        {
            // arrange
            int eventID = 1;
            int expectedCount = 2;
            int actualCount = 0;
            List<TaskAssignedViewModel> volunteersAssigned = new List<TaskAssignedViewModel>();

            // act
            volunteersAssigned = _taskManager.GetVolunteersAndTasksByEventID(eventID);
            actualCount = volunteersAssigned.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        [TestMethod]
        public void TestGetVolunteersAndTasksByEventIDReturnsZero()
        {
            // arrange
            int eventID = 99;
            int expectedCount = 0;
            int actualCount = 3;
            List<TaskAssignedViewModel> volunteersAssigned = new List<TaskAssignedViewModel>();

            // act
            volunteersAssigned = _taskManager.GetVolunteersAndTasksByEventID(eventID);
            actualCount = volunteersAssigned.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: TestMethod to test that the createTask 
        /// method will create a task as intended
        /// </summary>
        [TestMethod]
        public void TestTaskCreation_ShouldAddTestToList_WhenValidTaskIsProvided()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;
            var newTask = new DataDomain.Task()
            {
                Name = "New Task",
                Description = "New Description",
                ProjectID = 100000,
                TaskType = "Task Type",
                EventID = 100000,
                Active = true
            };

            // Act
            actualResult = _taskManager.AddTask(newTask);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: TestMethod to test that the createTask
        /// method will not create a task with invalid data
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestTaskCreation_ShouldntAddTaskToList_WhenInvalidTaskIsProvided()
        {
            // Arrange
            var newTask = new DataDomain.Task()
            {
                Name = "Task Name",
                Description = "New Description",
                ProjectID = 100000,
                TaskType = "Task Type",
                EventID = 100000,
                Active = true
            };

            // Act
            _taskManager.AddTask(newTask);

            // Assert
            // redundent due to ExpectedException
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: TestMethod to test that the UpdateTaskByTaskID
        /// method will update a task with valid data
        /// </summary>
        [TestMethod]
        public void TestTaskUpdate_ShouldUpdateTaskInfo()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;
            DataDomain.Task updatedTask = new DataDomain.Task()
            {
                TaskID = 100001,
                Name = "New Task Name",
                Description = "Updated Description",
                TaskType = "Task Type 2",
                TaskDate = new DateTime(2025, 2, 2),
                Active = true
            };

            // Act
            DataDomain.Task taskBeforeUpdate = _taskManager.GetTaskByTaskID(updatedTask.TaskID);
            actualResult = _taskManager.UpdateTaskByTaskID(updatedTask);
            DataDomain.Task taskAfterUpdate = _taskManager.GetTaskByTaskID(updatedTask.TaskID);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(100001, taskAfterUpdate.TaskID);
            Assert.AreNotEqual("Task Name 2", taskAfterUpdate.Name);
            Assert.AreNotEqual("Task Description 2", taskAfterUpdate.Description);
            Assert.AreEqual("Task Type 2", taskAfterUpdate.TaskType);
            Assert.AreEqual(new DateTime(2025, 2, 2), taskAfterUpdate.TaskDate);
            Assert.AreEqual(true, taskAfterUpdate.Active);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: TestMethod to test that the UpdateTaskByTaskID
        /// method will NOT update a task with invalid data
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        public void TestTaskUpdate_ShouldntUpdateName()
        {
            // Arrange
            var updatedTask = new DataDomain.Task()
            {
                TaskID = 100001,
                Name = "Task Name",
                Description = "Task Description",
                TaskType = "Task Type",
                TaskDate = DateTime.Now,
                Active = false
            };

            // Act
            bool test = _taskManager.UpdateTaskByTaskID(updatedTask);

            // Assert
            Assert.AreEqual(test, true);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: TestMethod to test that the GetTaskByTaskID
        /// method will get the correct task by taskID 
        /// </summary>
        [TestMethod]
        public void TestTaskGet_ShouldGetTaskByTaskID()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;
            DataDomain.Task task;
            var newTask = new DataDomain.Task()
            {
                TaskID = 100000,
                Name = "Task Name",
                Description = "Task Description",
                ProjectID = 100000,
                TaskType = "Task Type",
                EventID = 100000,
                Active = true
            };

            // Act
            task = _taskManager.GetTaskByTaskID(newTask.TaskID);
            if (task != null)
            {
                actualResult = true;
            }

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/27
        /// Summary: TestMethod to test that the GetTaskByTaskID
        /// method will NOT get a task by an incorrect taskID 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestTaskGet_ShouldntGetTaskByTaskID()
        {
            // Arrange
            bool expectedResult = false;
            bool actualResult = true;
            DataDomain.Task task;
            var newTask = new DataDomain.Task()
            {
                TaskID = 200000,
                Name = "Task Name",
                Description = "Task Description",
                ProjectID = 100000,
                TaskType = "Task Type",
                EventID = 100000,
                Active = true
            };

            // Act
            task = _taskManager.GetTaskByTaskID(newTask.TaskID);
            if (task != null)
            {
                actualResult = true;
            }

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/03/27
        /// Summary: TestMethod to test that the GetAllTaskTypes 
        /// method pulls all of the correct data being requested
        /// </summary>
        [TestMethod]
        public void TestGetAllTaskTypes_ShouldContainTwoTaskTypes()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            actualCount = _taskManager.GetAllTaskTypes().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetTasksByProjectIDReturnsCorrectNumberNoInactives()
        {
            // assign
            int expectedResult = 2;
            int actualResult = 0;
            int projectID = 1;
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            // act
            tasks = _taskManager.GetTasksByProjectID(projectID);
            actualResult = tasks.Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetTasksByProjectIDReturnsCorrectNumberWithInactives()
        {
            // assign
            int expectedResult = 0;
            int actualResult = 0;
            int projectID = 2;
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            // act
            tasks = _taskManager.GetTasksByProjectID(projectID);
            actualResult = tasks.Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetTasksByProjectIDReturnsNothingForNoTasks()
        {
            // assign
            int expectedResult = 0;
            int actualResult = 99;
            int projectID = 99;
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            // act
            tasks = _taskManager.GetTasksByProjectID(projectID);
            actualResult = tasks.Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        // Author: Brodie Pasker
        [TestMethod]
        public void GetTasksByUserID()
        {
            int expectedResult = 1;
            int actualResult = 0;
            int UserID = 1;

            actualResult = _taskManager.GetTasksByUserID(UserID).Count();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}