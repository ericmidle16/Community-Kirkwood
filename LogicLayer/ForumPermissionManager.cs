/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/04/18
/// Summary: This ForumPermissionManager class implements IForumPermissionAccessor
/// and handles forum permission related operations. It interacts with the
/// ForumPermissionAccessor to check if a user has write access to a project.
/// 
/// Updated By: Syler Bushlack
/// Updated: 2025-04-24
/// What Was Changed: Added EditForumPermissionWriteAccessValue and GettForumPermissionsByProjectID method
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: Added the AddForumPermission method.
/// </summary>

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
    public class ForumPermissionManager : IForumPermissionManager
    {
        private IForumPermissionAccessor _forumPermissionAccessor;

        public ForumPermissionManager(IForumPermissionAccessor forumPermissionAccessor)
        {
            _forumPermissionAccessor = forumPermissionAccessor;
        }

        public ForumPermissionManager()
        {
            _forumPermissionAccessor = new ForumPermissionAccessor();
        }

        // Author: Kate Rich
        public bool AddForumPermission(ForumPermission forumPermission)
        {
            bool added = false;

            int rowsAffected = 0;

            try
            {
                rowsAffected = _forumPermissionAccessor.InsertForumPermission(forumPermission);
                if(rowsAffected == 1)
                {
                    added = true;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Forum Permission Creation Failed...", ex);
            }

            return added;
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/04/18
        /// Summary: Determines whether the specified user has write access to the given project forum.  
        /// Calls the data access layer to verify permissions and handles any exceptions that may occur.   
        /// Last Upaded By:
        /// Last Updated: 
        /// What Was Changed: 
        /// </summary>
        public bool SelectUserWriteAccess(int userID, int projectID)
        {
            try
            {
                return _forumPermissionAccessor.SelectUserWriteAccess(userID, projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error checking user write access.", ex);
            }
        }

        // Author: Syler Bushlack
        public bool EditForumPermissionWriteAccessValue(ForumPermission forumPermission)
        {
            try
            {
                int rowsAffected = _forumPermissionAccessor.UpdateForumPermissionWriteAccessValue(forumPermission);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Updating user post permissions failed.", ex);
            }
        }

        // Author: Syler Bushlack
        public List<ForumPermissionVM> GettForumPermissionsByProjectID(int projectID)
        {
            List<ForumPermissionVM> forumPermissions = null;

            try
            {
                forumPermissions = _forumPermissionAccessor.SelectForumPermissionByProjectID(projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return forumPermissions;
        }
    }
}