/// <summary>
/// Kate Rich
/// Created: 2025-02-02
///
/// Test Class for ensuring that each LocationManager method
/// returns the expected values.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/04
/// What was Changed: Added TestGetLocationListIsSuccessful()
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///     Added AddNewLocation, using Microsoft.Data.SqlClient for
///     targeted SQL exceptions 
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-24
/// What Was Changed: 
///     Added TestGetActiveLocationsList and TestDeleteLocationByID
/// </summary>

using LogicLayer;
using DataAccessFakes;
using DataDomain;
using System.Diagnostics.Metrics;
using System.Net;

namespace LogicLayerTests;

[TestClass]
public class LocationManagerTests
{
    private ILocationManager? _locationManager;

    [TestInitialize]
    public void InitializeTest()
    {
        _locationManager = new LocationManager(new LocationAccessorFake());
    }


    [TestMethod]
    // Author: Kate Rich
    public void TestGetLocationByIDReturnsCorrectLocation()
    {
        // Arrange
        const int locationID = 1;
        const string expectedName = "Strickland Propane";
        const string expectedAddress = "135 Los Gatos Road";
        const string expectedCity = "Arlen";
        const string expectedState = "Texas";
        const string expectedZip = "76001";
        const string expectedCountry = "USA";
        const string expectedDescription = "The only place to get propane and propane accessories.";
        Location location = null;
        // Act
        location = _locationManager.GetLocationByID(locationID);
        // Assert
        Assert.IsNotNull(location);
        Assert.AreEqual(expectedName, location.Name);
        Assert.AreEqual(expectedAddress, location.Address);
        Assert.AreEqual(expectedCity, location.City);
        Assert.AreEqual(expectedState, location.State);
        Assert.AreEqual(expectedZip, location.Zip);
        Assert.AreEqual(expectedCountry, location.Country);
        Assert.AreEqual(expectedDescription, location.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    // Author: Kate Rich
    public void TestGetLocationByIDThrowsApplicationExceptionForUnknownLocation()
    {
        // Arrange
        const int locationID = 38;
        Location location = null;
        // Act
        location = _locationManager.GetLocationByID(locationID);
        // Assert
        // Nothing to do - Looking for an exception.
    }

    [TestMethod]
    // Author: Stan Anderson
    public void TestGetLocationListIsSuccessful()
    {
        // arrange
        List<Location> locations;
        int expectedCount = 4;
        int actualCount = 0;
        String expectedFirstLocation = "Strickland Propane";
        String actualFirstLocation = "";
        String expectedSecondLocation = "Tom Landry Middle School";
        String actualSecondLocation = "";

        // act
        locations = _locationManager.ViewLocationList();
        actualCount = locations.Count;
        actualFirstLocation = locations[0].Name;
        actualSecondLocation = locations[1].Name;

        // assert
        Assert.AreEqual(expectedCount, actualCount);
        Assert.AreEqual(expectedFirstLocation, actualFirstLocation);
        Assert.AreEqual(expectedSecondLocation, actualSecondLocation);
    }

    [TestMethod]
    // Author: Chase Hannen
    public void TestAddNewLocationToList()
    {
        // Arrange
        const bool expectedResult = true;
        bool actualResult = false;
        // Act
        Location fakeLocation = new Location();
        fakeLocation.LocationID = 100005;
        fakeLocation.Address = "1234 Williams Blvd";
        fakeLocation.City = "Cedar Rapids";
        fakeLocation.State = "Iowa";
        fakeLocation.Zip = "52405";
        fakeLocation.Country = "USA";
        fakeLocation.Description = "Christivie's store!";
        actualResult = _locationManager.AddNewLocation(fakeLocation);
        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    // Author: Nik Bell
    [TestMethod]
    public void UpdateLocationDataFake()
    {
        // Step 1: Create a new location
        Location originalLocation = new Location() {
            LocationID = 0,
            Name = "Beach Cleanup HQ",
            Address = "123 Ocean Ave",
            City = "Miami",
            State = "FL",
            Zip = "33101",
            Description = "Initial location for testing updates."
            };
        _locationManager.AddNewLocation(originalLocation);

        // Step 2: Retrieve all locations and get the last one (assumed to be the one just added)
        List<Location> locationList = _locationManager.ViewLocationList();
        Location lastLocation = locationList.Last();
        int newLocationId = lastLocation.LocationID;

        // Step 3: Update the last location
        Location updatedLocation = new Location() {
            LocationID = newLocationId, 
            Name = "Litter Management", 
            Address = "453 Beach Blvd", 
            City = "Miami", State = "FL", Zip = "40569", Description = "Environmental nonprofit organizing beach cleanups." };
        bool result = _locationManager.UpdateLocationByID(newLocationId, updatedLocation);

        // Step 4: Assert that update was successful
        Assert.IsTrue(result, "Update operation should return true.");

        // Step 5: Retrieve the location list again and check the last entry
        List<Location> updatedList = _locationManager.ViewLocationList();
        Location lastUpdatedLocation = updatedList.Last();

        // Step 6: Assert that the last location matches the updated location
        Assert.AreEqual(updatedLocation, lastUpdatedLocation, "Updated location should match the last entry in the list.");
    }

    [TestMethod]
    // Author: Chase Hannen
    public void TestGetActiveLocationsList()
    {
        // Arrange
        List<Location> locations;
        int expectedCount = 3;
        int actualCount = 0;
        // Act
        locations = _locationManager.ViewAllActiveLocations();
        actualCount = locations.Count;
        // Assert
        Assert.AreEqual(expectedCount, actualCount);
    }

    [TestMethod]
    // Author: Chase Hannen
    public void TestDeleteLocationByID()
    {
        // Arrange
        int projectID = 44;
        int expectedDeleted = 1;
        int actualDeleted = 0;
        // Act
        bool locations = _locationManager.DeactivateLocationByLocationID(projectID);
        if (locations)
        {
            actualDeleted++;
        }
        // Assert
        Assert.AreEqual(expectedDeleted, actualDeleted);
    }
}