/// <summary>
/// Ellie Wacker
/// Created: 2025-02-09
/// 
/// Interface that holds method declarations for accessing VolunteerStatusProjectRole data.
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IVolunteerStatusProjectRoleAccessor
    {
        int DeleteUserRoles(int userID, int projectID);
        int AddUserRoles(int userID, int projectID, string projectRoleID);
        List<VolunteerStatusProjectRole> SelectUserProjectRolesByUserIDProjectID(int userID, int projectID);
        int DeleteUserProjectRoleByUserIDProjectID(int userID, int projectID, string projectRoleID);
    }
}