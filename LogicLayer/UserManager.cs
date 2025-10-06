/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/02/02
/// Summary: This UserManager class implements IUserManager
/// and handles user-related operations. It interacts with the
/// UserAccessor to insert users and check if an email exists.
/// It includes exception handling to ensure errors are managed properly.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-02-02
/// What Was Changed:
///		Class that implements the IUserManager Interface - used for
/// 	managing User data from User Data Fake objects &/or the DB.
/// 
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-02-07
/// What Was Changed:
///		Added GetApprovedUserByProjectID and GetUserByEmail methods
///		
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-03-28
/// What Was Changed:
///		Added the UpdateUserStatusByID method
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///		Added the GetUsersByProjectID and UnassignVolunteerByProject methods
///		
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025-04-11
/// What Was Changed:
///		Added the UpdatePassword method
///	Last Updated By: Dat Tran
///	Last Updated: 2025-04-18
///	What was changed: 
///	    Removed ProfileManager and moved its code into here. 
///	    
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What Was Changed:
///		Added the DeactivateUserByUserID method
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System.Collections;
using static DataDomain.User;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor;

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Overloaded constructor that allows dependency 
        /// injection by accepting an IUserAccessor instance.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        // Constructor for Tests
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Default constructor that initializes 
        /// the UserManager with a concrete UserAccessor.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
		// Constructor for DB
        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }
        /// <summary>
        /// Creator: Brodie Pasker
        /// Created: 2025/02/20
        /// 
        /// Logs in the user with their email and password
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public UserVM LoginUser(string email, string password)
        {
            UserVM userVM = null;
            try
            {
                if (AuthenticateUser(email, password))
                {
                    userVM = (UserVM)RetrieveUserDetailsByEmail(email);
                    if (userVM.Active == false)
                    {
                        throw new ApplicationException("Your account is not active! Please contact a system admin to reactiave your account");
                    }
                    if (userVM != null)
                    {
                        userVM.Roles = GetRolesForUser(userVM.UserID);
                        userVM.ProjectRoles = GetProjectRolesByUserID(userVM.UserID);
                    }
                }
                else
                {
                    throw new ApplicationException("Invalid Credentials");
                }
            }
            catch (Exception ex)
            {
                throw; // exception would already be wrapped by logic functions
            }
            return userVM;
        }

        /// <summary>
        /// Creator: Brodie Pasker
        /// Created: 2025/02/20
        /// 
        /// Gets the roles assigned to the UserID
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="userID"></param>
        public List<string> GetRolesForUser(int userId)
        {
            List<string> roles = null;
            try
            {
                roles = _userAccessor.SelectRolesByUserID(userId);
                if (roles.Count == 0)
                {
                    throw new Exception("No roles were retrieved from the database.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No roles were found.", ex);
            }
            return roles;
        }

        /// <summary>
        /// Creator: Brodie Pasker
        /// Created: 2025/02/20
        /// 
        /// Updated the userid int provided 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="UserID"></param>

        public bool UpdateUser(User oldUser, UserVM newUser)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.UpdateUserByID(oldUser, newUser));
                if (!result)
                {
                    throw new ApplicationException("Duplicate User records found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Creator: Brodie Pasker
        /// Created: 2025/02/20
        /// 
        /// Checks in the database for the email and password values
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>True if user with proper credentials exist, false otherwise</returns>
        public bool AuthenticateUser(string email, string password)
        {
            bool result = false;

            password = HashSHA256(password);
            result = (1 == _userAccessor.SelectUserCountByEmailAndPasswordHash(email, password));

            return result;
        }

        // Author: Kate Rich
        public User GetUserInformationByUserID(int userID)
        {
            User user = null;

            try
            {
                user = _userAccessor.SelectUserInformationByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No User Found", ex);
            }

            return user;
        }

        /// <summary>
        /// Creator: Brodie Pasker
        /// Created: 2025/02/20
        /// 
        /// Retrieves the user with the email provided
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="email"></param>
        public User RetrieveUserDetailsByEmail(string email)
        {
            User user = null;
            try
            {
                user = _userAccessor.SelectUserDetailsByEmail(email);
            }
            catch (Exception ex)    
            {
                throw new ApplicationException("Login Failed", ex);
            }
            return user;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Inserts a new user into the database.
        /// Returns true if the user was successfully added, otherwise false.Hashes
        /// the password entered by the user.
        /// Last Updated By: Brodie Pasker
        /// Last Updated: 2025/04/30
        /// What Was Changed: Added byte[] defaultPFP and string defaultPFPMimeType for the ability to add a default profile picture upon account creation

        /// </summary>
        public bool InsertUser(string givenName, string familyName, string phoneNumber, string email, string password, byte[] defaultPFP, string defaultPFPMimeType)
        {
            try
            {
                int rowsAffected = _userAccessor.InsertUser(givenName, familyName, phoneNumber, email, HashSHA256(password), defaultPFP, defaultPFPMimeType);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User insertion failed.", ex);
            }
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Checks if an email exists in the system.
        /// Returns true if the email exists, otherwise false.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool DoesEmailExist(string email)
        {
            try
            {
                return _userAccessor.VerifyEmailExists(email);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error checking email existence.");
            }
        }
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/07
        /// Summary: Hashes the passwords before passing them to the 
        /// database.
        /// create their account.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public string HashSHA256(string password)
        {
            if (password == "" || password == null)
            {
                throw new ArgumentException("Missing Input.");
            }

            string hashValue = null;

            byte[] data;

            using (SHA256 sha256provider = SHA256.Create())
            {

                data = sha256provider.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            hashValue = s.ToString();

            return hashValue;
        }

        // Author: Jennifer Nicewanner
        public List<User> GetApprovedUserByProjectID(int ProjectID)
        {
            List<User> ProjectVolunteers = new List<User>();

            try
            {
                ProjectVolunteers = _userAccessor.SelectApprovedUserByProjectID(ProjectID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Volunteer list not found.", ex);
            }
            return ProjectVolunteers;
        } // END GetApprovedUserByProjectID

        // Author: Jennifer Nicewanner
        public User GetUserByEmail(string email)
        {
            User user;

            try
            {
                user = _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User with that email not found.", ex);
            }
            return user;
        } // End GetUserByEmail

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
        /// <param name="password">Password to Hash</param>
        private string HashSha256(string password)
        {
            if (password == null || password == "")
            {
                throw new ArgumentException("MISSING INPUT");
            }

            string result = "";
            byte[] data;

            using (SHA256 sha256provider = SHA256.Create())
            {
                data = sha256provider.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString().ToLower();

            return result;
        }

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
            try
            {
                return _userAccessor.DeactivateUser(email, HashSha256(password));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/03
        /// 
        /// Gets a List of all the Users from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        public List<User> GetAllUsers()
        {
            List<User> users;
            try
            {
                users = _userAccessor.SelectAllUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return users;
        }

        // Author:  Jennifer Nicewanner
        public bool UpdateUserStatusByID(User OldUser, User NewUser)
        {
            bool result = false;

            try
            {
                result = (1 == _userAccessor.EditUserStatusById(OldUser, NewUser));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("User not updated.", ex);
            }
            return result;
            //throw new NotImplementedException();
            //return true;
        } // End UpdateUserStatusByID

        public User GetUserById(int userID)
        {
            try
            {
                return _userAccessor.SelectUserByID(userID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("User not found.", ex);
            }
        }

        // Author: Chase Hannen
        public List<User> GetUsersByProjectID(int projectID)
        {
            List<User> usersOnProject;

            try
            {
                usersOnProject = _userAccessor.SelectAllUsersByProjectID(projectID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User retrieval failed", ex);
            }

            return usersOnProject;
        } // END GetUsersByProjectID

        // Author: Chase Hannen
        public bool UnassignVolunteerByProject(int userID, int projectID)
        {
            try
            {
                int rowsAffected = _userAccessor.UnassignVolunteerFromProject(userID, projectID);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Volunteer unassignment failed.", ex);
            }
        } // END UnassignVolunteerByProject

        // Creator: Ellie Wacker
        public bool UpdatePassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;
            oldPassword = HashSHA256(oldPassword);
            newPassword = HashSHA256(newPassword);
            try
            {
                result = (1 == _userAccessor.UpdatePasswordHashByEmail(email, oldPassword, newPassword));
                if (!result)
                {
                    throw new ApplicationException("Duplicate User Records Found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Creator: Brodie Pasker
        /// Created: 2025/02/20
        /// 
        /// Gets the project roles assigned to the UserID
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="userID"></param>
        public List<UserProjectRole> GetProjectRolesByUserID(int userId)
        {
            List<UserProjectRole> roles = null;
            try
            {
                roles = _userAccessor.SelectProjectRolesByUserID(userId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Retrieving Project Roles for User was unsuccessful", ex);
            }
            return roles;
        }

        ///<summary>
        /// Creator: Dat Tran
        /// Created: 02/05/2025
        /// Summary: This method manages the method in ProfileAccessor. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public User GetUsersByInfo(string email)
        {
            User user = null;
            try
            {
                user = _userAccessor.SelectUserByInfo(email);
                if (user == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return user;
        }

        // Author:  Jennifer Nicewanner
        public bool DeactivateUserByUserID(int userID)
        {
            //throw new NotImplementedException();
            //return true;

            bool result = false;

            try
            {
                result = (1 == _userAccessor.DeactivateUserByUserID(userID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not deactivated.", ex);
            }
            return result;
        } // End DeactivateUserByUserID
    }
}