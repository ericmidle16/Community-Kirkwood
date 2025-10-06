/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/10
/// Summary:Defines the IAvailabilityAccessor interface, 
/// which includes methods for inserting a 
/// new avaiablity into the database, checking if that avaiabililty 
/// exists already by user id, and selecting avaiabililty by userid.
/// Last Upaded By: Skyann Heintz
/// Last Updated:2025-03-12
/// What Was Changed: Added SelectAvailabilityByUser
/// /// Last Updated By: Dat Tran
/// Last Updated: 2025-03-15
/// What was changed: Deleted IUnassignedVolunteersAccessor and moved the method into this class.
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-11
/// What Was Changed:
///     Added SelectAvailabilityVMByProjectID
/// </summary>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IAvailabilityAccessor
    {
        int InsertAvailability(int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate);
        bool SelectExistingAvailability(int userID, DateTime startDate, DateTime endDate);
        List<Availability> SelectAvailabilityByUser(int userID);
        public List<UserAvailability> SelectAvailableUsers(bool isAvailable, int eventID);
        List<AvailabilityVM> SelectAvailabilityVMByProjectID(int projectID);
        public int UpdateAvailabilityByID(int availabilityID, int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate);
        public int DeleteAvailabilityByAvailabilityID(int availabilityID);
    }
}

