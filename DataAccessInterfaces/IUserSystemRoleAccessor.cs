/// <summary>
/// Ellie Wacker
/// Created: 2025-03-05
/// 
/// Interface that holds method declarations for accessing Project data.
/// </summary>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IUserSystemRoleAccessor
    {
        int AddUserSystemRole(int userID, string systemRoleID);
        List<UserSystemRole> SelectUserSystemRolesByUserID(int userID);
        int DeleteUserSystemRole(int userID, string systemRoleID);
    }
}
