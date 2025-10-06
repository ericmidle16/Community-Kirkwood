/// <summary>
/// Created By: Jackson Manternach
/// Created: 2025/03/13
/// 
/// Manager code for threads
/// </summary>
/// 
/// <remarks>
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-04
/// What Changed: Added InsertForumPost
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

    public class ThreadManager : IThreadManager
    {
        private IThreadAccessor _threadAccessor;

        public ThreadManager(IThreadAccessor threadAccessor)
        {
            _threadAccessor = threadAccessor;
        }

        public ThreadManager()
        {
            _threadAccessor = new ThreadAccessor();
        }

        public List<ThreadVM> GetThreadsByProjectID(int projectID)
        {
            List<ThreadVM> threads = null;
            try
            {
                threads = _threadAccessor.SelectThreadByProjectID(projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving threads", ex);
            }
            return threads;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: Inserts a new forum post record for a user into the database.
        /// Returns true if the post was successfully added, otherwise false.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool InsertForumPost(int userID, string content, int projectID, string threadName, DateTime datePosted)
        {
            try
            {
                int rowsAffected = _threadAccessor.InsertForumPost(userID, content, projectID, threadName, datePosted);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Post insertion failed.", ex);
            }
        }
    }
}