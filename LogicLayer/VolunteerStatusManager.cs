/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/06
/// Summary:  VolunteerStatusManager class that Manages VolunteerStatus objects, 
///           and calls VolunteerStatusAccessor methods
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/06
/// What was Changed: Initial creation	
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025/04/01
/// What was changed: Added the AddVolunteerStatus & SelectVolunteerStatusByProjectID methods.
/// </remarks>

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
    public class VolunteerStatusManager : IVolunteerStatusManager
    {
        private IVolunteerStatusAccessor _volunteerStatusAccessor;
        public VolunteerStatusManager()
        {
            _volunteerStatusAccessor = new VolunteerStatusAccessor();
        }
        public VolunteerStatusManager(IVolunteerStatusAccessor volunteerStatusAccessor)
        {
            _volunteerStatusAccessor = volunteerStatusAccessor;
        }

        public List<VMVolunteerStatus> GetRejectedVolunteerStatusByProjectID(int projectID)
        {
            List<VMVolunteerStatus> volunteerStatus = null;

            try
            {
                volunteerStatus = _volunteerStatusAccessor.SelectRejectedVolunteerStatusByProjectID(projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return volunteerStatus;
        }

        public List<VMVolunteerStatus> GetPendingVolunteerStatusByProjectID(int projectID)
        {
            List<VMVolunteerStatus> volunteerStatus = null;

            try
            {
                volunteerStatus = _volunteerStatusAccessor.SelectPendingVolunteerStatusByProjectID(projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return volunteerStatus;
        }

        public bool UpdateVolunteerStatus(VMVolunteerStatus volunteerStatus)
        {
            bool result = false;;

            try
            {
                result = (1 == _volunteerStatusAccessor.UpdateVolunteerStatusByUserIDAndProjectID(volunteerStatus));
                if (!result)
                {
                    throw new ApplicationException("Update failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// The manager method for deactivating a volunteer
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public int DeactivateVolunteerByUserIDAndProjectID(int userID, int projectID)
        {
            int result = 0;
            try
            {
                result = _volunteerStatusAccessor.DeactivateVolunteerByUserIDAndProjectID(userID, projectID);
                if (result == 0)
                {
                    // throw new ApplicationException("Volunteer Status not updated");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/12
        /// 
        /// The manager method for selecting a volunteer status
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public VolunteerStatus GetVolunteerStatusByUserIDAndProjectID(int userID, int projectID)
        {
            VolunteerStatus volunteerStatus = null;

            try
            {
                volunteerStatus = _volunteerStatusAccessor.SelectVolunteerStatusByUserIDAndProjectID(userID, projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No volunteerStatus Found", ex);
            }

            return volunteerStatus;
        }

        //Author: Akoi Kollie
        public bool AddVolunteerStatus(int userid, int projectid)// test this code, it only passed if the there is new user added to the database.
        {
            bool results = false;
            try
            {
                results = (1 == _volunteerStatusAccessor.AddVolunteerStatus(userid, projectid));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("get volunteer failed", ex);
            }
            return results;
        }

        //Author: Akoi Kollie
        public List<VolunteerStatus> SelectVolunteerStatusByProjectID(int projectid)
        {
            List<VolunteerStatus> volunteerStatuses = new List<VolunteerStatus>();
            try
            {
                volunteerStatuses = _volunteerStatusAccessor.SelectVolunteerStatusByProjectID(projectid);

            }
            catch (Exception ex)
            {
                throw;
            }
            return volunteerStatuses;
        }
        
    }
}
