/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// The method that connect to database, 
/// it is use to access the database and notification class 
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class NotificationAccessor : INotificationAccessor
    {
        /// <summary>
        /// Author: Brodie Pasker
        /// 
        /// Gets all Notifications for the reciever by UserID between the start date and end date
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<NotificationVM> GetAllNotificationsByUserID(int UserID)
        {
            List<NotificationVM> notifications = new List<NotificationVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_notifications_by_user_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID", UserID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    notifications.Add(new NotificationVM()
                    {
                        NotificationID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Sender = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                        GivenNameSender = reader.IsDBNull(3) ? "System" : reader.GetString(3),
                        FamilyNameSender = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        Receiver = reader.GetInt32(5),
                        Important = reader.GetBoolean(6),
                        IsViewed = reader.GetBoolean(7),
                        Date = reader.GetDateTime(8),
                        Content = reader.GetString(9)
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return notifications;
        }

        public List<Notification> GetNotifications()
        {
            List<Notification> notifications = new List<Notification>();
            //Make connection
            var ton = DBConnection.GetConnection();
            //The query to connection to the database
            var cmd = new SqlCommand("sp_select_all_notification", ton);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                //open connection
                ton.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Notification notification = new Notification();
                        notification.NotificationID = reader.GetInt32(0);
                        notification.Name = reader.GetString(1);
                        notification.Sender = reader.GetInt32(2);
                        notification.Receiver = reader.GetInt32(3);
                        notification.Important = reader.GetBoolean(4);
                        notification.IsViewed = reader.GetBoolean(5);
                        notification.Date = reader.GetDateTime(6);
                        notification.Content = reader.GetString(7);
                        notifications.Add(notification);
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ton.Close();
            }
            return notifications;
        }
        public int InsertNotification(Notification notification)
        {
            //The database connection, make connection to the database table
            int result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_notifications", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Sender", SqlDbType.Int);
            cmd.Parameters.Add("@Receiver", SqlDbType.Int);
            cmd.Parameters.Add("@Important", SqlDbType.Bit);
            cmd.Parameters.Add("@IsViewed", SqlDbType.Bit);
            cmd.Parameters.Add("@Date", SqlDbType.DateTime);
            cmd.Parameters.Add("@Content", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = notification.Name;
            cmd.Parameters["@Sender"].Value = notification.Sender;
            cmd.Parameters["@Receiver"].Value = notification.Receiver;
            cmd.Parameters["@Important"].Value = notification.Important;
            cmd.Parameters["@IsViewed"].Value = notification.IsViewed;
            cmd.Parameters["@Date"].Value = notification.Date;
            cmd.Parameters["@Content"].Value = notification.Content;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int InsertScheduledNotification(NotificationVM notification, List<int> ReceiverIDs)
        {
            int rowsAffected = 0;
            string ReceiverIDsList = "[" + string.Join(",", ReceiverIDs) + "]";

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_scheduled_notification", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Sender", SqlDbType.Int);
            cmd.Parameters.Add("@Receivers", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Important", SqlDbType.Bit);
            cmd.Parameters.Add("@Content", SqlDbType.NVarChar);
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
            cmd.Parameters["@Name"].Value = notification.Name;
            cmd.Parameters["@Sender"].Value = notification.Sender;
            cmd.Parameters["@Receivers"].Value = ReceiverIDsList;
            cmd.Parameters["@Important"].Value = notification.Important;
            cmd.Parameters["@Content"].Value = notification.Content;
            cmd.Parameters["@StartDate"].Value = notification.StartDate;
            cmd.Parameters["@EndDate"].Value = notification.EndDate;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;

        }

        /// <summary>
        /// Author: Brodie Pasker
        /// 
        /// Gets all notifications sent by a specific UserID
        /// </summary>
        /// <param name="UserID">Sender UserID to search for</param>
        /// <returns>List of NotificationVMs</returns>
        public List<NotificationVM> SelectNotificationsBySenderUserID(int UserID)
        {
            List<NotificationVM> notifications = new List<NotificationVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_notifications_by_sender_userid", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = UserID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    notifications.Add(new NotificationVM()
                    {
                        NotificationID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Sender = reader.GetInt32(2),
                        Receiver = reader.GetInt32(3),
                        Important = reader.GetBoolean(4),
                        Content = reader.GetString(5),
                        Date = reader.GetDateTime(6),
                        StartDate = reader.GetDateTime(7),
                        EndDate = reader.GetDateTime(8)
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return notifications;
        }


        /// <summary>
        /// Author: Brodie Pasker
        /// 
        /// Updates a notification's Viewed status by NotificationID
        /// </summary>
        /// <param name="NotificationID">ID of notification to update</param>
        /// <returns>Number of rows affected</returns>
        public int UpdateNotificationToIsViewed(int NotificationID)
        {
            int result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_notification_to_viewed_by_notification_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NotificationID", SqlDbType.Int);
            cmd.Parameters["@NotificationID"].Value = NotificationID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
