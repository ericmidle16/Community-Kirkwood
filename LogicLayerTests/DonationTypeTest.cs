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
using DataDomain;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayerTests
{
    /// <summary>
    /// Christivie Mauwa
    /// Created: 2025/02/21
    /// 
    /// Actual summary of the class if needed.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    /// <param name="sender"></param>
    [TestClass]
    public class DonationTypeTest
    {
        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        private IDonationTypeAccessor _donationTypeAccessor;

        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        public DonationTypeTest()
        {
            _donationTypeAccessor = new DonationTypeFakes();
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
        public void TestGetAllDonationTypeTest()
        {
            // arrange
            List<DonationType> donationTypes;
            IDonationTypeManager donationTypeManager = new DonationTypeManager(_donationTypeAccessor);
            int expectedCount = 1;
            //act
            donationTypes = donationTypeManager.GetAllDonationType();
            //assert
            Assert.AreEqual(expectedCount, donationTypes.Count);
        }
    }
}
