/// <summary>
/// Ellie Wacker
/// Created: 2025-02-09
/// 
/// Class that implements the IVolunteerStatusProjectRole Interface - used for
/// managing VolunteerStatusProjectRole data from VolunteerStatusProjectRole data fake objects &/or the DB.
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
    public class VolunteerStatusProjectRoleManager : IVolunteerStatusProjectRoleManager
    {
        private IVolunteerStatusProjectRoleAccessor _volunteerStatusProjectRoleAccessor;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// The default constructor for VolunteerStatusProjectRoleManager()
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public VolunteerStatusProjectRoleManager()
        {
            _volunteerStatusProjectRoleAccessor = new VolunteerStatusProjectRoleAccessor();
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// The parameterized constructor for VolunteerStatusProjectRoleManager()
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public VolunteerStatusProjectRoleManager(IVolunteerStatusProjectRoleAccessor volunteerStatusProjectRoleAccessor)
        {
            _volunteerStatusProjectRoleAccessor = volunteerStatusProjectRoleAccessor;
        }

        public int RemoveProjectRoleByUserIDProjectID(int userID, int projectID, string projectRoleID)
        {
            int result = 0;
            try
            {
                result = _volunteerStatusProjectRoleAccessor.DeleteUserProjectRoleByUserIDProjectID(userID, projectID, projectRoleID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Delete Project Role failed", ex);
            }

            return result;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/09
        /// 
        /// The manager method for deleteUserRoles()
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int DeleteUserRoles(int userID, int projectID)
        {
            int result = 0;
            try
            {
                result = _volunteerStatusProjectRoleAccessor.DeleteUserRoles(userID, projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Delete User Role failed", ex);
            }

            return result;
        }

        public List<VolunteerStatusProjectRole> GetUserProjectRolesByUserIDProjectID(int userID, int projectID)
        {
            List<VolunteerStatusProjectRole> userSystemRoles = new List<VolunteerStatusProjectRole>();
            try
            {
                userSystemRoles = _volunteerStatusProjectRoleAccessor.SelectUserProjectRolesByUserIDProjectID(userID, projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User's project role list not found", ex);
            }
            return userSystemRoles;
        }

        public int InsertUserProjectRole(int userID, int projectID, string projectRoleID)
        {
            int result = 0;
            try
            {
                result = (_volunteerStatusProjectRoleAccessor.AddUserRoles(userID, projectID, projectRoleID));
                if (result == 0)
                {
                    throw new ApplicationException("Insert User Project Role Failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
    }
}
