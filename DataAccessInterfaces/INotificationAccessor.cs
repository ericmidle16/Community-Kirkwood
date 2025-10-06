/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a Test Method that interface the notificationAccessor
/// it is use the test notification was submitted or not
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
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface INotificationAccessor
    {
        //this code the interface of the notification accessor
        public int InsertNotification( Notification notification );
        List<Notification> GetNotifications();
        // Author: Brodie Pasker
        public List<NotificationVM> GetAllNotificationsByUserID(int UserID);
        // Author: Brodie Pasker
        int InsertScheduledNotification(NotificationVM notification, List<int> ReceiverIDs);
        // Author: Brodie Pasker
        public int UpdateNotificationToIsViewed(int NotificationID);

        // Author: Brodie Pasker
        public List<NotificationVM> SelectNotificationsBySenderUserID(int UserID);
    }
}
