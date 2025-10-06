/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/04/18
/// Summary: Defines the IForumPermissionAccessor interface, 
/// which includes method for selecting a user's write access.
/// 
/// Updated By: Syler Bushlack
/// Updated: 2025-04-24
/// What Was Changed: Added UpdateForumPermissionWriteAccessValue and SelectForumPermissionByProjectID method
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: Added the InsertForumPermission method.
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IForumPermissionAccessor
    {
        bool SelectUserWriteAccess(int userID, int projectID);
        int UpdateForumPermissionWriteAccessValue(ForumPermission forumPermission);
        List<ForumPermissionVM> SelectForumPermissionByProjectID(int projectID);
        int InsertForumPermission(ForumPermission forumPermission);
    }
}