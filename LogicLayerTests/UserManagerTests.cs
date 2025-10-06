/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-11
/// Summary:
/// 	Test Class for ensuring that each UserManager method
/// 	returns the expected values.
/// Last Upaded By:Brodie Pasker
/// Last Updated: 2025-03-14
/// What Was Changed: Added tests for logging in and updating the user

/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-14
/// What Was Changed: Added UserTest.cs into this class
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///     Added TestGetUsersByProjectID, TestUnassignVolunteerByProjectID,
///     TestUnassignVolunteerByProjectIDThrowsApplicationExceptionForUnknownProjectID,
///     and TestUnassignVolunteerByProjectIDThrowsApplicationExceptionForUnknownUserID
///     
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025-04-11
/// What Was Changed:
///     Added TestUpdatePasswordReturnsTrueForSuccess, TestHashSHA256PasswordReturnsCorrectResult,
///     TestGetHashSHA256ThrowsAnArgumentExceptionForEmptyString, TestAuthenticateUserReturnsTrueForGoodEmailAndPassword,
///     TestAuthenticateUserReturnsFalseForBadEmailAndPassword, TestAuthenticateUserReturnsFalseForBadPassword,
///     TestAuthenticateUserReturnsFalseForInactiveUser
///     and TestGetHashSHA256ThrowsAnArgumentExceptionForNull
///     
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What Was Changed:
///     Added TestDeactivateUserByUserID()
/// </summary>

using DataAccessFakes;
using DataDomain;
using LogicLayer;
using DataDomain;

namespace LogicLayerTests;

[TestClass]
public class UserManagerTests
{
    private IUserManager? _userManager;

    [TestInitialize]
    public void InitializeTest()
    {
        _userManager = new UserManager(new UserAccessorFake());
    }

    [TestMethod]
    // Author: Kate Rich
    public void TestGetUserInformationByUserIDReturnsCorrectUser()
    {
        // Arrange
        const int userID = 4;
        const string expectedGivenName = "Hank";
        const string expectedFamilyName = "Hill";
        const string expectedPhoneNumber = "1234567890";
        const string expectedEmail = "hank@stricklandpropane.com";
        const string expectedCity = "Arlen";
        const string expectedState = "Texas";
        User user = null;
        // Act
        user = _userManager.GetUserInformationByUserID(userID);
        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual(expectedGivenName, user.GivenName);
        Assert.AreEqual(expectedFamilyName, user.FamilyName);
        Assert.AreEqual(expectedPhoneNumber, user.PhoneNumber);
        Assert.AreEqual(expectedEmail, user.Email);
        Assert.AreEqual(expectedCity, user.City);
        Assert.AreEqual(expectedState, user.State);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    // Author: Kate Rich
    public void TestGetUserInformationByUserIDThrowsApplicationExceptionForUnknownUserID()
    {
        // Arrange
        const int userID = 10000;
        User user = null;
        // Act
        user = _userManager.GetUserInformationByUserID(userID);
        // Assert
        // Nothing to do - Looking for an exception.
    }
    [TestMethod]
    public void TestUpdateUserReturnsCorrectResult()
    {
        UserVM oldUser = new UserVM()
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
            RestrictionDetails = "Lorum Ipsum."
        };
        UserVM newUser = new UserVM()
        { UserID = 1, GivenName = "Brodie", FamilyName = "Pasker", Biography = "Completion Date? Heh. When it's done.", PhoneNumber = "3194304793", Email = "brodiepasker20@gmail.com", City = "North Liberty", State = "Iowa", Image = new byte[0], ImageMimeType = "Image/jpeg", PasswordHash = "a70b6ea95832e115bb7e31e8142ffa7b38854007543e611979308d32520255d9", ReactivationDate = new DateTime(2023, 10, 10), Suspended = true, ReadOnly = false, Active = true, RestrictionDetails = "Suspended" };
        bool actualResult = false;
        bool expectedResult = true;

        actualResult = _userManager.UpdateUser(oldUser, newUser);

        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    // Author: Jennifer Nicewanner
    public void TestGetUserByEmailReturnsCorrectUser()
    {
        // arrange
        const string email = "a@test.com";
        const int expectedID = 6;
        int actualID = 0;

        //// act
        User user = _userManager.GetUserByEmail(email);
        actualID = user.UserID;
        //actualID = _userManager.GetUserByEmail(email).UserID;

        //// assert
        Assert.AreEqual(expectedID, actualID);
    }

    [TestMethod]
    // Author: Jennifer Nicewanner
    public void TestGetUserListByProjectID()
    {
        //arrange
        const int projectID = 2;
        const int expectedCount = 3;
        int actualCount = 0;

        //act
        actualCount = _userManager.GetApprovedUserByProjectID(projectID).Count;

        //assert
        Assert.AreEqual(expectedCount, actualCount);
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to leave the password box blank 
    /// when submitting.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestPasswordCannotBeBlankReturnsFalse()
    {
        const string givenName = "Test5";
        const string familyName = "Test5";
        const string phoneNumber = "1234567890";
        const string email = "test5@test.com";
        const string password = "";

        _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to enter a password that is too short.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestPasswordLengthTooShortReturnsFalse()
    {
        const string givenName = "Test1";
        const string familyName = "Test1";
        const string phoneNumber = "1234567890";
        const string email = "test1@test.com";
        const string password = "short";

        _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to enter a password that is too long.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestPasswordLengthTooLongReturnsFalse()
    {
        const string givenName = "Test1";
        const string familyName = "Test1";
        const string phoneNumber = "1234567890";
        const string email = "test1@test.com";
        const string password = "tooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooolong";

        _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");
    }

    [TestMethod]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that the test creates the user's
    /// account when the password length is valid.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestPasswordLengthValidReturnsTrue()
    {
        const string givenName = "Test5";
        const string familyName = "Test5";
        const string phoneNumber = "1234567890";
        const string email = "test5@test.com";
        const string password = "validpassword";

        bool actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");

        Assert.IsTrue(actualResult, "Expected the user insertion to succeed.");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts leave the email blank.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestEmailCannotBeBlankReturnsFalse()
    {
        const string givenName = "Test5";
        const string familyName = "Test5";
        const string phoneNumber = "1234567890";
        const string email = "";
        const string password = "password123";

        _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to leave the phone number box blank.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestPhoneNumberCannotBeBlankReturnsFalse()
    {
        const string givenName = "Test5";
        const string familyName = "Test5";
        const string phoneNumber = "";
        const string email = "test5@test.com";
        const string password = "password123";

        _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts leave the family name box blank.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestFamilyNameCannotBeBlankReturnsFalse()
    {
        const string givenName = "Test5";
        const string familyName = "";
        const string phoneNumber = "1234567890";
        const string email = "test5@test.com";
        const string password = "password123";

        _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to leave the given name box blank.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestGivenNameCannotBeBlankReturnsFalse()
    {
        const string givenName = "";
        const string familyName = "Test5";
        const string phoneNumber = "1234567890";
        const string email = "test5@test.com";
        const string password = "password123";

        _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to enter a email that is too long.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestEmailLengthTooLongReturnsFalse()
    {
        const string givenName = "Test5";
        const string familyName = "Test5";
        const string phoneNumber = "1234567890";
        const string email = "ajdlsafjdslfjkadsjflsdjflkadsjflkjdsalkfjdklsjflkdsjafkldsjfkljdsaljfljskalfjdskljfkldjsafkldsjlkfjadsklfjdasklfjkdsajfdsklajfkldsjfkldjsaflkdsjfkladsjfkljdsklfjdslkajfkldsjfakljdsflkjklajdsfkdlsajfkldsjfkldjsafkljdsklfjaskldfjkladjfkljakldsjfkldsajfkljkalsjdfkljas@gmail.com";
        const string passwordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
        const bool expectedResult = true;
        bool actualResult;


        actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, passwordHash, new byte[0], "");
    }
    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to enter a family name that is too long.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestFamilyNameLengthTooLongReturnsFalse()
    {
        const string givenName = "John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt";
        const string familyName = "Test5";
        const string phoneNumber = "1234567890";
        const string email = "test5@test.com";
        const string passwordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
        const bool expectedResult = true;
        bool actualResult;


        actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, passwordHash, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to enter a given name that is too long.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestGivenNameLengthTooLongReturnsFalse()
    {
        const string givenName = "Test1";
        const string familyName = "John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt, John Jacob Jingleheimer Schmidt";
        const string phoneNumber = "1234567890";
        const string email = "test5@test.com";
        const string passwordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
        const bool expectedResult = true;
        bool actualResult;


        actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, passwordHash, new byte[0], "");
    }

    [TestMethod]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that a user account is created
    /// when all user information is valid.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestAddUserShouldReturnTrue()
    {
        const string givenName = "Test5";
        const string familyName = "Test5";
        const string phoneNumber = "1234567890";
        const string email = "test5@test.com";
        const string passwordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
        const bool expectedResult = true;
        bool actualResult;


        actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, passwordHash, new byte[0], "");

        Assert.AreEqual(expectedResult, actualResult, "Expected the user insertion to succeed.");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to enter a email that is already in use.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestEmailAlreadyExistsReturnFalse()
    {
        const string givenName = "Test1";
        const string familyName = "Test1";
        const string phoneNumber = "1234567890";
        const string email = "test1@test.com";
        const string passwordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
        const bool expectedResult = true;
        bool actualResult;

        actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, passwordHash, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys that an Application Exception is 
    /// thrown when the user attempts to enter a phone number length that is 
    /// too long.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestPhoneNumberLengthTooLongReturnsFalse()
    {
        const string givenName = "Test1";
        const string familyName = "Test1";
        const string phoneNumber = "123456789";
        const string email = "test1@test.com";
        const string passwordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
        const bool expectedResult = true;
        bool actualResult;

        actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, passwordHash, new byte[0], "");
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: The unit test verifys the user's account
    /// is created when the email entered is create
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestEmailEnteredCorrectlyReturnsFalse()
    {
        const string givenName = "Test1";
        const string familyName = "Test1";
        const string phoneNumber = "1234567890";
        const string email = "test1@";
        const string passwordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
        const bool expectedResult = true;
        bool actualResult;

        actualResult = _userManager.InsertUser(givenName, familyName, phoneNumber, email, passwordHash, new byte[0], "");
    }

    [TestMethod]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: This test verifies that DoesEmailExist returns true for an existing user
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestSelectUserByEmailReturnsTrueForExistingUser()
    {
        const string email = "test1@test.com";
        bool userFound = _userManager.DoesEmailExist(email);
        Assert.IsTrue(userFound, "Expected SelectUserByEmail to return true for an existing user.");
    }

    [TestMethod]
    ///<summary>
    /// Creator: Skyann Heintz
    /// Created: 2025/02/02
    /// Summary: This test verifies that DoesEmailExist returns false when a user 
    /// with that email does not exist.
    /// Last Updated By:
    /// Last Updated:
    /// What Was Changed:
    /// </summary>
    public void TestSelectUserByEmailReturnsFalseForNonExistingUser()
    {
        const string email = "nonexistent@test.com";
        bool userFound = _userManager.DoesEmailExist(email);
        Assert.IsFalse(userFound, "Expected SelectUserByEmail to return false for a non-existing user.");
    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/02/03
    /// 
    /// Returns True if the amount of users matches the expected value
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name: Dat Tran
    /// Updated: 2025/04/19
    /// What was changed: Added 3 to the 12 expected users for the tests from the now removed ProfileManager Tests. 
    /// </remarks>
    [TestMethod]
    public void TestGetAllUsers()
    {
        const int expectedValue = 15;

        int actualValue = _userManager.GetAllUsers().Count();

        Assert.AreEqual(expectedValue, actualValue);
    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/02/11
    /// 
    /// Tests DeactivateUser With Correct Password
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    [TestMethod]
    public void TestDeactivateUser()
    {
        string email = "b@test.com";
        string password = "newuser";

        Assert.IsTrue(_userManager.DeactivateUser(email, password));
    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/02/11
    /// 
    /// Tests DeactivateUser With Incorrect Password
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    [TestMethod]
    public void TestDeactivateUserFailureWithBadPassword()
    {
        string email = "b@test.com";
        string password = "bad password";

        Assert.IsFalse(_userManager.DeactivateUser(email, password));
    }

    [TestMethod]
    // Author: Chase Hannen
    public void TestGetUsersByProjectID()
    {
        // Arrange
        const int projectID = 2;
        const int expectedCount = 3;
        int actualCount = 0;
        // Act
        actualCount = _userManager.GetUsersByProjectID(projectID).Count();
        // Assert
        Assert.AreEqual(expectedCount, actualCount);
    }

    [TestMethod]
    // Author: Chase Hannen
    public void TestUnassignVolunteerByProjectID()
    {
        // Arrange
        const int projectID = 2;
        const int userID = 7;
        bool expectedResult = true;
        bool actualResult = false;
        // Act
        actualResult = _userManager.UnassignVolunteerByProject(userID, projectID);
        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    // Author: Chase Hannen
    public void TestUnassignVolunteerByProjectIDThrowsApplicationExceptionForUnknownProjectID()
    {
        // Arrange
        const int projectID = 22;
        const int userID = 7;
        bool expectedResult = true;
        bool actualResult = true;
        // Act
        actualResult = _userManager.UnassignVolunteerByProject(userID, projectID);
        // Assert - Expecting Inequality
        Assert.AreNotEqual(expectedResult, actualResult);
    }

    [TestMethod]
    // Author: Chase Hannen
    public void TestUnassignVolunteerByProjectIDThrowsApplicationExceptionForUnknownUserID()
    {
        // Arrange
        const int projectID = 2;
        const int userID = 77;
        bool expectedResult = true;
        bool actualResult = true;
        // Act
        actualResult = _userManager.UnassignVolunteerByProject(userID, projectID);
        // Assert - Expecting Inequality
        Assert.AreNotEqual(expectedResult, actualResult);
    }

    [TestMethod]
    // Author: Brodie Pasker
    public void TestGetProjectRolesByUserIDReturnsProperRoles()
    {
        // Arrange
        const int UserID = 1;
        List<UserProjectRole> expectedResult = new List<UserProjectRole>();
        expectedResult.Add(new UserProjectRole()
        {
            ProjectId = 1,
            ProjectRole = "Manager"
        });
        expectedResult.Add(new UserProjectRole()
        {
            ProjectId = 1,
            ProjectRole = "Accountant"
        });
        expectedResult.Add(new UserProjectRole()
        {
            ProjectId = 2,
            ProjectRole = "Volunteer"
        });
        expectedResult.Add(new UserProjectRole()
        {
            ProjectId = 2,
            ProjectRole = "Janitor"
        });
        List<UserProjectRole> actualResult = new List<UserProjectRole>();

        // Act
        actualResult = _userManager.GetProjectRolesByUserID(UserID);

        // Assert - Expecting Equality
        Assert.AreEqual(expectedResult.Count, actualResult.Count);
    }

    [TestMethod]
    // Author: Brodie Pasker
    public void TestGetProjectRolesByUserIDReturnsZeroRoles()
    {
        // Arrange
        const int UserID = 2;
        List<UserProjectRole> expectedResult = new List<UserProjectRole>();
        List<UserProjectRole> actualResult = new List<UserProjectRole>();

        // Act
        actualResult = _userManager.GetProjectRolesByUserID(UserID);

        // Assert - Expecting Equality
        Assert.AreEqual(expectedResult.Count, actualResult.Count);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method checks if updating the password will return true if it works
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    [TestMethod]
    public void TestUpdatePasswordReturnsTrueForSuccess()
    {
        //arrange
        const string email = "test1@test.com";
        const string oldPassword = "password";
        const string newPassword = "newpassword";
        const bool expectedResult = true;
        bool actualResult = false;

        //act
        actualResult = _userManager.UpdatePassword(email, oldPassword, newPassword);

        //assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method makes sure that the HashSHA256 returns the desired password
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    [TestMethod]
    public void TestHashSHA256PasswordReturnsCorrectResult()
    {
        // Arrange
        const string valueToHash = "newuser";
        const string expectedHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
        var actualHash = "";

        //act
        actualHash = _userManager.HashSHA256(valueToHash);

        //assert
        Assert.AreEqual(expectedHash, actualHash);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method makes sure that the HashSHA256 will not accept empty strings
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetHashSHA256ThrowsAnArgumentExceptionForEmptyString()
    {
        // arrange
        const string valueToHash = "";

        //act
        _userManager.HashSHA256(valueToHash);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method makes sure that the HashSHA256 throws an exception for nulls
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetHashSHA256ThrowsAnArgumentExceptionForNull()
    {
        // arrange
        const string valueToHash = null;

        //act
        _userManager.HashSHA256(valueToHash);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method makes sure that AuthenticateUser returns true for a good email and password
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    [TestMethod]
    public void TestAuthenticateUserReturnsTrueForGoodEmailAndPassword()
    {
        //arrange 
        const string email = "test1@test.com";
        const string password = "password";
        const bool expectedResult = true;
        bool actualResult = false;

        //act
        actualResult = _userManager.AuthenticateUser(email, password);

        //assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method makes sure that AuthenticateUser returns false for a bad email and password
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    [TestMethod]
    public void TestAuthenticateUserReturnsFalseForBadEmailAndPassword()
    {
        //arrange 
        const string email = " bad1@test.com";
        const string password = "password";
        const bool expectedResult = false;
        bool actualResult = true;

        //act
        actualResult = _userManager.AuthenticateUser(email, password);

        //assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method makes sure that AuthenticateUser returns false for a bad password
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    [TestMethod]
    public void TestAuthenticateUserReturnsFalseForBadPassword()
    {
        //arrange 
        const string email = " bad1@test.com";
        const string password = "badpassword";
        const bool expectedResult = false;
        bool actualResult = true;

        //act
        actualResult = _userManager.AuthenticateUser(email, password);

        //assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/02
    /// 
    /// This test method makes sure that AuthenticateUser returns false for an inactive user
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    [TestMethod]
    public void TestAuthenticateUserReturnsFalseForInactiveUser()
    {
        //arrange 
        const string email = "test3@test.com";
        const string password = "password";
        const bool expectedResult = false;
        bool actualResult = true;

        //act
        actualResult = _userManager.AuthenticateUser(email, password);

        //assert
        Assert.AreEqual(expectedResult, actualResult);
    }
    //CREATOR: Dat Tran
    [TestMethod]
    public void TestEmailReturnProfile()
    {
        const string email = "test1@test.com";
        const string expectedGivenName = "Something";
        string actualGivenName = "";
        User user = _userManager.GetUsersByInfo(email);
        actualGivenName = user.GivenName;
        Assert.AreEqual(expectedGivenName, actualGivenName);


    }
    //CREATOR: Dat Tran
    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEmailReturnNoProfile()
    {
        const string email = "test4@test.com";
        const string expectedGivenName = "Something";
        string actualGivenName = "";
        User user = _userManager.GetUsersByInfo(email);
        actualGivenName = user.GivenName;
    }

    [TestMethod]
    // Author: Jennifer Nicewanner
    public void TestDeactivateUserByUserID()
    {
        // arrange
        const int userID = 6;
        bool expectedResult = true;
        bool actualResult = false;

        //// act
        actualResult = _userManager.DeactivateUserByUserID(userID);

        //// assert
        Assert.AreEqual(expectedResult, actualResult);
    }
}