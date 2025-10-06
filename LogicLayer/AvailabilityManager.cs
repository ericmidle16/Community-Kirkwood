/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/02/02
/// Summary: This AvailabilityManager class implements IAvailabilityManager
/// and handles availability-related operations. It interacts with the
/// AvailabiltyAccessor to insert volunteers avaiability and check that 
/// the availability being added does not already exist for the current
/// volunteer
/// Last Updated By: Skyann Heintz  
/// Last Updated: 2025-03-12
/// What Was Changed: Added SelectAvailabilityByUser
/// /// Last Updated By: Dat Tran
/// Last Updated: 2025-03-15
/// What was changed: Deleted UnassignedVolunteersManager and moved the method into this class.
/// 
/// Last Updated By: Chase Hannen
///	Last Updated Date:  2025-04-11
///	What Was Changed:
///	    Added SelectAvailabilityVMByProjectID
/// </summary>
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AvailabilityManager : IAvailabilityManager
    {
        private IAvailabilityAccessor _availabilityAccessor;

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Default constructor that initializes 
        /// the AvailabilityManager with a concrete AvailabilityAccessor.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public AvailabilityManager()
        {
            _availabilityAccessor = new AvailabilityAccessor();
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Overloaded constructor that allows dependency 
        /// injection by accepting an IAvailabilityAccessor instance.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public AvailabilityManager(IAvailabilityAccessor availabilityAccessor)
        {
            _availabilityAccessor = availabilityAccessor;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Inserts a new availability record for a user into the database.
        /// Returns true if the availability was successfully added, otherwise false.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool InsertAvailability(int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate)
        {
            try
            {
                int rowsAffected = _availabilityAccessor.InsertAvailability(userID, isAvailable, repeatWeekly, startDate, endDate);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Availability insertion failed.", ex);
            }
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: This method checks if there is any existing availability for the specified user / volunteer
        /// within the given date range. If an error occurs, a new exception is thrown.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool SelectExistingAvailability(int userID, DateTime startDate, DateTime endDate)
        {
            try
            {
                AvailabilityAccessor accessor = new AvailabilityAccessor();
                return accessor.SelectExistingAvailability(userID, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception("Database error when checking existing availability.", ex);
            }
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/23
        /// Summary: Retrieves the availability information for a user based on their UserID.
        /// Returns a list of availability records for the specified user.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public List<Availability> SelectAvailabilityByUser(int userID)
        {
            try
            {
                return _availabilityAccessor.SelectAvailabilityByUser(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching availability by user.", ex);
            }
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 02/11/2025
        /// Summary: This method manages the method in UnassignedVolunteerAccessor. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public List<UserAvailability> GetAvailableUsers(bool isAvailable, int eventID)
        {
            List<UserAvailability> users = new List<UserAvailability>();
            try
            {
                users = _availabilityAccessor.SelectAvailableUsers(isAvailable, eventID);

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not available.", ex);
            }
            return users;
        }

        // Author: Chase Hannen
        public List<AvailabilityVM> SelectAvailabilityVMByProjectID(int projectID)
        {
            List<AvailabilityVM> availabilities = new List<AvailabilityVM>();
            try
            {
                availabilities = _availabilityAccessor.SelectAvailabilityVMByProjectID(projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Availability retrieval failed", ex);
            }
            return availabilities;
        } // END SelectAvailabilityVMByProjectID

        public bool UpdateAvailabilityByID(int availabilityID, int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate)
        {
            try
            {
                int rowsAffected = _availabilityAccessor.UpdateAvailabilityByID(
                    availabilityID,
                    userID,
                    isAvailable,
                    repeatWeekly,
                    startDate,
                    endDate);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating availability.", ex);
            }
        }

        public bool DeleteAvailabilityByAvailabilityID(int availabilityID)
        {
            bool success = false;

            try
            {
                int rowsAffected = _availabilityAccessor.DeleteAvailabilityByAvailabilityID(availabilityID);
                success = rowsAffected >= 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete availability records", ex);
            }

            return success;
        }
    }
}
