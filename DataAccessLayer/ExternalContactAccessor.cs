/// <summary>
/// Creator:  Jacob McPherson
/// Created:  2025/02/18
/// Summary:  Class for Accessing ExternalContact Data
/// Last Updated By: Jacob McPherson
/// Last Updated: Merge
/// What was Changed: 	2025/03/25
/// </summary>

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
    public class ExternalContactAccessor : IExternalContactAccessor
    {

        public List<ExternalContact> SelectAllExternalContacts()
        {
            List<ExternalContact> externalContacts = new List<ExternalContact>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_view_all_external_contacts", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    externalContacts.Add(new ExternalContact()
                    {
                        ExternalContactID = reader.GetInt32(0),
                        ExternalContactTypeID = reader.GetString(1),
                        ContactName = reader.GetString(2),
                        FamilyName = reader.IsDBNull(3) ? null : reader.GetString(3),
                        GivenName = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                        PhoneNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                        JobTitle = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Active = true
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

            return externalContacts;
        }

        public ExternalContactVM SelectSingleExternalContact(int externalContactID)
        {
            ExternalContactVM externalContact = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_view_single_external_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ExternalContactID", SqlDbType.Int);
            cmd.Parameters["@ExternalContactID"].Value = externalContactID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    externalContact = new ExternalContactVM()
                    {
                        ExternalContactID = externalContactID,
                        ExternalContactTypeID = reader.GetString(0),
                        TypeDescription = reader.GetString(1),
                        ContactName = reader.GetString(2),
                        FamilyName = reader.IsDBNull(3) ? null : reader.GetString(3),
                        GivenName = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                        PhoneNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                        JobTitle = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Address = reader.IsDBNull(8) ? null : reader.GetString(8),
                        Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                        Active = true
                    };
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


            return externalContact;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/03/04
        /// 
        /// Updates External Contact
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="currentUserId">Id of Current User</param> 
        /// <param name="externalContactNew">New Contact Information</param>
        /// <param name="externalContactOld"> Old Contact Information</param>
        public bool UpdateExternalContact(int currentUserId, ExternalContact externalContactNew, ExternalContact externalContactOld)
        {
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_update_external_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ExternalContactID", SqlDbType.Int);
            cmd.Parameters.Add("@ExternalContactTypeID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar);
            cmd.Parameters.Add("@JobTitle", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ExternalContactTypeID_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ContactName_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@GivenName_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@FamilyName_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Email_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PhoneNumber_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@JobTitle_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Address_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description_Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters[0].Value = externalContactNew.ExternalContactID;
            cmd.Parameters[1].Value = externalContactNew.ExternalContactTypeID;
            cmd.Parameters[2].Value = externalContactNew.ContactName;
            cmd.Parameters[3].Value = externalContactNew.GivenName;
            cmd.Parameters[4].Value = externalContactNew.FamilyName;
            cmd.Parameters[5].Value = externalContactNew.Email;
            cmd.Parameters[6].Value = externalContactNew.PhoneNumber;
            cmd.Parameters[7].Value = externalContactNew.JobTitle;
            cmd.Parameters[8].Value = externalContactNew.Address;
            cmd.Parameters[9].Value = externalContactNew.Description;
            cmd.Parameters[10].Value = externalContactOld.ExternalContactTypeID;
            cmd.Parameters[11].Value = externalContactOld.ContactName;
            cmd.Parameters[12].Value = externalContactOld.GivenName;
            cmd.Parameters[13].Value = externalContactOld.FamilyName;
            cmd.Parameters[14].Value = externalContactOld.Email;
            cmd.Parameters[15].Value = externalContactOld.PhoneNumber;
            cmd.Parameters[16].Value = externalContactOld.JobTitle;
            cmd.Parameters[17].Value = externalContactOld.Address;
            cmd.Parameters[18].Value = externalContactOld.Description;
            cmd.Parameters[19].Value = currentUserId;

            int rows = 0;

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows == 1;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/18
        /// 
        /// Inserts ExternalContact to DB
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="externalContact">Data to Insert</param>
        public bool InsertExternalContact(ExternalContact externalContact)
        {
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_insert_external_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ExternalContactTypeID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar);
            cmd.Parameters.Add("@JobTitle", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int);

            cmd.Parameters[0].Value = externalContact.ExternalContactTypeID;
            cmd.Parameters[1].Value = externalContact.ContactName;
            cmd.Parameters[2].Value = externalContact.GivenName;
            cmd.Parameters[3].Value = externalContact.FamilyName;
            cmd.Parameters[4].Value = externalContact.Email;
            cmd.Parameters[5].Value = externalContact.PhoneNumber;
            cmd.Parameters[6].Value = externalContact.JobTitle;
            cmd.Parameters[7].Value = externalContact.Address;
            cmd.Parameters[8].Value = externalContact.Description;
            cmd.Parameters[9].Value = externalContact.AddedBy;

            int rows = 0;

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows == 1;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/25
        /// 
        /// Inserts ExternalContactType to DB
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="name">Contact Type Name</param>
        /// <param name="description">Contact Type Description</param>
        public bool InsertExternalContactType(string name, string description)
        {
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_insert_external_contact_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ExternalContactTypeID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = name;
            cmd.Parameters[1].Value = description;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return true;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/18
        /// 
        /// Retreives ExternalContactTypes from DB
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        public List<string> SelectAllExternalContactTypes()
        {
            List<string> contactTypes = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_contact_types", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contactTypes.Add(reader.GetString(0));
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

            return contactTypes;
        }

        public bool DeactivateExternalContact(int contactId)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_external_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ExternalContactID", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = contactId;

            int rows;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows > 0;
        }
    }
}
