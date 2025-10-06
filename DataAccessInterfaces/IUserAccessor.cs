/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/02
/// Summary:Defines the IUserAccessor interface, 
/// which includes methods for inserting a 
/// new user into the database and checking if a user 
/// exists by email.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-02-11
/// What Was Changed:
///		Interface that holds method declarations for accessing User data.
///	Last Updated By: Brodie Pasker
/// Last Updated: 2025-03-11
/// What Was Changed:
///		Added interfaces for accessing user details as well as updating user details
///		Updated using statement to access the UserVM	
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-03-13
/// What Was Changed:  Added definitions for SelectUserByEmail & SelectApprovedUserByProjectID.
///     Renamed/Refactored Sky's previous SelectUserByEmail to be VerifyEmailExists, as that name
///     better explains what her method is doing.
///Last Updated By:  Jennifer Nicewanner
///Last Updated:  2025-03-28
///What Was Changed:  Added interface for EditUserStatusById method;
///
///Last Updated By:  Christivie Mauwa
///Last Updated:  2025-03-28
///What Was Changed:  Added interface for SelectUserByID method;
///
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///     Added SelectAllUsersByProjectID and UnassignVolunteerFromProject
///     
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025-04-10
/// What Was Changed:
///     Added UpdatePasswordHashByEmail
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-18
/// What was changed: Deleted IProfileAccessor and moved its code into here.
/// 
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What Was Changed:
///     Added DeactivateUserByUserID
///	</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        User SelectUserInformationByUserID(int userID);
        int InsertUser(string givenName, string familyName, string phoneNumber, string email, string password,byte[] defaultPFP, string defaultPFPMimeType);
        List<string> SelectRolesByUserID(int userID);
        int SelectUserCountByEmailAndPasswordHash(string email, string passwordHash);
        int UpdateUserByID(User oldUser, User newUser);
        UserVM SelectUserDetailsByEmail(string email);
        bool VerifyEmailExists(string email);
        User SelectUserByEmail(string email);
        List<User> SelectApprovedUserByProjectID(int ProjectID);
        public List<User> SelectAllUsers();
        public bool DeactivateUser(string email, string password);
        int EditUserStatusById(User OldUser, User NewUser);
        User SelectUserByID(int id);
        List<User> SelectAllUsersByProjectID(int projectID);
        int UnassignVolunteerFromProject(int userID, int projectID);
        int UpdatePasswordHashByEmail(string email, string oldPasswordHash, string newPasswordHash);
        List<UserProjectRole> SelectProjectRolesByUserID(int UserID);

        public User SelectUserByInfo(string email);
        int DeactivateUserByUserID(int userID);

    }
}