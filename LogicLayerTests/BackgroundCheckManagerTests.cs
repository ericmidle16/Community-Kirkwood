/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-10
/// Summary:
///		Test Class for ensuring that each BackgroundCheckManager method
/// 	returns the expected values.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-20
/// What Was Changed:
///     Added the test for selecting the correct list of background checks associated with a project.
///     
/// Updated By: Kate Rich
/// Updated: 2025-02-26
/// What Was Changed:
///     Added the test for updating an existing BackgroundCheck record.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-27
/// What Was Changed:
///     Added the test for viewing a single BackgroundCheck record -
///     TestGetBackgroundCheckByIDReturnsCorrectBackgroundCheck &
///     TestGetBackgroundCheckByIDThrowsApplicationExceptionForUnknownBackgroundCheckID.
/// </summary>

using DataAccessFakes;
using DataDomain;
using LogicLayer;

namespace LogicLayerTests;

[TestClass]
public class BackgroundCheckManagerTests
{
    private IBackgroundCheckManager? _backgroundCheckManager;

    [TestInitialize]
    public void InitializeTest()
    {
        _backgroundCheckManager = new BackgroundCheckManager(new BackgroundCheckAccessorFake());
    }

    [TestMethod]
    // Author: Kate Rich
    public void TestAddBackgroundCheckReturnsTrueForSuccessfulAdd()
    {
        // Arrange
        BackgroundCheck bgc = new BackgroundCheck()
        {
            Investigator = 12,
            UserID = 5,
            ProjectID = 3,
            Status = "In Progress",
            Description = "adfkoiewkdj"
        };

        const bool expectedValue = true; // Supposed to pass
        bool actualValue = false;

        // Act
        actualValue = _backgroundCheckManager.AddBackgroundCheck(bgc);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    // Author: Kate Rich
    public void TestGetBackgroundChecksByProjectIDReturnsCorrectList()
    {
        // Arrange
        const int projectID = 1;
        const int expectedBackgroundCheckCount = 2;
        int actualBackgroundCheckCount = 0;
        // Act
        actualBackgroundCheckCount = _backgroundCheckManager.GetBackgroundChecksByProjectID(projectID).Count;
        // Assert
        Assert.AreEqual(expectedBackgroundCheckCount, actualBackgroundCheckCount);
    }

    [TestMethod]
    public void TestEditBackgroundCheckReturnsTrueForSuccessfulUpdate()
    {
        // Arrange
        BackgroundCheck oldBGC = new BackgroundCheck()
        {
            BackgroundCheckID = 1,
            Investigator = 10,
            UserID = 7,
            ProjectID = 1,
            Status = "In Progress",
            Description = ""
        };
        BackgroundCheck newBGC = new BackgroundCheck()
        {
            BackgroundCheckID = 1,
            Investigator = 10,
            UserID = 7,
            ProjectID = 1,
            Status = "Passed",
            Description = "Yay, they passed!"
        };

        const bool expectedValue = true; // Supposed to Pass
        bool actualValue = false;

        // Act
        actualValue = _backgroundCheckManager.EditBackgroundCheck(oldBGC, newBGC);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    // Author: Kate Rich
    public void TestGetBackgroundCheckByIDReturnsCorrectBackgroundCheck()
    {
        // Arrange
        const int backgroundCheckID = 5;
        const int expectedInvestigatorID = 1;
        const string expectedInvestigatorGivenName = "Hank";
        const string expectedInvestigatorFamilyName = "Hill";
        const int expectedUserID = 3;
        const string expectedVolunteerGivenName = "Joseph";
        const string expectedVolunteerFamilyName = "Gribble";
        const int expectedProjectID = 1;
        const string expectedProjectName = "Propane Delivery to TLMS";
        const string expectedStatus = "Failed";
        const string expectedDescription = "Joseph is a child in middle school. He is not allowed to help transport propane. It's too dangerous.";
        BackgroundCheckVM backgroundCheck = null;
        // Act
        backgroundCheck = _backgroundCheckManager.GetBackgroundCheckByID(backgroundCheckID);
        // Assert
        // Pulling all actual values from the BackgroundCheck object.
        Assert.AreEqual(expectedInvestigatorID, backgroundCheck.Investigator);
        Assert.AreEqual(expectedInvestigatorGivenName, backgroundCheck.InvestigatorGivenName);
        Assert.AreEqual(expectedInvestigatorFamilyName, backgroundCheck.InvestigatorFamilyName);
        Assert.AreEqual(expectedUserID, backgroundCheck.UserID);
        Assert.AreEqual(expectedVolunteerGivenName, backgroundCheck.VolunteerGivenName);
        Assert.AreEqual(expectedVolunteerFamilyName, backgroundCheck.VolunteerFamilyName);
        Assert.AreEqual(expectedProjectID, backgroundCheck.ProjectID);
        Assert.AreEqual(expectedProjectName, backgroundCheck.ProjectName);
        Assert.AreEqual(expectedStatus, backgroundCheck.Status);
        Assert.AreEqual(expectedDescription, backgroundCheck.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    // Author: Kate Rich
    public void TestGetBackgroundCheckByIDThrowsApplicationExceptionForUnknownBackgroundCheckID()
    {
        // Arrange
        const int backgroundCheckID = 100;
        BackgroundCheckVM backgroundCheck = null;
        // Act
        backgroundCheck = _backgroundCheckManager.GetBackgroundCheckByID(backgroundCheckID);
        // Assert
        // Nothing to do - Looking for an exception.
    }
}