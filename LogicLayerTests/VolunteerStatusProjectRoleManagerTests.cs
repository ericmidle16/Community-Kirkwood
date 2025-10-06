/// <summary>
/// Ellie Wacker
/// Created: 2025-02-09
/// 
/// Test Class for ensuring that each VoluneerStatusProjectRole method
/// returns the expected values.

using DataAccessFakes;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class VolunteerStatusProjectRoleManagerTests
    {
        private IVolunteerStatusProjectRoleManager? _volunteerStatusProjectRoleManager;


        [TestInitialize]
        public void TestSetup()
        {
            _volunteerStatusProjectRoleManager = new VolunteerStatusProjectRoleManager(new VolunteerStatusProjectRoleAccessorFake());

        }

        [TestMethod]
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// This is the test method for if deleting a user role is successful
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public void TestDeleteUserRolesReturnsTrue()
        {
            //arrange
            const int userID = 1;
            const int projectID = 1;
            const int expectedResult = 1;
            int actualResult = 0;

            //act
            actualResult = _volunteerStatusProjectRoleManager.DeleteUserRoles(userID, projectID);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// This is the test method for if deleting a user role that doesnt exist returns false
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public void TestDeleteUserRolesReturnsFalse()
        {
            //arrange
            const int userID = 0;
            const int projectID = 0;

            //act
            _volunteerStatusProjectRoleManager.DeleteUserRoles(userID, projectID);
        }
    }
   }
