/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the notification manager method that access data from the inotification method.
/// It make connection to the Logic layer test and data access fakes.
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using DataAccessFakes;
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
    // The notification interface method been conected
    public class NotificationManager : INotificationManager
    {
        INotificationAccessor _notificationAccessor;
        //the notification manager to access the notification acccessor method.
        public NotificationManager()
        {
            _notificationAccessor = new NotificationAccessor();
        }
        // the notificatin manager access the notification accessor fake.
        public NotificationManager(NotificationAccessorFake notificationAccessFake)
        {
            _notificationAccessor = notificationAccessFake;

        }
        //Author: Akoi Kollie
        //The insert notification method been test.
        public bool InsertNotification(Notification notification)
        {
            //checking if the notification is submitted
            bool results = false;
            try
            {
                results = (1  == _notificationAccessor.InsertNotification(notification));

            }
            catch (Exception ex)
            {
                //throw an exception
                throw new ArgumentException("Insert user failed", ex);

            }
            return results;
        }
        public List<Notification> RetriveAllNotifications()
        {
            List<Notification> notifications = null;
            try
            {
                notifications = _notificationAccessor.GetNotifications();

            }
            catch (Exception ex)
            {
                throw new ArgumentException("All these all the notifications");
            }
            return notifications;

        }
        public List<NotificationVM> GetAllNotificationsByUserID(int UserID)
        {
            List<NotificationVM> notifications = null;
            try
            {
                notifications = _notificationAccessor.GetAllNotificationsByUserID(UserID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Notification Retrieval Failed", ex);
            }
            return notifications;
        }

        public bool MarkNotificationAsViewed(int notificationID)
        {
            bool result = false;
            try
            {
                result = (1 == _notificationAccessor.UpdateNotificationToIsViewed(notificationID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not mark notification as viewed", ex);
            }
            return result;
        }

        public int ScheduleNotification(NotificationVM notification, List<int> recipients)
        {
            int notificationsSent = 0;
            try
            {
                notificationsSent = _notificationAccessor.InsertScheduledNotification(notification, recipients);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not send notifications", ex);
            }
            return notificationsSent;
        }

        public List<NotificationVM> GetAllSenderNotificationsByUserID(int UserID)
        {
            List<NotificationVM> senderNotifications = new List<NotificationVM>();
            try
            {
                senderNotifications = _notificationAccessor.SelectNotificationsBySenderUserID(UserID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not retrieve any notifications for user", ex);
            }
            return senderNotifications;
        }
    }
}
