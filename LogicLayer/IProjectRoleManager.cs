/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/11
/// 
/// The interface for manager to ProjectRole
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
    public interface IProjectRoleManager
    {
        public List<ProjectRole> GetAllProjectRoles();
    }
}
