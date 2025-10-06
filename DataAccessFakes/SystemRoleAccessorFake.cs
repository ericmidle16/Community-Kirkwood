/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/06
/// 
/// Class accessor for fake SystemRole data
/// 
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
    public class SystemRoleAccessorFake : ISystemRoleAccessor
    {
        private List<SystemRole> _systemRoles;
        public SystemRoleAccessorFake() 
        {
            _systemRoles = new List<SystemRole>();
            _systemRoles.Add(new SystemRole()
            {
                SystemRoleID = "Admin",
                Description= "Administrator for the system."
            });
            _systemRoles.Add(new SystemRole()
            {
                SystemRoleID = "User",
                Description = "A base user to be able to access core functions."
            });
        }

        public List<SystemRole> GetSystemRolesByUserID(int userId)
        {
            throw new NotImplementedException();
        }

        public List<SystemRole> SelectAllSystemRoles()
        {
            return _systemRoles;
        }

        public List<SystemRole> SelectSystemRolesByUserID(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
