/// <summary>
/// Creator: Nikolas Bell
/// Created: 2025-03-14
/// Summary: Represents a forum post with content, user and forum information,  
/// optional reply reference, and status indicators like pinned and edited.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-31
/// What Was Changed: Added comment, ProjectID, and ThreadName
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-04
/// What Was Changed: Removed ThreadName
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int ThreadID { get; set; }
        public bool Reply { get; set; }
        public bool Edited { get; set; }
        public bool Pinned { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }
        public int ProjectID { get; set; }


    }
    public class PostVM : Post
    {
        public string GivenName { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;
        public string FullName => $"{GivenName} {FamilyName}";

        public PostVM(int postID, int userID, int threadID, bool reply, bool edited, bool pinned,
            string content, DateTime datePosted, string givenName, string familyName)
        {
            PostID = postID;
            UserID = userID;
            ThreadID = threadID;
            Reply = reply;
            Edited = edited;
            Pinned = pinned;
            Content = content;
            DatePosted = datePosted;
            GivenName = givenName;
            FamilyName = familyName;
        }
    }
}
