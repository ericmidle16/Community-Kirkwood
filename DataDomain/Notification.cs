/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the notification class that is for notification that  need to be send
/// It make connection to notification table in the database.
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Notification
    {
        //this the notification class
        public int NotificationID { get; set; }
        public string Name { get; set; }
        public int? Sender {  get; set; }
        public int Receiver {  get; set; }
        public bool Important { get; set; }
        public bool IsViewed { get; set; }
        public DateTime Date { get; set; }  
        public string Content { get; set; }
    }
    public class NotificationVM : Notification
    {
        public string? GivenNameSender { get; set; }
        public string? FamilyNameSender { get; set; }
        public string? DisplayDateString { get; set; }
        public string FullSenderName => $"{GivenNameSender} {FamilyNameSender}";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
