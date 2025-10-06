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
using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
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
    /// <param name="e"></param>
    public class DonationTypeFakes:IDonationTypeAccessor
    {
        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        private List<DonationType> donationTypes;
        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        public DonationTypeFakes()
        {
            donationTypes = new List<DonationType>()
            {
                new DonationType()
                {
                    DonationTypeID = "Cash",
                    Description = "Cash donation"
                }
            };
        }

        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        public List<DonationType> GetAllDonationTypes()
        {
            return donationTypes;
        }

    }
}
