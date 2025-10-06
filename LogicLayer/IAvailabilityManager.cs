/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/10
/// Summary:Defines the IAvailabilityManager interface
/// in the LogicLayer namespace, which includes 
/// methods for checking if availability is inserted
/// and if the avaiability already exists in the database
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-12
/// What Was Changed: Added SelectAvailabilityByUser
/// Last Updated By: Dat Tran
/// Last Updated: 2025-03-15
/// What was changed: Deleted IUnassignedVolunteersManager and moved the method into this class.
/// 
/// Last Updated By: Chase Hannen
///	Last Updated Date:  2025-04-11
///	What Was Changed:
///	    Added SelectAvailabilityVMByProjectID
/// </summary>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IAvailabilityManager
    {
        public bool InsertAvailability(int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate);
        bool SelectExistingAvailability(int userID, DateTime startDate, DateTime endDate);
        public List<Availability> SelectAvailabilityByUser(int userID);
        public List<UserAvailability> GetAvailableUsers(bool isAvailable, int eventID);
        public List<AvailabilityVM> SelectAvailabilityVMByProjectID(int projectID);
        public bool UpdateAvailabilityByID(int availabilityID, int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate);
        public bool DeleteAvailabilityByAvailabilityID(int availabilityID);
    }
}
