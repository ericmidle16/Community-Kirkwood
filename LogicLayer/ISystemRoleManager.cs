/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/06
/// 
/// The interface for manager to SystemRole
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ISystemRoleManager
    {
        public List<SystemRole> GetAllSystemRoles();
    }
}
