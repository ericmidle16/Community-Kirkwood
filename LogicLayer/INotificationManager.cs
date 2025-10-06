/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the Inotification class that is use to interface the notification manager class.
/// It make connection to the database and need to be test.
/// this the code that is test when notification loagic layer test class is runs.
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface INotificationManager
    {
        public bool InsertNotification(Notification notification);
        public List<Notification> RetriveAllNotifications();
        // Author: Brodie Pasker
        public int ScheduleNotification(NotificationVM notification, List<int> recipients);
        // Author: Brodie Pasker
        public List<NotificationVM> GetAllNotificationsByUserID(int UserID);
        // Author: Brodie Pasker
        public bool MarkNotificationAsViewed(int notificationID);
        // Author: Brodie Pasker
        public List<NotificationVM> GetAllSenderNotificationsByUserID(int UserID);
    }
}
