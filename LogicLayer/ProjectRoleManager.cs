/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/11
/// 
/// The manager to ProjectRole that uses methods
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
    public class ProjectRoleManager : IProjectRoleManager
    {
        private IProjectRoleAccessor _projectRoleAccessor;

        public ProjectRoleManager()
        {
            _projectRoleAccessor = new ProjectRoleAccessor();
        }

        public ProjectRoleManager(IProjectRoleAccessor projectRoleAccessor)
        {
            _projectRoleAccessor = projectRoleAccessor;
        }

        public List<ProjectRole> GetAllProjectRoles()
        {
            List<ProjectRole> projectRoles = null;

            try
            {
                projectRoles = _projectRoleAccessor.SelectAllProjectRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data no available", ex);
            }
            return projectRoles;
        }
    }
}
