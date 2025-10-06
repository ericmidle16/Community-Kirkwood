/// <summary>
/// Created By: Jackson Manternach
/// Created: 2025/03/13
/// 
/// Accessor interface code for threads
/// </summary>
/// 
/// <remarks>
/// Updater Name: Skyann Heintz
/// Updated: 2024-04-04
/// What Changed: Added InsertForumPost
/// </remarks>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IThreadAccessor
    {
        public List<ThreadVM> SelectThreadByProjectID(int projectID);

        int InsertForumPost(int userID, string content, int projectID, string threadName, DateTime datePosted);

    }
}
