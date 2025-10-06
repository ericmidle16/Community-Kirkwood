/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/02
/// Summary: Implements the IUserAccessor interface with 
/// methods for inserting a new user, 
/// checking if a user exists by email,
/// and storing a list of fake users for tests.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-02-11
/// What Was Changed:
/// 	Class for fake User objects that are used in testing.
/// 	
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-02-07
/// What Was Changed:  Initial Creation
/// 
/// Last Updated By: Christivie Mauwa
/// Last Updated: 2025= 03-28
/// What Was Changed:  Initial Creation
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed: 
///     Added SelectAllUsersByProjectID and UnassignVolunteerFromProject
///     
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025-04-11
/// What Was Changed: 
///     Added SelectAllUsersByProjectID and UnassignVolunteerFromProject
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-18
/// What was changed:
///     Removed ProfileAccessorFakes and moved its code into here. 
///     
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What Was Changed: 
///     Added DeactivateUserByUserID
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataDomain.User;

namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        private List<User> _users;
        private List<UserVM> _userVMs;
        private List<VolunteerStatus> _applications;
        private User _lastFoundUser;
        private List<string> _passwordhashes;

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Constructor for the UserAccessorFake
        /// class. Initializes a list of fake users to 
        /// simulate the behavior of the user data access 
        /// layer.
        /// Last Upaded By: Kate Rich
        /// Last Updated: 2025-02-28
        /// What Was Changed:
        ///		Added additional data to the list of Users.
        ///	Last Upaded By: Brodie Pasker
        /// Last Updated: 2025-03-14
        /// What Was Changed:
        ///		Added additional data to the list of Users.
        ///Last Updated By:  Jennifer Nicewanner
        ///Last Updated:d 2025-03-28
        ///What Was Changed:
        ///     Added testing for EditUserStatusById
        ///
        /// </summary>
        public UserAccessorFake()
        {

            _users = new List<User>()
            {
                new User()
                {
                    UserID = 1,
                    GivenName = "Test1",
                    FamilyName = "Test1",
                    Biography = "Lorum Ipsum",
                    PhoneNumber = "1234567890",
                    Email = "test1@test.com",
                    City = "New York City",
                    State = "New York",
                    // Image = new byte[0],
                    ImageMimeType = "Image/png",
                    PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                    ReactivationDate = new DateTime(2022, 10, 12),
                    Suspended = false,
                    ReadOnly = false,
                    Active = true,
                    RestrictionDetails = "Lorum Ipsum."
                },
                new User()
                {
                    UserID = 2,
                    GivenName = "Test2",
                    FamilyName = "Test2",
                    Biography = "Blah blah blah",
                    PhoneNumber = "1234567890",
                    Email = "test2@test.com",
                    City = "Los Angeles",
                    State = "California",
                    // Image = new byte[0],
                    ImageMimeType = "Image/png",
                    PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                    ReactivationDate = new DateTime(),
                    Suspended = true,
                    ReadOnly = false,
                    Active = true,
                    RestrictionDetails = "Account suspended due to policy violation"
                },
                new User()
                {
                    UserID = 3,
                    GivenName = "Test3",
                    FamilyName = "Test3",
                    Biography = "blah blah blah",
                    PhoneNumber = "1234567890",
                    Email = "test3@test.com",
                    City = "Houston",
                    State = "Texas",
                    // Image = new byte[0],
                    ImageMimeType = "Image/png",
                    PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                    ReactivationDate = new DateTime(),
                    Suspended = false,
                    ReadOnly = true,
                    Active = false,
                    RestrictionDetails = "Read-only access assigned for compliance"
                },
            };

            _users.Add(new User()
            {
                UserID = 4,
                GivenName = "Hank",
                FamilyName = "Hill",
                Biography = "Lorum Ipsum",
                PhoneNumber = "1234567890",
                Email = "hank@stricklandpropane.com",
                City = "Arlen",
                State = "Texas",
                // Image = new byte[0],
                ImageMimeType = "Image/png",
                PasswordHash = "a70b6ea95832e115bb7e31e8142ffa7b38854007543e611979308d32520255d9",
                ReactivationDate = new DateTime(2022, 10, 12),
                Suspended = false,
                ReadOnly = false,
                Active = true,
                RestrictionDetails = "Lorum Ipsum."
            });
            _users.Add(new User()
            {
                UserID = 5,
                GivenName = "Peggy",
                FamilyName = "Hill",
                PhoneNumber = "1234567890",
                Email = "peggy@tlms.edu",
                City = "Arlen",
                State = "Texas",
                // Image = new byte[0],
                ImageMimeType = "Image/png",
                PasswordHash = "daa1fbe049b816ac1d3f7018798400a3427ab5979a5b5a5a021fc348a44ffb99",
                ReactivationDate = new DateTime(),
                Suspended = true,
                ReadOnly = false,
                Active = true,
                RestrictionDetails = "Account suspended due to policy violation"
            });

            // JN Data Fakes
            _users.Add(new User()
            {
                UserID = 6,
                GivenName = "A",
                FamilyName = "AA",
                PhoneNumber = "1111111111111",
                Email = "a@test.com",
                Active = true
            });

            _users.Add(new User()
            {
                UserID = 7,
                GivenName = "B",
                FamilyName = "BB",
                PhoneNumber = "2222222222222",
                Email = "b@test.com",
                Active = true
            });

            _users.Add(new User()
            {
                UserID = 8,
                GivenName = "C",
                FamilyName = "CC",
                PhoneNumber = "3333333333333",
                Email = "c@test.com",
                Active = true
            });
            _users.Add(new User()
            {
                UserID = 8,
                GivenName = "C",
                FamilyName = "CC",
                PhoneNumber = "3333333333333",
                Email = "c@test.com",
                Active = true
            });
            _users.Add(new User()
            {
                UserID = 9,
                GivenName = "Test1",
                FamilyName = "Test1",
                Email = "test6@test.com",
                PhoneNumber = "1234567890",
                City = "Cedar Rapids",
                State = "Iowa",
                PasswordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e",
                ReactivationDate = DateTime.MinValue,
                Suspended = false,
                ReadOnly = false,
                Active = true,
                RestrictionDetails = "It wasn't",
            });

            _applications = new List<VolunteerStatus>();
            _applications.Add(new VolunteerStatus()
            {
                UserID = 6,
                ProjectID = 1,
                Approved = true
            });
            _applications.Add(new VolunteerStatus()
            {
                UserID = 7,
                ProjectID = 2,
                Approved = true
            });
            _applications.Add(new VolunteerStatus()
            {
                UserID = 8,
                ProjectID = 2,
                Approved = true
            });

            _userVMs = new List<UserVM>();
            _userVMs.Add(new UserVM()
            {
                UserID = 1,
                GivenName = "John",
                FamilyName = "Doe",
                Biography = "Lorum Ipsum",
                PhoneNumber = "1234567890123",
                Email = "john.doe@example.com",
                City = "New York City",
                State = "New York",
                Image = new byte[0],
                ImageMimeType = "Image/png",
                PasswordHash = "a70b6ea95832e115bb7e31e8142ffa7b38854007543e611979308d32520255d9",
                ReactivationDate = new DateTime(2022, 10, 12),
                Suspended = false,
                ReadOnly = false,
                Active = true,
                RestrictionDetails = "Lorum Ipsum.",
                Roles = new List<string>(),
                ProjectRoles = new List<UserProjectRole>()
            });
            _userVMs.Add(new UserVM()
            {
                UserID = 2,
                GivenName = "Jane",
                FamilyName = "Smith",
                Biography = "Blah blah blah",
                PhoneNumber = "9876543210987",
                Email = "jane.smith@example.com",
                City = "Los Angeles",
                State = "California",
                Image = new byte[0],
                ImageMimeType = "Image/png",
                PasswordHash = "daa1fbe049b816ac1d3f7018798400a3427ab5979a5b5a5a021fc348a44ffb99",
                ReactivationDate = new DateTime(),
                Suspended = true,
                ReadOnly = false,
                Active = true,
                RestrictionDetails = "Account suspended due to policy violation",
                Roles = new List<string>(),
                ProjectRoles = new List<UserProjectRole>()
            });
            _userVMs.Add(new UserVM()
            {
                UserID = 3,
                GivenName = "Bob",
                FamilyName = "Williams",
                Biography = "blah blah blah",
                PhoneNumber = "4449876543210",
                Email = "bob.williams@example.com",
                City = "Houston",
                State = "Texas",
                Image = new byte[0],
                ImageMimeType = "Image/png",
                PasswordHash = "0d8bf5956ded6a6bd243103a1796d030159edd1cea30263f815f164ebdb8c264",
                ReactivationDate = new DateTime(),
                Suspended = false,
                ReadOnly = true,
                Active = false,
                RestrictionDetails = "Read-only access assigned for compliance",
                Roles = new List<string>(),
                ProjectRoles = new List<UserProjectRole>()
            });


            _userVMs[0].Roles.Add("Role1");
            _userVMs[0].Roles.Add("Role2");

            _userVMs[1].Roles.Add("Role3");


            _userVMs[2].Roles.Add("Role4");
            _userVMs[2].Roles.Add("Role5");

            _userVMs[0].ProjectRoles.Add(new UserProjectRole()
            {
                ProjectId = 1,
                ProjectRole = "Manager"
            });
            _userVMs[0].ProjectRoles.Add(new UserProjectRole()
            {
                ProjectId = 1,
                ProjectRole = "Accountant"
            });
            _userVMs[0].ProjectRoles.Add(new UserProjectRole()
            {
                ProjectId = 2,
                ProjectRole = "Volunteer"
            });
            _userVMs[0].ProjectRoles.Add(new UserProjectRole()
            {
                ProjectId = 2,
                ProjectRole = "Janitor"
            });

            _passwordhashes = new List<string>();

            _users.Add(new User()
            {
                UserID = 100000,
                GivenName = "A",
                FamilyName = "AA",
                PhoneNumber = "1005003411",
                Email = "a1@test.com",
                Suspended = false,
                ReadOnly = false,
                Active = true,
                RestrictionDetails = ""
            });
            _users.Add(new User()
            {
                UserID = 100001,
                GivenName = "B",
                FamilyName = "BB",
                PhoneNumber = "1042353411",
                Email = "b2@test.com",
                Suspended = false,
                ReadOnly = false,
                Active = true,
                RestrictionDetails = ""
            });

            _users.Add(new User()
            {
                GivenName = "Something",
                FamilyName = "What",
                Email = "test1@test.com",
                City = "somewhere",
                State = "Iowa"
            });
            _users.Add(new User()
            {
                GivenName = "This",
                FamilyName = "Thing",
                Email = "test2@test.com",
                City = "someplace",
                State = "Iowa"
            });
            _users.Add(new User()
            {
                GivenName = "Donald",
                FamilyName = "Trump",
                Email = "tes31@test.com",
                City = "somehow",
                State = "Iowa"
            });

            //put after all users fakes are added
            for (int i = 0; i < _users.Count; i++)
            {
                _passwordhashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            }
        }


        public User SelectUserInformationByUserID(int userID)
        {
            foreach (User u in _users)
            {
                if (u.UserID == userID)
                {
                    return u;
                }
            }
            throw new ArgumentException("User Record Not Found.");
        }


        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Inserts a new user into the fake user 
        /// list after validating input parameters like name,
        /// email, and password, and checking for duplicate 
        /// emails.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int InsertUser(string givenName, string familyName, string phoneNumber, string email, string password, byte[] defaultPFP, string defaultPFPMimeType)
        {
            int count = _users.Count();
            if (string.IsNullOrWhiteSpace(givenName))
            {
                throw new ApplicationException("Given name cannot be blank.");
            }

            if (string.IsNullOrWhiteSpace(familyName))
            {
                throw new ApplicationException("Family name cannot be blank.");
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ApplicationException("Phone number cannot be blank.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ApplicationException("Email cannot be blank.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException("Password cannot be blank.");
            }


            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                throw new ApplicationException("Invalid email format.");
            }

            if (_users.Any(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Email already exists.");
            }
            if (familyName.Length > 50)
            {
                throw new ApplicationException("Family name cannot exceed 50 characters.");
            }

            if (givenName.Length > 50)
            {
                throw new ApplicationException("Given name cannot exceed 50 characters.");
            }

            if (email.Length > 250)
            {
                throw new ApplicationException("Email cannot exceed 250 characters.");
            }

            if (password.Length < 7 || password.Length > 100)
            {
                throw new ApplicationException("Password must be between 7 and 100 characters.");
            }

            int newUserID = _users.Any() ? _users.Max(user => user.UserID) + 1 : 1;

            var newUser = new User
            {
                UserID = newUserID,
                GivenName = givenName,
                FamilyName = familyName,
                PhoneNumber = phoneNumber,
                Email = email,
                PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                Active = true
            };

            _users.Add(newUser);

            return _users.Count() - count;

        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Retrieves the last found user, 
        /// based on the last search by email.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public User GetLastFoundUser()
        {
            return _lastFoundUser;
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Searches for a user by email in the fake
        /// user list and returns true if found, 
        /// otherwise false. Stores the found user
        /// in a private variable.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool VerifyEmailExists(string email)
        {
            foreach (var user in _users)
            {
                if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    _lastFoundUser = user;
                    return true;
                }
            }
            _lastFoundUser = null;
            return false;
        }

        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<string>();

            foreach (var user in _userVMs)
            {
                if (user.UserID == userID)
                {
                    roles = user.Roles;
                }
            }
            return roles;
        }

        public int SelectUserCountByEmailAndPasswordHash(string email, string passwordHash)
        {
            int count = 0;

            foreach (var user in _userVMs)
            {
                if (user.Email == email && user.PasswordHash == passwordHash && user.Active == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                foreach (var user in _users)
                {
                    if (user.Email == email && user.PasswordHash == passwordHash && user.Active == true)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public int UpdateUserByID(User oldUser, User newUser)
        {
            int count = 0;
            foreach (User user in _userVMs)
            {
                if (user.UserID == oldUser.UserID)
                {
                    if (oldUser.GivenName.Equals(user.GivenName) &&
                       oldUser.FamilyName.Equals(user.FamilyName) &&
                       oldUser.Biography.Equals(user.Biography) &&
                       oldUser.PhoneNumber.Equals(user.PhoneNumber) &&
                       oldUser.Email.Equals(user.Email) &&
                       oldUser.City.Equals(user.City) &&
                       oldUser.State.Equals(user.State) &&
                       oldUser.ImageMimeType.Equals(user.ImageMimeType))
                    {
                        {
                            user.GivenName = newUser.GivenName;
                            user.FamilyName = newUser.FamilyName;
                            user.Biography = newUser.Biography;
                            user.PhoneNumber = newUser.PhoneNumber;
                            user.Email = newUser.Email;
                            user.City = newUser.City;
                            user.State = newUser.State;
                            user.Image = newUser.Image;
                            user.ImageMimeType = newUser.ImageMimeType;
                        }
                        count = 1;
                    }
                    else
                    {
                        throw new Exception("User record does not exist");
                    }
                }
            }
            return count;
        }

        public UserVM SelectUserDetailsByEmail(string email)
        {
            throw new NotImplementedException();
        }

        // Author: Jennifer Nicewanner
        public List<User> SelectApprovedUserByProjectID(int ProjectID)
        {
            List<User> users = new List<User>();

            foreach (var vs in _applications)
            {
                if (vs.ProjectID == ProjectID && vs.Approved == true)
                {
                    foreach (var user in _users)
                    {
                        if (user.UserID == vs.UserID)
                        {
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        // Author: Jennifer Nicewanner
        public User SelectUserByEmail(string email)
        {

            User user = null;

            foreach (var newuser in _users)
            {
                if (newuser.Email == email)
                {
                    user = newuser;
                }
            }
            if (user == null)
            {
                throw new ArgumentException("User not found in database.");
            }

            return user;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/11
        /// 
        /// Deactivates Fake User
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
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Email == email && _passwordhashes[i] == password)
                {
                    _users[i].Active = false;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/03
        /// 
        /// Retreives Fake Data for all Fake Users
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public List<User> SelectAllUsers()
        {
            return _users;
        }

        // Author:  Jennifer Nicewanner
        public int EditUserStatusById(User OldUser, User NewUser)
        {
            //throw new NotImplementedException();
            int rows = 0;

            foreach (var user in _users)
            {
                if (NewUser.UserID == user.UserID)
                {
                    rows++;
                }
            }
            return rows;
        }

        public User SelectUserByID(int id)
        {
            return _users.FirstOrDefault(u => u.UserID == id);
        }

        // Author: Chase Hannen
        public List<User> SelectAllUsersByProjectID(int projectID)
        {
            List<User> users = new List<User>();

            foreach (VolunteerStatus assignedVolunteer in _applications)
            {
                if (assignedVolunteer.ProjectID == projectID)
                {
                    foreach (User user in _users)
                    {
                        if (user.UserID == assignedVolunteer.UserID)
                        {
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        // Author: Chase Hannen
        public int UnassignVolunteerFromProject(int userID, int projectID)
        {
            List<VolunteerStatus> assignedVolunteers = new List<VolunteerStatus>();
            int rowsAffected = 0;

            foreach (VolunteerStatus assignedVolunteer in _applications)
            {
                if (assignedVolunteer.UserID == userID && assignedVolunteer.ProjectID == projectID && assignedVolunteer.Approved == true)
                {
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        // Author: Brodie Pasker
        public List<UserProjectRole> SelectProjectRolesByUserID(int UserID)
        {
            List<UserProjectRole> roles = new List<UserProjectRole>();
            foreach (UserVM user in _userVMs)
            {
                if (user.UserID == UserID)
                {
                    roles = user.ProjectRoles;
                }
            }
            return roles;
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/02
        /// 
        /// This fake method checks to see if the UpdatePasswordHashByEmail
        /// method works correctly by looping through all of the fake users, checking if 
        /// their email and oldPassword match the typed ones and then setting the oldPasswordHash to equal the newPasswordHash 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="oldPasswordHash"></param>
        /// <param name="newPasswordHash"></param>
        /// <exception cref="ArgumentException">A message stating that the user record wasn't found will appear</exception>
        /// <returns>A one if sucessful and a 0 if not</returns>  

        public int UpdatePasswordHashByEmail(string email, string oldPasswordHash, string newPasswordHash)
        {
            int count = 0;
            for (int i = 0; i < _users.Count(); i++)
            {
                if (_users[i].Email == email && _users[i].PasswordHash == oldPasswordHash)
                {
                    _users[i].PasswordHash = newPasswordHash;
                    count++;
                }
            }
            if (count == 0)
            {
                throw new ArgumentException("The oldPasswordHash" + oldPasswordHash);
            }
            return count;
        }

        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-05
        /// Summary: This method is needed to implement the data fakes for the following method in the accessor. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public User SelectUserByInfo(string email)
        {

            User user = null;

            foreach (User u in _users)
            {
                if (u.Email == email)
                {
                    user = u;
                }
            }
            return user;
        }

        // Author: Jennifer Nicewanner
        public int DeactivateUserByUserID(int userID)
        {
            int rows = 0;

            foreach (var User in _users)
            {
                if (userID == User.UserID)
                {
                    rows++;
                }
            }
            return rows;
        }
    }
}