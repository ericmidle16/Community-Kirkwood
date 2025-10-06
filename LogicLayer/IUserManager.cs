/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/02
/// Summary: Defines the IUserManager interface
/// in the LogicLayer namespace, which includes 
/// methods for checking if an email exists and 
/// inserting a new user with their details 
/// (name, phone number, email, password).
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-02-11
/// What Was Changed:
///		Interface that holds method declarations for managing User data.
/// 
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-02-07
/// What Was Changed:
///		Added the GetUserByEmail and GetApprovedUserByProjectID methods
///	Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-03-28
/// What Was Changed:
///		Added the UpdateUserStatusByID method
/// 
///	Last Updated By: Christivie MAuwa
/// Last Updated: 2025-03-28
/// What Was Changed:
///		Added the GetUserID method
///		
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///		Added the GetUsersByProjectID and UnassignVolunteerByProject methods
///		
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025-04-10
/// What Was Changed:
///		Added the UpdatePassword method
///	Last Updated By: Dat Tran
///	Last Updated: 2025-04-11
///	What was changed:
///	    Removed IProfileManager and moved its method into here. 
///	    
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What Was Changed: 
///     Added DeactivateUserByUserID method
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;
using static DataDomain.User;

namespace LogicLayer
{
    public interface IUserManager
    {
        User GetUserInformationByUserID(int userID);
        bool DoesEmailExist(string email);
        public bool InsertUser(string givenName, string familyName, string phoneNumber, string email, string password, byte[] defaultPFP, string defaultPFPMimeType);
        bool AuthenticateUser(string username, string password);
        User RetrieveUserDetailsByEmail(string email);
        UserVM LoginUser(string username, string password);
        List<string> GetRolesForUser(int userid);
        bool UpdateUser(User oldUser, UserVM newUser);
        string HashSHA256(string password);
        public User GetUserByEmail(string email);
        public List<User> GetApprovedUserByProjectID(int ProjectID);
        public List<User> GetAllUsers();
        public bool DeactivateUser(string email, string password);
        bool UpdateUserStatusByID(User OldUser, User NewUser);
        User GetUserById(int userID);
        public List<User> GetUsersByProjectID(int projectID);
        public bool UnassignVolunteerByProject(int userID, int projectID);
        public bool UpdatePassword(string email, string oldPassword, string newPassword);
        public List<UserProjectRole> GetProjectRolesByUserID(int userID);
        public User GetUsersByInfo(string email);
        public bool DeactivateUserByUserID(int userID);
    }
}