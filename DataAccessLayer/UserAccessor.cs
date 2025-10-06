/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/02
/// Summary:Implements the IUserAccessor 
/// interface to manage user-related data operations,
/// such as inserting a new user into the 
/// database and checking if a user exists already
/// with that email.
/// Last Updated By: Kate Rich
/// Last Updated: 2025-02-11
/// What Was Changed:
///		Class that implements the IUserAccessor Interface - used for
/// 	accessing User data from the DB.
/// Last Updated By: Brodie Pasker
/// Last Updated: 2025-03-11
/// What Was Changed:
///		Added methods to retrieve the user details and 
///		update user details from the DB
///	Last Updated By: Jennifer Nicewanner
///	Last Updated Date:  2025-03-28
///	What Was Changed:
///	    Added methods for admin to edit the user status including for active, read only, or suspended as 
///	    well as adding a date and text for added restrictions.
///	    
/// Last Updated By: Chase Hannen
///	Last Updated Date:  2025-04-04
///	What Was Changed:
///	    Added SelectAllUsersByProjectID and UnassignVolunteerFromProject
///	    
/// Last Updated By: Stan Anderson
///	Last Updated Date:  2025-04-09
///	What Was Changed:
///	    Fixed an issue with updating a user
///	    
/// Last Updated By: Ellie Wacker
///	Last Updated Date:  2025-04-11
///	What Was Changed: Added UpdatePasswordHashByEmail
///	
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-18
/// What was changed: Removed ProfileAccessor and moved its code here. 
/// 
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What Was Changed:
///     Added DeactivateUserByUserID
/// </summary>

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataDomain;
using DataAccessInterfaces;
using static DataDomain.User;
using System.Data.SqlTypes;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        // Author: Kate Rich
        public User SelectUserInformationByUserID(int userID)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_userInformation_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        City = reader.IsDBNull(5) ? null : reader.GetString(5),
                        State = reader.IsDBNull(6) ? null : reader.GetString(6)
                    };
                }
                else
                {
                    throw new ArgumentException("User Record Not Found...");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Record Found...", ex);
            }
            return user;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Executes the stored procedure 
        /// sp_insert_new_user to insert a new user 
        /// into the database with their given name, family name,
        /// phone number, email, and password.
        /// Last Updated By: Brodie Pasker
        /// Last Updated: 2025/04/30
        /// What Was Changed: Added byte[] defaultPFP and string defaultPFPMimeType for the ability to add a default profile picture upon account creation
        /// </summary>
        public int InsertUser(string givenName, string familyName, string phoneNumber, string email, string password, byte[] defaultPFP, string defaultPFPMimeType)
        {
            int rowsAffected = 0;


            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_insert_new_user", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50).Value = givenName;
                    cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 50).Value = familyName;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11).Value = phoneNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250).Value = email;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = password;

                    cmd.Parameters.AddWithValue("@Image", ((object)defaultPFP) ?? SqlBinary.Null);
                    cmd.Parameters.AddWithValue("@ImageMimeType", defaultPFPMimeType ?? (object)DBNull.Value);

                    try
                    {
                        conn.Open();

                        rowsAffected = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Insert User failed", ex);
                    }

                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Executes the stored procedure 
        /// sp_select_user_by_email
        /// to check if a user already exists in the 
        /// database with that email address.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// 
        /// 
        /// 
        /// </summary>
        /// 
        public bool VerifyEmailExists(string email)
        {
            bool exists = false;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_verify_email_exists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250).Value = email;

                    try
                    {
                        conn.Open();

                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        if (result == 1)
                        {
                            exists = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Check Email Exists failed", ex);
                    }
                }
            }

            return exists;
        }


        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Gets a list of all roles assigned to a specific user by their user id
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="userID">The user id of the user to get the roles for</param>
        /// <returns>List of roles</returns>
        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<string>();

            // connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_select_roles_by_UserID", conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            // values
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(reader.GetString(1));
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
            return roles;
        }


        // Author: Jennifer Nicewanner
        public User SelectUserByEmail(string email)
        {
            User newuser = null;

            // connection
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_user_by_email", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    int fieldIndex = 7;
                    long fieldWidth;
                    byte[] image = null;
                    if (!reader.IsDBNull(fieldIndex))
                    {
                        fieldWidth = reader.GetBytes(fieldIndex, 0, null, 0, Int32.MaxValue);
                        image = new byte[fieldWidth];
                        reader.GetBytes(fieldIndex, 0, image, 0, image.Length);
                    }

                    newuser = new User()
                    {
                        //[UserID],
                        //[GivenName],
                        //[FamilyName],
                        //[PhoneNumber],
                        //[Email],
                        //[City],
                        //[State], /*[Image], [ImageMimeType])*/
                        //[ReactivationDate],
                        //[Suspended],
                        //[ReadOnly],
                        //[Active],
                        //[RestrictionDetails],
                        //[Biography]

                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        City = reader.IsDBNull(5) ? null : reader.GetString(5),
                        State = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Image = image,
                        ImageMimeType = reader.IsDBNull(8) ? null : reader.GetString(8),
                        ReactivationDate = reader.IsDBNull(9) ? null : reader.GetDateTime(9),
                        Suspended = reader.GetBoolean(10),
                        ReadOnly = reader.GetBoolean(11),
                        Active = reader.GetBoolean(12),
                        RestrictionDetails = reader.GetString(13),
                        Biography = reader.GetString(14)
                    };
                }
                else
                {
                    throw new ArgumentException("User not found for that email.");
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

            return newuser;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Used to authenticate a user with their email and a hashed version of their password
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="email">User email</param>
        /// <param name="passwordHash">User hashed password</param>
        /// <exception cref="SQLException">Retrieval failed</exception>
        /// <returns>Returns 1 if there is a user that exists in the db</returns>
        public int SelectUserCountByEmailAndPasswordHash(string email, string passwordHash)
        {
            int count = 0;
            // we need to get a count of employees that meet our requirements
            // first we need to get a connection
            var conn = DBConnection.GetConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_authenticate_user", conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add the parameters to the command object
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // add the values to the parameters
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now to work with the database. That is unsafe code, so it needs a try/catch
            try
            {
                // open the connection
                conn.Open();

                // execute the command and capture the result
                var result = cmd.ExecuteScalar();
                count = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Updates the current user logged in making sure to incorporate concurrency
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/09
        /// Allowed the old city and state to be null
        /// </remarks>
        /// <param name="oldUser">The old user details that is being checked for concurrency</param>
        /// <param name="newUser">The new user details</param>
        /// <exception cref="SQLException">Update fails (User data not found. Did you update somewhere else?)</exception>
        /// <returns>Rows updated</returns>    
        public int UpdateUserByID(User oldUser, User newUser)
        {
            int result = 0;

            //connection
            var conn = DBConnection.GetConnection();
            //command
            var cmd = new SqlCommand("sp_update_user_by_UserID", conn);
            //type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.AddWithValue("@UserID", oldUser.UserID);
            cmd.Parameters.AddWithValue("@OldGivenName", oldUser.GivenName);
            cmd.Parameters.AddWithValue("@OldFamilyName", oldUser.FamilyName);
            cmd.Parameters.AddWithValue("@OldBiography", oldUser.Biography ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldUser.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldUser.Email);
            cmd.Parameters.AddWithValue("@OldCity", oldUser.City ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@OldState", oldUser.State ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@OldImage", ((object)oldUser.Image) ?? SqlBinary.Null);
            cmd.Parameters.AddWithValue("@OldImageMimeType", oldUser.ImageMimeType ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NewGivenName", newUser.GivenName);
            cmd.Parameters.AddWithValue("@NewFamilyName", newUser.FamilyName);
            cmd.Parameters.AddWithValue("@NewBiography", newUser.Biography ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newUser.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewEmail", newUser.Email);
            cmd.Parameters.AddWithValue("@NewCity", newUser.City ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NewState", newUser.State ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NewImage", ((object)newUser.Image) ?? SqlBinary.Null);
            cmd.Parameters.AddWithValue("@NewImageMimeType", newUser.ImageMimeType ?? (object)DBNull.Value);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("User data not found. Did you update somewhere else?");
            }

            return result;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Returns a user object that has passed in user email
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="email">User's email address</param>
        /// <returns>A user object</returns>
        public UserVM SelectUserDetailsByEmail(string email)
        {
            UserVM user = null;

            // connection
            var conn = DBConnection.GetConnection();

            // command
            var cmd = new SqlCommand("sp_select_user_details_by_email", conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int fieldIndex = 8; // Getting the column that the byte array is stored
                    long fieldWidth; // Image varbinary size
                    byte[] image = null; // Setting null if the image data doesn't exisy
                    if (!reader.IsDBNull(fieldIndex)) // If there IS data in the varbinary field
                    {
                        fieldWidth = reader.GetBytes(fieldIndex, 0, null, 0, Int32.MaxValue); // Buffer size
                        image = new byte[fieldWidth]; // Setting the byte array size to the size of the varbinary
                        reader.GetBytes(fieldIndex, 0, image, 0, image.Length); // Finally getting the actual image data
                    }
                    user = new UserVM()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        Biography = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Email = reader.GetString(5),
                        City = reader.IsDBNull(6) ? null : reader.GetString(6),
                        State = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Image = reader.IsDBNull(8) ? null : image,
                        ImageMimeType = reader.IsDBNull(9) ? null : reader.GetString(9),
                        PasswordHash = reader.GetString(10),
                        ReactivationDate = reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                        Suspended = reader.GetBoolean(12),
                        ReadOnly = reader.GetBoolean(13),
                        Active = reader.GetBoolean(14),
                        RestrictionDetails = reader.IsDBNull(15) ? null : reader.GetString(15)
                    };
                    return user;
                }
                else
                {
                    throw new ArgumentException("User record not found.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // Author: Jennifer Nicewanner
        public List<User> SelectApprovedUserByProjectID(int ProjectID)
        {
            List<User> users = new List<User>();

            // connection
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_approved_user_by_projectid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@ProjectID"].Value = ProjectID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //[User].[UserID], [GivenName], [FamilyName], [PhoneNumber], [Email]

                    User u = new User()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4)
                    };

                    users.Add(u);
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;
        } // END SelectApprovedUserByProjectID

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/11
        /// 
        /// Deactivates User
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="email">User's Email</param>
        /// <param name="password">User's Password</param>
        public bool DeactivateUser(string email, string password)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = email;
            cmd.Parameters[1].Value = password;

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

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/03
        /// 
        /// Retreives Data for All Users
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public List<User> SelectAllUsers()
        {
            List<User> users = new List<User>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_users", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        City = reader.IsDBNull(5) ? null : reader.GetString(5),
                        State = reader.IsDBNull(6) ? null : reader.GetString(6),
                        //TODO Add ByteReader
                        ReactivationDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                        Suspended = reader.GetBoolean(9),
                        ReadOnly = reader.GetBoolean(10),
                        Active = reader.GetBoolean(11),
                        RestrictionDetails = reader.GetString(12),
                    });
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

            return users;
        }

        //Author: Jennifer Nicewanner
        public int EditUserStatusById(User OldUser, User NewUser)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_user_status_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", OldUser.UserID);
            //cmd.Parameters.AddWithValue("@OldGivenName", OldUser.GivenName);
            //cmd.Parameters.AddWithValue("@OldFamilyName", OldUser.FamilyName);
            //cmd.Parameters.AddWithValue("@OldPhoneNumber", OldUser.PhoneNumber);
            //cmd.Parameters.AddWithValue("@OldEmail", OldUser.Email);           
            //if (OldUser.City == null)
            //{
            //    cmd.Parameters.AddWithValue("@OldCity", DBNull.Value);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@OldCity", OldUser.City);
            //}

            //if (OldUser.State == null)
            //{
            //    cmd.Parameters.AddWithValue("@OldState", DBNull.Value);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@OldState", OldUser.State);
            //}
            //cmd.Parameters.AddWithValue("@OldImage", OldUser.Image);
            //cmd.Parameters.AddWithValue("@OldImageMimeType", OldUser.ImageMimeType);
            if (OldUser.ReactivationDate == null)
            {
                cmd.Parameters.AddWithValue("@OldReactivationDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldReactivationDate", OldUser.ReactivationDate);
            }
            //cmd.Parameters.AddWithValue("@OldSuspended", OldUser.Suspended);
            //cmd.Parameters.AddWithValue("@OldReadOnly", OldUser.ReadOnly);
            //cmd.Parameters.AddWithValue("@OldActive", OldUser.Active);
            cmd.Parameters.AddWithValue("@OldRestrictionDetails", OldUser.RestrictionDetails);
            //cmd.Parameters.AddWithValue("@OldBiography", OldUser.Biography);


            //cmd.Parameters.AddWithValue("@NewGivenName", NewUser.GivenName);
            //cmd.Parameters.AddWithValue("@NewFamilyName", NewUser.FamilyName);
            //cmd.Parameters.AddWithValue("@NewPhoneNumber", NewUser.PhoneNumber);
            //cmd.Parameters.AddWithValue("@NewEmail", NewUser.Email);

            //if (NewUser.City == null)
            //{
            //    cmd.Parameters.AddWithValue("@NewCity", DBNull.Value);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@NewCity", NewUser.City);
            //}


            //if (NewUser.State == null)
            //{
            //    cmd.Parameters.AddWithValue("@NewState", DBNull.Value);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@NewState", NewUser.State);
            //}

            //cmd.Parameters.AddWithValue("@NewImage", NewUser.Image);
            //cmd.Parameters.AddWithValue("@NewImageMimeType", NewUser.ImageMimeType);

            if (NewUser.ReactivationDate == null)
            {
                cmd.Parameters.AddWithValue("@NewReactivationDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewReactivationDate", NewUser.ReactivationDate);
            }

            cmd.Parameters.AddWithValue("@NewSuspended", NewUser.Suspended);
            cmd.Parameters.AddWithValue("@NewReadOnly", NewUser.ReadOnly);
            cmd.Parameters.AddWithValue("@NewActive", NewUser.Active);
            cmd.Parameters.AddWithValue("@NewRestrictionDetails", NewUser.RestrictionDetails == null ? "" : NewUser.RestrictionDetails);
            //cmd.Parameters.AddWithValue("@NewBiography", NewUser.Biography);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();  //the database call
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        } // End EditUserStatusById

        //Auhtor : Christivie
        public User SelectUserByID(int id)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_User_by_ID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", id);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return user;
        }

        // Author: Chase Hannen
        public List<User> SelectAllUsersByProjectID(int projectID)
        {
            List<User> usersOnProject = new List<User>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_user_by_projectID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Image processing
                    int fieldIndex = 7;
                    long fieldWidth;
                    byte[] image = null;
                    if (!reader.IsDBNull(fieldIndex))
                    {
                        fieldWidth = reader.GetBytes(fieldIndex, 0, null, 0, Int32.MaxValue);
                        image = new byte[fieldWidth];
                        reader.GetBytes(fieldIndex, 0, image, 0, image.Length);
                    }

                    User user = new User();
                    user.UserID = reader.GetInt32(0);
                    user.GivenName = reader.GetString(1);
                    user.FamilyName = reader.GetString(2);
                    user.PhoneNumber = reader.GetString(3);
                    user.Email = reader.GetString(4);
                    user.City = reader.IsDBNull(5) ? null : reader.GetString(5);
                    user.State = reader.IsDBNull(6) ? null : reader.GetString(6);
                    user.Image = image;
                    user.ImageMimeType = reader.IsDBNull(8) ? null : reader.GetString(8);
                    user.PasswordHash = reader.GetString(9);
                    user.ReactivationDate = reader.IsDBNull(10) ? null : reader.GetDateTime(10);
                    user.Suspended = reader.GetBoolean(11);
                    user.ReadOnly = reader.GetBoolean(12);
                    user.Active = reader.GetBoolean(13);
                    user.RestrictionDetails = reader.GetString(14);
                    user.Biography = reader.GetString(15);
                    usersOnProject.Add(user);
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

            return usersOnProject;
        } // END SelectAllUsersByProjectID

        // Author: Chase Hannen
        public int UnassignVolunteerFromProject(int userID, int projectID)
        {
            int result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_volunteer_status_by_user_id_and_project_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);

            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        } // END UnassignVolunteerFromProject

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/04/11
        /// 
        /// This method pulls all project roles that the user has
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="UserID">An integer field representing the User's ID</param>
        /// <exception cref="Exception">Throws an exception</exception>
        /// <returns>A list of UserProjectRoles</returns>
        public List<UserProjectRole> SelectProjectRolesByUserID(int UserID)
        {
            List<UserProjectRole> roles = new List<UserProjectRole>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_project_roles_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = UserID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string role = reader.GetString(0);
                    int projectId = reader.GetInt32(1);
                    roles.Add(new UserProjectRole()
                    {
                        ProjectId = projectId,
                        ProjectRole = role
                    });
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
            return roles;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/02
        /// 
        /// This method updates the password hash by using the existing email to find the desired 
        /// user's old password and updating it to match the new one
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="email">An NVarchar field with a length of 250, that holds the user's email(also known as username)</param>
        /// <param name="oldPasswordHash">An NVarchar field with a length of 100, that holds the user's old password</param>
        /// <param name="newPasswordHash">An NVarchar field with a length of 100, that holds the user's new password</param>
        /// <exception cref="Exception">Throws an exception</exception>
        /// <returns>A 1 if the update was sucessful and a 0 if it fails</returns>   
        public int UpdatePasswordHashByEmail(string email, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            //connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_update_passwordhash_by_email", conn);
            //type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250).Value = email;
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            //values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-05
        /// Summary: This method connects to the database and uses the stored procedure to display the parameters 
        /// in the Page called View_Profile.xaml.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public User SelectUserByInfo(string email)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_profile_info", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Email"].Value = email;


            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    r.Read();
                    user = new User()
                    {
                        GivenName = r.GetString(0),
                        FamilyName = r.GetString(1),
                        City = r.GetString(2),
                        State = r.GetString(3),
                        Email = r.GetString(4)
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
            return user;
        }

        ///<summary>
        /// Creator: Jennifer Nicewanner
        /// Created: 2025-04-22
        /// Summary: This method lets an admin change a user status to deactivated by unchecking the active field
        /// in the Page called ViewSingleUser.xaml.cs.
        /// Last updated by: Jennifer Nicewanner
        /// Last updated:    2025-04-22
        /// Changes:   Initial creation
        /// </summary>
        public int DeactivateUserByUserID(int userID)
        {
            int result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_user_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}