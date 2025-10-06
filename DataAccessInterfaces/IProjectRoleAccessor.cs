/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/11
/// 
/// Interface for project roles accessor
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

namespace DataAccessInterfaces
{
    public interface IProjectRoleAccessor
    {
        List<ProjectRole> SelectAllProjectRoles();
    }
}
