/// <summary>
/// Ellie Wacker
/// Created: 2025-03-05
/// 
/// Class that implements the IUserSystemRoleManager Interface - used for
/// managing UserSystemRole data from UserSystemRole data fake objects &/or the DB.
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
    public class UserSystemRoleManager : IUserSystemRoleManager
    {
        private IUserSystemRoleAccessor _userSystemRoleAccessor;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025-03-05
        /// 
        /// Default constructor for UserSystemRoleManager
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public UserSystemRoleManager()
        {
            _userSystemRoleAccessor = new UserSystemRoleAccessor();

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025-03-05
        /// 
        /// Parameterized constructor for UserSystemRoleManager
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public UserSystemRoleManager(IUserSystemRoleAccessor userSystemRoleAccessor)
        {
            _userSystemRoleAccessor = userSystemRoleAccessor;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025-03-05
        /// 
        /// Method for InsertUserSystemRole
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int InsertUserSystemRole(int userID, string systemRoleID)
        {
            int result = 0;
            try
            {
                result = (_userSystemRoleAccessor.AddUserSystemRole(userID, systemRoleID));
                if (result == 0)
                {
                    throw new ApplicationException("Insert User System Role Failed");
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
        /// Created: 2025/03/06
        /// 
        /// Method for GetUserSystemRolesByUserID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<UserSystemRole> GetUserSystemRolesByUserID(int userID)
        {
            List<UserSystemRole> userSystemRoles = new List<UserSystemRole>();
            try
            {
                userSystemRoles = _userSystemRoleAccessor.SelectUserSystemRolesByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("UserSystemRole list not found", ex);
            }
            return userSystemRoles;
        }

        /// <summary>
        /// Eric Idle
        /// Created: 2025/02/13
        /// 
        /// Method for Removing a System Role from a User
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public bool RemoveUserSystemRole(int userID, string systemRoleID)
        {
            try
            {
                _userSystemRoleAccessor.DeleteUserSystemRole(userID, systemRoleID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return true;
        }
    }
}
