/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a Test Method that Test the Request a volunteer 
/// it create a fake data to be test
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class NotificationAccessorFake : INotificationAccessor
    {
        private List<Notification> _notification;
        private List<NotificationVM> _notifications = new List<NotificationVM>();

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Construction of Notification classes and set the value of each field in the notification that 
        /// need to be be tested
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public NotificationAccessorFake()
        {
            //Creating a fake data to be test for the notification logic layer test.
            _notification = new List<Notification>();
            _notification.Add(new Notification()
            {
                NotificationID = 1,
                Name = "Test",
                Sender = 100,
                Receiver = 1002,
                Important = true,
                IsViewed = true,
                Date = DateTime.Now,
                Content = "Test",
            });
            _notification.Add(new Notification()
            {
                NotificationID = 2,
                Name = "Test1",
                Sender = 1001,
                Receiver = 1003,
                Important = false,
                IsViewed = true,
                Date = DateTime.Now,
                Content = "Test2",
            });
            _notification.Add(new Notification()
            {
                NotificationID = 3,
                Name = "Test2",
                Sender = 1003,
                Receiver = 1004,
                Important = true,
                IsViewed = false,
                Date = DateTime.Now,
                Content = "Test3",
            });
            // Version 1: Future-dated unread notification with different sender/receiver
            _notifications.Add(new NotificationVM()
            {
                NotificationID = 4,
                Name = "Meeting Reminder",
                Sender = 2001,
                Receiver = 3001,
                Important = true,
                IsViewed = false,
                Date = DateTime.Now.AddDays(-1),
                Content = "Quarterly review meeting tomorrow",
                GivenNameSender = "Sarah",
                FamilyNameSender = "Connor",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2)
            });

            // Version 2: Expired unimportant notification
            _notifications.Add(new NotificationVM()
            {
                NotificationID = 5,
                Name = "Lunch Invite",
                Sender = null,
                Receiver = 1002,
                Important = false,
                IsViewed = true,
                Date = DateTime.Now.AddDays(-7),
                Content = "Team lunch at 1PM",
                GivenNameSender = "Mike",
                FamilyNameSender = "Johnson",
                StartDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now.AddDays(-5)
            });

            // Version 3: High-priority system notification
            _notifications.Add(new NotificationVM()
            {
                NotificationID = 6,
                Name = "SYSTEM ALERT",
                Sender = null,  // System user
                Receiver = 1002,
                Important = true,
                IsViewed = false,
                Date = DateTime.Now,
                Content = "Security update required",
                GivenNameSender = "System",
                FamilyNameSender = "Admin",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(24)
            });

            // Version 4: Long-duration recurring notification
            _notifications.Add(new NotificationVM()
            {
                NotificationID = 7,
                Name = "Weekly Report",
                Sender = 3000,
                Receiver = 4000,
                Important = false,
                IsViewed = true,
                Date = DateTime.Now.AddDays(-3),
                Content = "Please submit your weekly progress report",
                GivenNameSender = "Emily",
                FamilyNameSender = "Davis",
                StartDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now.AddMonths(6)
            });

            // Version 5: Multi-receiver broadcast message
            _notifications.Add(new NotificationVM()
            {
                NotificationID = 8,
                Name = "Company Update",
                Sender = 5000,
                Receiver = 9999,  // Special broadcast ID
                Important = true,
                IsViewed = false,
                Date = DateTime.Now,
                Content = "All-hands meeting Friday",
                GivenNameSender = "CEO",
                FamilyNameSender = "Smith",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            });

            // Version 6: Edge case - Very long content
            _notifications.Add(new NotificationVM()
            {
                NotificationID = 9,
                Name = "Policy Update",
                Sender = 5000,
                Receiver = 7002,
                Important = true,
                IsViewed = false,
                Date = DateTime.Now,
                Content = new string('A', 500),  // 500-character message
                GivenNameSender = "Legal",
                FamilyNameSender = "Team",
                StartDate = DateTime.Now.AddMinutes(30),
                EndDate = DateTime.Now.AddYears(1)
            });
        }


        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/0/
        /// Summary:Send notification to all volunteers.
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public List<Notification> GetNotifications()
        {
            return _notification;

        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: this code added new request that will be send to volunteers and 
        /// returns 1 for true and returns 0 false.
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int InsertNotification(Notification notification)
        {
            // this code added new request that will be send to volunteers
            int expectLenght = _notification.Count +1;
            _notification.Add(notification);
            if((expectLenght == _notification.Count)){
                return 1;
            }
            else
            {
                return 0;
            }   
        }

        public List<NotificationVM> GetAllNotificationsByUserID(int UserID)
        {
            return _notifications
                .Where(n => n.Receiver == UserID)
                .ToList();
        }

        public int InsertScheduledNotification(NotificationVM notification, List<int> ReceiverIDs)
        {
            int _nextId = 1;
            foreach (int receiver in ReceiverIDs)
            {
                _notifications.Add(new NotificationVM
                {
                    NotificationID = _nextId++,
                    Receiver = receiver,
                    Name = notification.Name,
                    Sender = notification.Sender,
                    Important = notification.Important,
                    Content = notification.Content,
                    StartDate = notification.StartDate,
                    EndDate = notification.EndDate,
                    Date = DateTime.Now
                });
            }
            return ReceiverIDs.Count;
        }

        public int UpdateNotificationToIsViewed(int NotificationID)
        {
            NotificationVM note = _notifications.FirstOrDefault(n => n.NotificationID == NotificationID);
            if (note != null)
            {
                note.IsViewed = true;
                return 1;
            }
            return 0;
        }


        public List<NotificationVM> SelectNotificationsBySenderUserID(int UserID)
        {
            List<NotificationVM> nvm = new List<NotificationVM>();
            foreach (NotificationVM n in _notifications)
            {
                if (n.Sender == UserID)
                {
                    nvm.Add(n);
                }
            }
            return nvm;
        }
    }
}
