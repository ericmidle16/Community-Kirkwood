/// <summary>
/// Creator:  Kate Rich
/// Created:  2025/02/02
/// Summary:
///		Test Class for ensuring that each ProjectManager method
/// 	returns the expected values.
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/06
/// What was Changed: Initial creation	
/// </remarks>
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/04/04
/// What was Changed: Added TestGetProjectsByUserID
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/04/17
/// What was Changed: Added test methods for the TestGetProjectVMsByUserID method
/// 
/// Last Updated By: Brodie Pasker
/// Last Updated: 2025/04/25
/// What was Changed: Added test methods for getting projects the user is approved on
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using DataAccessInterfaces;
using DataAccessFakes;
using DataDomain;
using DataAccessLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class ProjectManagerTests
    {
        private IProjectManager? _projectManager;

        [TestInitialize]
        public void TestSetup()
        {
            _projectManager = new ProjectManager(new ProjectAccessorFake());
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/04
        /// Summary:  TestMethod that test that the GetAllProjects method 
        /// returns all stored project objects
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/04
        /// What was Changed: Initial creation	
        /// </summary>
        [TestMethod]
        public void TestGetAllProjectsReturnsAllProjects()
        {
            int numberOfProjects = 4;
            List<ProjectVM> projects = _projectManager.GetAllProjects();
            Assert.AreEqual(numberOfProjects, projects.Count);
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/04/17
        /// Summary:  TestMethod that test that the GetAllProjectsByUserID method 
        /// returns all stored project objects created by a a specific user
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/03/06
        /// What was Changed: Initial creation	
        /// </summary>
        [TestMethod]
        public void TestGetAllProjectsByUserID()
        {
            int numberOfProjects = 1;
            List<ProjectVM> projects = _projectManager.GetAllProjectVMsByUserID(100005);
            Assert.AreEqual(numberOfProjects, projects.Count);
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/04/17
        /// Summary:  TestMethod that tests that the GetAllProjectsByUserID method 
        /// fails if given a invalid userID
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/03/06
        /// What was Changed: Initial creation	
        /// </summary>
        [TestMethod]
        public void TestGetAllProjectsByUserIDFails()
        {
            int numberOfProjects = 1;
            int userID = 0;
            List<ProjectVM> projects = _projectManager.GetAllProjectVMsByUserID(userID);
            Assert.AreEqual(projects.Count, 0);
        }

        [TestMethod]
		// Author: Kate Rich
		public void TestGetProjectByIDReturnsCorrectProject()
		{
			// Arrange
			const int projectID = 1;
			const string expectedProjectTypeID = "Propane Emergency";
			const int expectedLocationID = 2;
			const int expectedUserID = 17;
			// DateOnly cannot be declared as a constant.
			DateTime expectedStartDate = new DateTime(2025, 2, 2);
			const string expectedStatus = "Active";
			const string expectedDescription = "Emergency delivery of propane to Tom Landry Middle School.";
			const decimal expectedAvailableFunds = 17m;
			Project project = null;
			// Act
			project = _projectManager.GetProjectByID(projectID);
			// Assert
			Assert.IsNotNull(project);
			Assert.AreEqual(expectedProjectTypeID, project.ProjectTypeID);
			Assert.AreEqual(expectedLocationID, project.LocationID);
			Assert.AreEqual(expectedUserID, project.UserID);
			Assert.AreEqual(expectedStartDate, project.StartDate);
			Assert.AreEqual(expectedStatus, project.Status);
			Assert.AreEqual(expectedDescription, project.Description);
			Assert.AreEqual(expectedAvailableFunds, project.AvailableFunds);
		}

		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		// Author: Kate Rich
		public void TestGetProjectByIDThrowsApplicationExceptionForUnknownProjectID()
		{
			// Arrange
			const int projectID = 4;
			Project project = null;
			// Act
			project = _projectManager.GetProjectByID(projectID);
			// Assert
			// Nothing to do - Looking for an exception.
		}

		[TestMethod]
		// Author: Kate Rich
		public void TestGetProjectInformationByProjectIDReturnsCorrectProject()
		{
			// Arrange
			const int projectID = 1;
			const string expectedProjectTypeID = "Propane Emergency";
			const int expectedLocationID = 2;
			const string expectedProjectLocationName = "Tom Landry Middle School";
			const string expectedProjectCity = "Arlen";
			const string expectedProjectState = "Texas";
			const int expectedUserID = 17;
			// DateOnly cannot be declared as a constant.
			DateTime expectedStartDate = new DateTime(2025, 2, 2);
			const string expectedStatus = "Active";
			const string expectedDescription = "Emergency delivery of propane to Tom Landry Middle School.";
			const decimal expectedAvailableFunds = 17m;
			ProjectVM project = null;
			// Act
			project = _projectManager.GetProjectInformationByProjectID(projectID);
			// Assert
			Assert.IsNotNull(project);
			Assert.AreEqual(expectedProjectTypeID, project.ProjectTypeID);
			Assert.AreEqual(expectedLocationID, project.LocationID);
			Assert.AreEqual(expectedProjectLocationName, project.LocationName);
			Assert.AreEqual(expectedUserID, project.UserID);
			Assert.AreEqual(expectedStartDate, project.StartDate);
			Assert.AreEqual(expectedStatus, project.Status);
			Assert.AreEqual(expectedDescription, project.Description);
			Assert.AreEqual(expectedAvailableFunds, project.AvailableFunds);
		}

		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		// Author: Kate Rich
		public void TestGetProjectInformationByProjectIDThrowsApplicationExceptionForUnknownProjectID()
		{
			// Arrange
			const int projectID = 4;
			Project project = null;
			// Act
			project = _projectManager.GetProjectInformationByProjectID(projectID);
			// Assert
			// Nothing to do - Looking for an exception.
		}

        // Author: Christivie
        [TestMethod]
        public void TestGetProjectByIDReturnCorrectProject()
        {
            // Arrange
            int projectID = 3; // Assuming a project with ID 1 exists in the fake accessor

            // Act
            ProjectVM project = _projectManager.GetProjectVMByID(projectID);

            // Assert
            Assert.AreEqual(projectID, project.ProjectID);
		}

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/07
        /// Summary: TestMethod to test that the createProject 
        /// method will create a project as intended
        /// 
        /// Last Updated By: Kate Rich
        /// Last Updated: 2025-05-02
        /// What Was Changed: Updated this test to match the other changes I made
        ///     to AddProject() in other files (returning an int - the newly
        ///     created ProjectID).
        /// </summary>
        [TestMethod]
        public void TestProjectCreation_ShouldAddProjectToList_WhenValidProjectIsProvided()
        {
            // Arrange
            int expectedResult = 200000;
            int actualResult = 0;
            var newProject = new ProjectVM()
            {
                ProjectID = 200000,
                Name = "New Project",
                LocationID = 100000,
                UserID = 100001,
                StartDate = new DateTime(2025, 5, 1),
                Description = "New Description"
            };

            // Act
            actualResult = _projectManager.AddProject(newProject);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/07
        /// Summary: TestMethod to test that the createProject 
        /// method will not create a project with invalid data
        /// 
        /// Last Updated By: Kate Rich
        /// Last Updated: 2025-05-02
        /// What Was Changed: Updated this test to match the other changes I made
        ///     to AddProject() in other files (returning an int - the newly
        ///     created ProjectID).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestProjectCreation_ShouldNotAddProjectToList_WhenInvalidProjectIsProvided()
        {
            // Arrange
            int expectedResult = 0;
            int actualResult = 200000;
            var newProject = new ProjectVM()
            {
                ProjectID = 200000,
                Name = "Project Name 1",
                LocationID = 100000,
                UserID = 100002,
                StartDate = new DateTime(2025, 1, 1),
                Description = "Project Description 1"
            };

            // Act
            actualResult = _projectManager.AddProject(newProject);

            // Assert
            // redundent due to ExpectedException
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/03/27
        /// Summary: TestMethod to test that the GetAllProjectTypes 
        /// method pulls all of the correct data being requested
        /// </summary>
        [TestMethod]
        public void TestGetAllProjectTypes_ShouldContainTwoProjectTypes()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            actualCount = _projectManager.GetAllProjectTypes().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        // Author: Chase Hannen
        public void TestGetProjectsByUserID()
        {
            // Arrange
            const int userID = 6;
            const int expectedCount = 1;
            int actualCount = 0;
            // Act
            actualCount = _projectManager.GetAllProjectsByUserID(userID).Count();
            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        // Author: Christivie MAuwa

        [TestMethod]
        public void TestUpdateProject()
        {
            // Arrange
            var oldProject = new ProjectVM()
            {
                ProjectID = 1,
                Name = "Propane Delivery to TLMS",
                ProjectTypeID = "Propane Emergency",
                LocationID = 2,
                LocationName = "Tom Landry Middle School",
                City = "Arlen",
                State = "Texas",
                UserID = 17,
                StartDate = new DateTime(2025, 2, 2),
                Status = "Active",
                Description = "Emergency delivery of propane to Tom Landry Middle School.",
                AvailableFunds = 17m
            };
            var newProject = new ProjectVM()
            {
                ProjectID = 2,
                Name = "Propane Delivery to TLMS",
                ProjectTypeID = "Propane Emergency",
                LocationID = 4,
                LocationName = "Tom Landry Middle School",
                City = "Arlen",
                State = "Texas",
                UserID = 17,
                StartDate = new DateTime(2025, 2, 2),
                Status = "Active",
                Description = "Emergency delivery of propane to Tom Landry Middle School.",
                AvailableFunds = 17m
            };
            bool expectedProject = true;

            // Act
            bool actualProject = _projectManager.EditProject(oldProject, newProject);

            // Assert
            Assert.AreEqual(expectedProject, actualProject);
        }
        // Author: Brodie Pasker
        public void GetAllApprovedProjectsByUserID()
        {
            // Arrange
            const int userID = 6;
            const int expectedCount = 1;
            int actualCount = 0;
            // Act
            actualCount = _projectManager.GetAllApprovedProjectsByUserID(userID).Count();
            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}