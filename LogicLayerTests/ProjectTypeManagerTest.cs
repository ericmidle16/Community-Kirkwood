/// <summary>
/// Christivie Mauwa
/// Created: 2025/02/06
/// 
/// Actual summary of the class if needed, example is for DTO
/// Class for the creation of User Objects with set data fields
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessFakes;
using DataAccessInterfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace LogicLayerTests
{
    [TestClass]
    public class ProjectTypeManagerTest
    {
        private IProjectTypeAccessor _projectTypeAccessor;
        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        public ProjectTypeManagerTest()
        {
            _projectTypeAccessor = new ProjectTypeAccessorFakes();
        }
        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        [TestMethod]
        public void TestGetProjectTypeID()
        {
            // arrange
            IProjectTypeManager projectTypeManager = new ProjectTypeManager(_projectTypeAccessor);
            string expectedID = "Test";
            // act
            var result = projectTypeManager.GetProjectTypeID(expectedID);
            // assert
            Assert.AreEqual(expectedID,result.ProjectTypeID, "No Project Type found for the user.");

        }
    }
}
