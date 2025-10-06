/// <summary>
/// Created By: Jackson Manternach
/// Created: 2025/03/14
/// 
/// Data access fake for the ViewProjectForum feature
/// </summary>
/// 
/// <remarks>
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-04
/// What Changed: Added InsertForumPost and GetLastAddedPost
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-21
/// What Changed: Renamed class to ThreadAccessorFake
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class ThreadAccessorFake : IThreadAccessor
    {
        private List<ThreadVM> _threads;
        private ThreadVM _lastAddedPost;

        /// <summary>
        /// Created By: Jackson Manternach
        /// Created: 2025/03/14
        /// 
        /// Creates data fakes for the ViewProjectForum test methods
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Updated:
        /// </remarks>
        public ThreadAccessorFake()
        {
            // Initialize the list
            _threads = new List<ThreadVM>();

            // Add sample thread data
            _threads.Add(new ThreadVM()
            {
                ThreadName = "the first sample",
                UserID = 1,
                GivenName = "John",
                FamilyName = "Smith",
                DatePosted = DateTime.Parse("2025-03-01 10:30:00"),
                ProjectID = 1
            });

            _threads.Add(new ThreadVM()
            {
                ThreadName = "another sample",
                UserID = 2,
                GivenName = "someones",
                FamilyName = "name",
                DatePosted = DateTime.Parse("2025-03-22 08:30:00"),
                ProjectID = 2
            });

            _threads.Add(new ThreadVM()
            {
                ThreadName = "the third sample",
                UserID = 3,
                GivenName = "another",
                FamilyName = "naming",
                DatePosted = DateTime.Parse("2025-04-11 08:30:00"),
                ProjectID = 3
            });
        }

        /// <summary>
        /// Created By: Jackson Manternach
        /// Created: 2025/03/13
        /// 
        /// Method that selects the list of threads by a project id
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Updated:
        /// </remarks>
        /// <param name="projectID">Reads in a project id to find the threads within a project</param>
        public List<ThreadVM> SelectThreadByProjectID(int projectID)
        {
            var threads = _threads.Where(t => t.ProjectID == projectID).ToList();

            if (threads.Count == 0)
            {
                throw new ApplicationException("Thread not found");
            }

            return threads;
        }


        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: Inserts a forum post for a given project and user. 
        /// Validates input values, ensuring the content is not empty and does not exceed character limits as 
        /// well as the thread name.
        /// Throws an exception if any validation
        /// fails or if a post with the same content exists for the user in the forum.
        /// </summary>
        public int InsertForumPost(int userID, string content, int projectID, string threadName, DateTime datePosted)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ApplicationException("Please enter content for your post.");
            }

            if (content.Length > 250)
            {
                throw new ApplicationException("Your post cannot exceed 250 characters.");
            }

            if (string.IsNullOrWhiteSpace(threadName))
            {
                throw new ApplicationException("Please enter a thread name for your post.");
            }

            if (threadName.Length > 100)
            {
                throw new ApplicationException("Your post cannot exceed 100 characters.");
            }

            var newPost = new ThreadVM
            {
                UserID = userID,
                ProjectID = projectID,
                ThreadName = threadName,
                DatePosted = datePosted
            };

            _threads.Add(newPost);
            _lastAddedPost = newPost; // Save the last inserted post for testing

            return 1; // Simulate success by returning 1 to indicate one row affected
        }


        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: Retrieves the last added forum post record.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public ThreadVM GetLastAddedPost()
        {
            return _lastAddedPost;
        }


    }
}
