/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/04/21
/// Summary: Unit tests for the ForumPermissionAccessorFake class to test user write access selection.
/// 
/// Updated By: Syler Bushlack
/// Updated: 2025-04-24
/// What Was Changed: added test methods for GettForumPermissionsByProjectID and EditForumPermissionWriteAccessValue
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed:
///     Added the test for adding a new ForumPermission record - TestAddForumPermissionReturnsTrueForSuccessfulAdd.
/// </summary>

using DataAccessFakes;
using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class ForumPermissionManagerTests
    {
        private IForumPermissionManager? _forumPermissionManager;

        [TestInitialize]
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: This is a unit test setup method for initializing 
        /// _forumPermissionManager before running tests.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void Setup()
        {
            _forumPermissionManager = new ForumPermissionManager(new ForumPermissionAccessorFake());
        }

        [TestMethod]
        // Author: Kate Rich
        public void TestAddForumPermissionReturnsTrueForSuccessfulAdd()
        {
            // Arrange
            ForumPermission forumPermission = new ForumPermission()
            {
                UserID = 5,
                ProjectID = 3,
                WriteAccess = true
            };

            const bool expectedValue = true; // Supposed to pass
            bool actualValue = false;

            // Act
            actualValue = _forumPermissionManager.AddForumPermission(forumPermission);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: This test verifies that the user has write access for a given project.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestSelectUserWriteAccessReturnsTrueForUserWithWriteAccess()
        {
            // Arrange
            int userID = 1;
            int projectID = 101;

            // Act
            bool result = _forumPermissionManager.SelectUserWriteAccess(userID, projectID);

            // Assert
            Assert.IsTrue(result, "Expected the user to have write access.");
        }

        [TestMethod]
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: This test verifies that the user does not have write access for a given project.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestSelectUserWriteAccessReturnsFalseForUserWithoutWriteAccess()
        {
            // Arrange
            int userID = 2;
            int projectID = 101;

            // Act
            bool result = _forumPermissionManager.SelectUserWriteAccess(userID, projectID);

            // Assert
            Assert.IsFalse(result, "Expected the user to not have write access.");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: This test verifies that an exception is thrown when trying to 
        /// select write access for a user that does not exist in the project.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestSelectUserWriteAccessThrowsExceptionForNonExistingUser()
        {
            // Arrange
            int userID = 999; // Non-existing user
            int projectID = 101;

            // Act
            _forumPermissionManager.SelectUserWriteAccess(userID, projectID);
        }

        [TestMethod]
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: This test verifies that a user can have write access to a different project.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestSelectUserWriteAccessReturnsTrueForDifferentProject()
        {
            // Arrange
            int userID = 3;
            int projectID = 102;

            // Act
            bool result = _forumPermissionManager.SelectUserWriteAccess(userID, projectID);

            // Assert
            Assert.IsTrue(result, "Expected the user to have write access to a different project.");
        }

        ///<summary>
        /// Creator: Syelr Bushlack
        /// Created: 2025/04/24
        /// Summary: This test verifies that GettForumPermissionsByProjectID returns all permissions for a project
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        public void TestGettForumPermissionsByProjectIDReturnsAllPermissionsForAProject()
        {
            // Arrange
            int projectID = 101;
            int expectedPermissions = 2;

            // Act
            List<ForumPermissionVM> permissions = _forumPermissionManager.GettForumPermissionsByProjectID(projectID);

            // Assert
            Assert.AreEqual(expectedPermissions, permissions.Count());
        }

        ///<summary>
        /// Creator: Syelr Bushlack
        /// Created: 2025/04/24
        /// Summary: This test verifies that GettForumPermissionsByProjectID returns all permissions for a project
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        public void TestGettForumPermissionsByProjectIDReturns0WhenNoPermissionssAreFound()
        {
            // Arrange
            int projectID = 0;
            int expectedPermissions = 0;

            // Act
            List<ForumPermissionVM> permissions = _forumPermissionManager.GettForumPermissionsByProjectID(projectID);

            // Assert
            Assert.AreEqual(expectedPermissions, permissions.Count());
        }

        ///<summary>
        /// Creator: Syelr Bushlack
        /// Created: 2025/04/24
        /// Summary: This test verifies that EditForumPermissionWriteAccessValue returns true when a permission is updated
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        public void TestEditForumPermissionWriteAccessValueReturnsTrueWhenApermissionIsUpdated()
        {
            // Arrange
            ForumPermission permission = new ForumPermission();
            permission.ProjectID = 101;
            permission.UserID = 1;
            permission.WriteAccess = true;

            // Act
            bool success = _forumPermissionManager.EditForumPermissionWriteAccessValue(permission);

            // Assert
            Assert.IsTrue(success);
        }

        ///<summary>
        /// Creator: Syelr Bushlack
        /// Created: 2025/04/24
        /// Summary: This test verifies that EditForumPermissionWriteAccessValue returns false when a permission is updated
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        public void TestEditForumPermissionWriteAccessValueReturnsFalseWhenApermissionIsUpdated()
        {
            // Arrange
            ForumPermission permission = new ForumPermission();
            permission.ProjectID = 101;
            permission.UserID = 0;
            permission.WriteAccess = true;

            // Act
            bool success = _forumPermissionManager.EditForumPermissionWriteAccessValue(permission);

            // Assert
            Assert.IsFalse(success);
        }

    }
}