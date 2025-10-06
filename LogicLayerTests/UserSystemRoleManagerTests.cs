
using DataAccessFakes;
using DataDomain;
using LogicLayer;

/// <summary>
/// Ellie Wacker
/// Created: 2025-03-05
/// 
/// Test Class for ensuring that each UserSystemRoleManager method
/// returns the expected values.
/// </summary>
/// 

namespace LogicLayerTests;

[TestClass]
public class UserSystemRoleManagerTests
{
    private IUserSystemRoleManager? _userSystemRoleManager;

    [TestInitialize]
    public void InitializeTest()
    {
        _userSystemRoleManager = new UserSystemRoleManager(new UserSystemRoleAccessorFake());
    }

    [TestMethod]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025-03-05
    /// 
    /// The test method that tests if the insert succeeds the UserSystemRole is good
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertUserSystemRoleSucceedsForGoodRole()
    {
        // Arrange
        const int userID = 101;
        const string systemRoleID = "Driver";

        // Act
        const int expectedUserID = 1;

        int actualUserID = 0;

        actualUserID = _userSystemRoleManager.InsertUserSystemRole(userID, systemRoleID);
        // Assert
        Assert.AreEqual(expectedUserID, actualUserID);
    }

    [TestMethod]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/21
    /// 
    /// The test method that tests if retrieving a UserSystemRole with a certain user id gets the correct list
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestRetrieveUserSystemRolesByUserIDReturnsCorrectUserSystemRoles()
    {

        // arrange 
        const int expectedUserID = 100000;

        // act
        List<UserSystemRole> userSystemRoles = _userSystemRoleManager.GetUserSystemRolesByUserID(expectedUserID);

        // assert
        foreach (var userSystemRole in userSystemRoles)
        {
            Assert.AreEqual(expectedUserID, userSystemRole.UserID);
        }
    }
    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/03/06
    /// 
    /// The test method that tests if retrieving an empty list of userSystemRoles throws an exception
    /// </summary>
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestRetrieveUserSystemRolesByUserIDReturnsFalseForEmptyList()
    {
        // arrange 
        const int expectedUserID = 10000000;

        // act
        List<UserSystemRole> userSystemRoles = _userSystemRoleManager.GetUserSystemRolesByUserID(expectedUserID);

        // if no vehicles are returned, the exception should be thrown
        if (userSystemRoles.Count == 0)
        {
            throw new ApplicationException("No userSystemRoles found for the provided UserID");
        }

        // assert - if vehicles are returned, we check the UserID
        foreach (var userSystemRole in userSystemRoles)
        {
            Assert.AreEqual(expectedUserID, userSystemRole.UserID);
        }
    }
}