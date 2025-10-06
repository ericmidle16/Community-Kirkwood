/// <summary>
/// Creator: Yousif Omer
/// Created: 2025/02/14
/// Summary:
///     This class is created to hold the connection on the Accessor class
/// </summary>
///
/// <remarks>
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Updated the UpdateEvent() method to include the @OldEventType & @NewEventType
///     parameters for the stored procedure.
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;


namespace DataAccessLayer
{
    public class EventAccessor : IEventAccessor
    {
        public int UpdateEvent(Event oldEvent, Event newEvent)
        {
            int rows = 0;

            // connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_update_event", conn);
            // type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters

            cmd.Parameters.AddWithValue("@EventID", oldEvent.EventID);

            cmd.Parameters.AddWithValue("@OldDateCreated", oldEvent.DateCreated);
            cmd.Parameters.AddWithValue("@OldStartDate", oldEvent.StartDate);
            cmd.Parameters.AddWithValue("@OldEndDate", oldEvent.EndDate);
            cmd.Parameters.AddWithValue("@OldName", oldEvent.Name);
            cmd.Parameters.AddWithValue("@OldVolunteersNeeded", oldEvent.VolunteersNeeded);
            cmd.Parameters.AddWithValue("@OldNotes", oldEvent.Notes);
            cmd.Parameters.AddWithValue("@OldDescription", oldEvent.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldEvent.Active);

            cmd.Parameters.AddWithValue("@NewDateCreated", newEvent.DateCreated);
            cmd.Parameters.AddWithValue("@NewStartDate", newEvent.StartDate);
            cmd.Parameters.AddWithValue("@NewEndDate", newEvent.EndDate);
            cmd.Parameters.AddWithValue("@NewName", newEvent.Name);
            cmd.Parameters.AddWithValue("@NewVolunteersNeeded", newEvent.VolunteersNeeded);
            cmd.Parameters.AddWithValue("@NewNotes", newEvent.Notes);
            cmd.Parameters.AddWithValue("@NewDescription", newEvent.Description);
            cmd.Parameters.AddWithValue("@NewActive", newEvent.Active);

            cmd.Parameters.AddWithValue("@OldEventTypeID", oldEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@NewEventTypeID", newEvent.EventTypeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ApplicationException("record not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public List<Event> GetEvents(bool active = true)
        {
            List<Event> events = new List<Event>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_event_list");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var Anevent = new Event();
                        Anevent.EventID = reader.GetInt32(0);
                        Anevent.EventTypeID = reader.GetString(1);
                        Anevent.ProjectID = reader.GetInt32(2);
                        Anevent.DateCreated = reader.GetDateTime(3);
                        Anevent.StartDate = reader.GetDateTime(4);
                        Anevent.EndDate = reader.GetDateTime(5);
                        Anevent.Name = reader.GetString(6);
                        Anevent.LocationID = reader.GetInt32(7);
                        Anevent.VolunteersNeeded = reader.GetInt32(8);
                        Anevent.UserID = reader.GetInt32(9);
                        Anevent.Notes = reader.GetString(10);
                        Anevent.Description = reader.GetString(11);
                        Anevent.Active = reader.GetBoolean(12);




                        events.Add(Anevent);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw new ApplicationException("Can't read from the datatbase!");
            }
            finally
            {
                conn.Close();
            }
            return events;
        }

        public int DeactivateEventById(int ID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eventID", ID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public Event SelectEventByID(int eventId)
        {
            Event _event = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_single_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventId", eventId);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    _event = new Event()
                    {
                        EventID = reader.GetInt32(0),
                        EventTypeID = reader.GetString(1),
                        ProjectID = reader.GetInt32(2),
                        DateCreated = reader.GetDateTime(3),
                        StartDate = reader.GetDateTime(4),
                        EndDate = reader.GetDateTime(5),
                        Name = reader.GetString(6),
                        LocationID = reader.GetInt32(7),
                        VolunteersNeeded = reader.GetInt32(8),
                        UserID = reader.GetInt32(9),
                        Notes = reader.GetString(10),
                        Description = reader.GetString(11),
                        Active = reader.GetBoolean(12),

                    };
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { 

                conn.Close();
            }

            return _event;
        }

        public List<Event> GetEventsByProjectID(int projectID)
        {
            List<Event> events = new List<Event>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_event_list_by_project_id");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProjectID", projectID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var Anevent = new Event();
                        Anevent.EventID = reader.GetInt32(0);
                        Anevent.EventTypeID = reader.GetString(1);
                        Anevent.ProjectID = reader.GetInt32(2);
                        Anevent.DateCreated = reader.GetDateTime(3);
                        Anevent.StartDate = reader.GetDateTime(4);
                        Anevent.EndDate = reader.GetDateTime(5);
                        Anevent.Name = reader.GetString(6);
                        Anevent.LocationID = reader.GetInt32(7);
                        Anevent.VolunteersNeeded = reader.GetInt32(8);
                        Anevent.UserID = reader.GetInt32(9);
                        Anevent.Notes = reader.GetString(10);
                        Anevent.Description = reader.GetString(11);
                        Anevent.Active = reader.GetBoolean(12);

                        events.Add(Anevent);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw new ApplicationException("Can't read from the datatbase!");
            }
            finally
            {
                conn.Close();
            }
            return events;
        }

        public List<Event> GetEventsByApprovedUserID(int UserID)
        {
            List<Event> events = new List<Event>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_events_on_projects_user_approved");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        events.Add(new Event()
                        {
                            EventID = reader.GetInt32(0),
                            DateCreated = reader.GetDateTime(1),
                            StartDate = reader.GetDateTime(2),
                            EndDate = reader.GetDateTime(3),
                            Name = reader.GetString(4)
                        });
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw new ApplicationException("Can't read from the datatbase!");
            }
            finally
            {
                conn.Close();
            }
            return events;
        }

        public List<EventType> GetEventTypes()
        {
            List<EventType> eventTypes = new List<EventType>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_eventtypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EventType eventType = new EventType();
                    eventType.EventTypeID = reader.GetString(0);
                    eventTypes.Add(eventType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return eventTypes;
        }

        public int InsertEvent(Event newEvent)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Clear();

            cmd.Parameters.Add("@EventTypeID", SqlDbType.NVarChar, 50).Value = newEvent.EventTypeID;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = newEvent.ProjectID;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = newEvent.StartDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = newEvent.EndDate;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = newEvent.Name;
            cmd.Parameters.Add("@LocationID", SqlDbType.Int).Value = newEvent.LocationID;
            cmd.Parameters.Add("@VolunteersNeeded", SqlDbType.Int).Value = newEvent.VolunteersNeeded;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = newEvent.UserID;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 250).Value = newEvent.Notes;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value = newEvent.Description;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}