/// <summary>
/// Creator: Kate Rich
/// Created: 2025-03-03
/// Summary:
///     Test Class for ensuring that each DonationManger method
///     returns the expected values.
/// 
/// Updated By : Christivie Mauwa
/// Updated : 2025-03-28
/// What was changed : Added test for TestAddDonation, TestGetDonationByID, TestGetAllDonation,TestGetDonationByDonationID
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What Was Changed:
///     Added TestViewDonationsReturnTrueIfProjectIDIsCorrect & TestViewDonationsReturnFalseIfProjectIDIsNotcoreect.
///     
/// </summary>

using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using LogicLayer;

namespace LogicLayerTests;

[TestClass]
public class DonationManagerTests
{
    private IDonationManager? _donationManager;
    List<DonationVM> donationVMs;

    [TestInitialize]
    public void InitializeTest()
    {
        _donationManager = new DonationManager(new DonationAccessorFake());
    }

    [TestMethod]
    // Author: Kate Rich
    public void TestGetMonetaryProjectDonationSummariesByUserIDReturnsCorrectList()
    {
        // Arrange
        const int userID = 3;
        const int expectedListCount = 2;
        const decimal expectedTotalDonationsForProjectID4 = 10.00m;
        DateTime expectedLastDonationDateForProjectID4 = new DateTime(2025, 1, 31);
        const decimal expectedTotalDonationsForProjectID5 = 225.00m;
        DateTime expectedLastDonationDateForProjectID5 = new DateTime(2025, 3, 1);

        // Act
        var donationSummaries = _donationManager.GetMonetaryProjectDonationSummariesByUserID(userID);

        int actualListCount = donationSummaries.Count;
        decimal actualTotalDonationsForProjectID4 = donationSummaries[0].MonetaryDonationTotal; // Lowest projectID will always be first.
        DateTime actualLastDonationDateForProjectID4 = donationSummaries[0].LastDonationDate;
        decimal actualTotalDonationsForProjectID5 = donationSummaries[1].MonetaryDonationTotal;
        DateTime actualLastDonationDateForProjectID5 = donationSummaries[1].LastDonationDate;

        // Assert
        Assert.AreEqual(expectedListCount, actualListCount);
        Assert.AreEqual(expectedTotalDonationsForProjectID4, actualTotalDonationsForProjectID4);
        Assert.AreEqual(expectedLastDonationDateForProjectID4, actualLastDonationDateForProjectID4);
        Assert.AreEqual(expectedTotalDonationsForProjectID5, actualTotalDonationsForProjectID5);
        Assert.AreEqual(expectedLastDonationDateForProjectID5, actualLastDonationDateForProjectID5);
    }

    [TestMethod]
    public void TestAddDonation()
    {
        // arrange
        var newDonation = new Donation()
        {
            DonationID = 100007,
            DonationType = "type",
            UserID = 100003,
            ProjectID = 100005,
            Amount = 40m,
            DonationDate = DateTime.Now,
            Description = "Test Donation"
        };
        //act
        bool result = _donationManager.AddDonation(newDonation);
        //assert
        Assert.IsTrue(result, "One donation added");
    }

    // author : Christivie
    [TestMethod]
    public void TestGetAllDonation()
    {
        // arrange
        int expectedCount = 2;
        // Act
        donationVMs = _donationManager.GetAllDonation();
        //assert
        Assert.AreEqual(expectedCount, donationVMs.Count);
    }

    // Author : Christivie
    [TestMethod]
    public void TestGetDonationByDonationID()
    {
        // arrange
        int expectedID = 100003;
        //// act
        var result = _donationManager.RetrieveDonationByDonationID(expectedID);
        ////assert
        Assert.AreEqual(expectedID, result.DonationID);
    }
    // Author: Christivie
    [TestMethod]
    public void TestGetDonationByUser()
    {
        // arrange
        int userID = 100002;
        // act
        var result = _donationManager.RetrieveDonationByUserId(userID);
        // assert
        Assert.IsTrue(result.All(d => d.UserID == userID));
    }

    [TestMethod]
    //Author: Akoi Kollie
    public void TestViewDonationsReturnTrueIfProjectIDIsCorrect()
    {
        //Arrange
        int numberdonations = 2;
        List<Donation> donations = _donationManager.SelectToViewDonations(3);
        //Assert
        Assert.AreEqual(numberdonations, donations.Count);
    }
    [TestMethod]
    //Author:Akoi Kollie
    public void TestViewDonationsReturnFalseIfProjectIDIsNotcoreect()
    {
        //Arrange
        int numberdonations = 2;
        List<Donation> donations = _donationManager.SelectToViewDonations(1000003);
        //Assert
        Assert.IsFalse(numberdonations == donations.Count);
    }
}