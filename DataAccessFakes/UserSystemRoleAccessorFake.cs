/// <summary>
/// Ellie Wacker
/// Created: 2025-03-05
/// 
/// Class for fake UserSystemRole objects that are used in testing.
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class UserSystemRoleAccessorFake : IUserSystemRoleAccessor
    {
        private List<UserSystemRole> _userSystemRoles;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/05
        /// 
        /// This method creates a fake userSystemRole
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public UserSystemRoleAccessorFake()
        {
            _userSystemRoles = new List<UserSystemRole>();

            _userSystemRoles.Add(new UserSystemRole()
            {
                UserID = 101,
                SystemRoleID = "Driver"
            });
        }


        // <summary>
        /// Ellie Wacker
        /// Created: 2025/03/05
        /// 
        /// This method shows validation for adding a UserSystemRole that can be checked in the UserSystemRoleManagerTests
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int AddUserSystemRole(int userID, string systemRoleID)
        {
            var _userSystemRole = new UserSystemRole()
            {
                UserID = userID,
                SystemRoleID = systemRoleID
            };

            _userSystemRole.UserID = _userSystemRoles.Count + 1;

            _userSystemRoles.Add(_userSystemRole);

            if (_userSystemRole.UserID > 0)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException("Unable to insert UserSystemRole");
            }
        }

        public int DeleteUserSystemRole(int userID, string systemRoleID)
        {
            throw new NotImplementedException();
        }


        // <summary>
        /// Ellie Wacker
        /// Created: 2025/03/06
        /// 
        /// This method shows validation for selecting all UserSystemRoles that can be checked in the UserSystemRoleManagerTests
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<UserSystemRole> SelectUserSystemRolesByUserID(int userID)
        {
            List<UserSystemRole> userSystemRoles = new List<UserSystemRole>();
            foreach (var userSystemRole in userSystemRoles)
            {
                if (userSystemRole.UserID == userID)
                {
                    userSystemRoles.Add(userSystemRole);
                }
            }
            if (userID == 0)
            {
                throw new ArgumentException("UserSystemRole record not found");
            }
            return userSystemRoles;
        }
    }
}
