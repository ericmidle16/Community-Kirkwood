/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/06
/// 
/// The manager to SystemRole that uses methods
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
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
    public class SystemRoleManager
    {
        private ISystemRoleAccessor _systemRoleAccessor;

        public SystemRoleManager()
        {
            _systemRoleAccessor = new SystemRoleAccessor();
        }

        public SystemRoleManager(ISystemRoleAccessor systemRoleAccessor)
        {
            _systemRoleAccessor = systemRoleAccessor;
        }

        /// <summary>
        /// Eric Idle
        /// Created: 2025/02/06
        /// 
        /// This is to grab all System Roles from the system role accessor
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public List<SystemRole> GetAllSystemRoles()
        {
            List<SystemRole> sysroles = null;

            try
            {
                sysroles = _systemRoleAccessor.SelectAllSystemRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data no available", ex);
            }
            return sysroles;
        }
    }
}
