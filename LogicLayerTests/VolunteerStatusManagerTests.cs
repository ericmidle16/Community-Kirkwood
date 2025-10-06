/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/14
/// Summary:  Class for testing VolunteerStatusManager methods
/// with fake data from the VolunteerStatusAccessorFakes
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/20
/// What was Changed: Added TestSelectRejectedVolunteerStatusByUserID test method
/// 
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025/03/31
/// What was Changed: Added all my test methods
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What was Changed:
///     Added the TestReturnTrueAddVolunteerStatus, TestReturnFalseAddVolunteerStatus,
///     TestGetVolunteerStatusByProjectID, & TestIfNotGetVolunteerStatusByProjectID methods.
/// </remarks>
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
    public class VolunteerStatusManagerTests
    {
        private IVolunteerStatusManager? _volunteerStatusManager;
        VMVolunteerStatus _volunteerStatus = new VMVolunteerStatus();

        [TestInitialize]
        public void TestSetup()
        {
            _volunteerStatusManager = new VolunteerStatusManager(new VolunteerStatusAccessorFakes());
            _volunteerStatus.UserID = 1;
            _volunteerStatus.ProjectID = 1;
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/14
        /// Summary:  TestMethod that test that the GetVolunteerStatusByUserID method 
        /// returns all stored VolunteerStatus objects from the project that the given userID started
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/14
        /// What was Changed: Initial creation	
        /// </summary>
        [TestMethod]
        public void TestGetVolunteerStatusByUserID()
        {
            int numberOfVolunteerStatuses = 2;
            List<VMVolunteerStatus> volunteerStatus = _volunteerStatusManager.GetPendingVolunteerStatusByProjectID(1);
            Assert.AreEqual(numberOfVolunteerStatuses, volunteerStatus.Count);
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/20
        /// Summary:  TestMethod that test that the GetRejectedVolunteerStatusByUserID method 
        /// returns all stored rejected VolunteerStatus objects from the project that the given userID started
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/20
        /// What was Changed: Initial creation	
        /// </summary>
        [TestMethod]
        public void TestSelectRejectedVolunteerStatusByUserID()
        {
            int numberOfVolunteerStatuses = 2;
            List<VMVolunteerStatus> volunteerStatus = _volunteerStatusManager.GetRejectedVolunteerStatusByProjectID(1);
            Assert.AreEqual(numberOfVolunteerStatuses, volunteerStatus.Count);
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/14
        /// Summary:  TestMethod that test that the UpdateVolunteerStatus method 
        /// returns true if update is successful
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/14
        /// What was Changed: Initial creation	
        /// </summary>
        [TestMethod]
        public void TestUpdateVolunteerStatusSuccess()
        {
            bool volunteerStatus = _volunteerStatusManager.UpdateVolunteerStatus(_volunteerStatus);
            Assert.AreEqual(true, volunteerStatus);
            
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/14
        /// Summary:  TestMethod that test that the UpdateVolunteerStatus method 
        /// returns ApplicationException if update failed
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/14
        /// What was Changed: Initial creation	
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateVolunteerStatusFail()
        {
            VMVolunteerStatus badVolunteerStatus = new VMVolunteerStatus();
            badVolunteerStatus.ProjectID = 5;
            badVolunteerStatus.UserID = 5;
            badVolunteerStatus.Approved = true;
            bool volunteerStatus = _volunteerStatusManager.UpdateVolunteerStatus(badVolunteerStatus);
        }

        [TestMethod]
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// The test method that tests if deactivating a volunteer succeeds
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public void TestDeactivateVolunteerByUserIDAndProjectIDReturnsTrue()
        {
            //arrange
            const int userID = 2;
            const int projectID = 1;
            const int expectedResult = 1;
            int actualResult = 0;

            //act
            actualResult = _volunteerStatusManager.DeactivateVolunteerByUserIDAndProjectID(userID, projectID);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// The test method that tests if deactivating a volunteer with bad userID and projectID fails
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public void TestDeactivateVolunteerByUserIDAndProjectIDReturnsFalse()
        {
            //arrange
            const int userID = 0;
            const int projectID = 0;

            //act
            _volunteerStatusManager.DeactivateVolunteerByUserIDAndProjectID(userID, projectID);
        }

        [TestMethod]
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/12
        /// 
        /// The test method that tests if selecting a volunteer status by valid user and project ids returns the correct result
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>         
        public void TestGetVolunteerStatusByUserIDAndProjectIDReturnsCorrectVolunteerStatus()
        {
            // Arrange
            const int userID = 2;
            const int projectID = 1;

            const bool approved = true;
            VolunteerStatus volunteerStatus = null;
            // Act
            volunteerStatus = _volunteerStatusManager.GetVolunteerStatusByUserIDAndProjectID(userID, projectID);
            // Assert
            Assert.IsNotNull(volunteerStatus);
            Assert.AreEqual(approved, volunteerStatus.Approved);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/12
        /// 
        /// The test method that tests if selecting a volunteer status by an invalid user id throws an exception
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>                 
        public void TestGetVolunteerStatusByInvalidUserIDThrowsApplicationException()
        {
            // Arrange
            const int userID = 20;
            const int projectID = 20;
            VolunteerStatus volunteerStatus = null;
            // Act
            volunteerStatus = _volunteerStatusManager.GetVolunteerStatusByUserIDAndProjectID(userID, projectID);
        }

        [TestMethod]
        //Author: Akoi Kollie
        public void TestReturnTrueAddVolunteerStatus()
        {
            //Arrange
            const int userid = 1005;
            const int projectid = 1000;
            const bool expectedResult = true;
            bool actualResult = false;
            //Act
            actualResult = _volunteerStatusManager.AddVolunteerStatus(userid, projectid);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        //Author: Akoi Kollie
        public void TestReturnFalseAddVolunteerStatus()
        {
            //Arrange
            const int userid = 10;
            const int projectid = 1;
            const bool Approved = true;
            const bool expectedResult = false;
            bool actualResult = true;
            //Act
            actualResult = _volunteerStatusManager.AddVolunteerStatus(userid, projectid);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        //Author:Akoi Kollie
        public void TestGetVolunteerStatusByProjectID()
        {
            //Arrange
            int VolunteerStatusnumbers = 1;
            List<VolunteerStatus> volunteerStatuses = _volunteerStatusManager.SelectVolunteerStatusByProjectID(1);
            //Assert
            Assert.AreEqual(VolunteerStatusnumbers, volunteerStatuses.Count());

        }
        [TestMethod]
        //Author: Akoi Kollie
        public void TestIfNotGetVolunteerStatusByProjectID()
        {
            //Arrange
            int VolunteerStatusnumbers = 0;
            List<VolunteerStatus> volunteerStatuses = _volunteerStatusManager.SelectVolunteerStatusByProjectID(2);
            //Assert
            Assert.AreEqual(VolunteerStatusnumbers, volunteerStatuses.Count());
        }
    }
}
