/// <summary>
/// Created By: Jackson Manternach
/// Created: 2025/03/13
/// 
/// Manager interface code for threads
/// </summary>
/// 
/// <remarks>
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-04
/// What Changed: Added InsertForumPost
/// </remarks>
/// 
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IThreadManager
    {
        public List<ThreadVM> GetThreadsByProjectID(int projectID);
        bool InsertForumPost(int userID, string content, int projectID, string threadName, DateTime datePosted);
    }
}
