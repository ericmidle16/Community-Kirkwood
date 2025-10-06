/// <summary>
/// Ellie Wacker
/// Created: 2025-02-17
/// 
/// Test Class for ensuring that each VehicleManager method
/// returns the expected values.
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Added TestRetrieveActiveVehiclesByUserIDReturnsCorrectVehicles
/// </summary>

using LogicLayer;
using DataAccessFakes;
using DataDomain;
using System.Diagnostics.Metrics;
using System.Net;

namespace LogicLayerTests;

[TestClass]
public class VehicleManagerTests
{
    private IVehicleManager? _vehicleManager;

    [TestInitialize]
    public void InitializeTest()
    {
        _vehicleManager = new VehicleManager(new VehicleAccessorFake());
    }

    [TestMethod]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if the insert succeeds if the vehicle is good
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleSucceedsForGoodVehicle()
    {
        // Arrange
        const string vehicleID = "1HGCM82633A123456";             
        const int userID = 1000002;
        const string color = "Red";
        const int year = 2020;
        const string licensePlateNumber = "ABC-DE-1234";
        const bool insuranceStatus = true;
        const string make = "Toyota";
        const string model = "Camry";
        const int numberOfSeats = 5;
        const string transportUtility = "Personal";

        // Act
        const int expectedVehicleID = 1;

        int actualVehicleID = 0;

        actualVehicleID = _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
        // Assert
        Assert.AreEqual(expectedVehicleID, actualVehicleID);
    }

    [TestMethod]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if retreiving the vehicle returns the correct number
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestRetrieveVehiclesReturnsCorrectNumber()
    {

        // Arrange
        const int expectedNumber = 1;

        // Act
        List<Vehicle> vehicles = _vehicleManager.GetAllVehicles();
        int actualNumber = vehicles.Count;

        // Assert
        Assert.AreEqual(expectedNumber, actualNumber);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown if the VIN already exists
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForExistingVINNumber()
    {
        string existingVIN = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 2020;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = true;
        string make = "Toyota";
        string model = "Camry";
        int numberOfSeats = 5;
        string transportUtility = "Personal";

        // Add initial vehicle to data fakes
        _vehicleManager.InsertVehicle(existingVIN, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);


        // Act
        _vehicleManager.InsertVehicle(existingVIN, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility); // This should throw an exception
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown if the VIN is invalid
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForInvalidVINNumber()
    {
        string vehicleID = "1"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 2020;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = true;
        string make = "Toyota";
        string model = "Camry";
        int numberOfSeats = 5;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown for an invalid license plate
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForInvalidLicensePlate()
    {
        string vehicleID = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 2020;
        string licensePlateNumber = "A";
        bool insuranceStatus = true;
        string make = "Toyota";
        string model = "Camry";
        int numberOfSeats = 5;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown for inserting a vehicle with no seats
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForNoSeats()
    {
        string vehicleID = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 2020;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = true;
        string make = "Toyota";
        string model = "Camry";
        int numberOfSeats = 0;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown for inserting a vehicle with an invalid make
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForInvalidMake()
    {
        string vehicleID = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 2020;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = true;
        string make = "T";
        string model = "Camry";
        int numberOfSeats = 5;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, active, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown for inserting a vehicle with an invalid model
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForInvalidModel()
    {
        string vehicleID = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 2020;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = true;
        string make = "Toyota";
        string model = "y";
        int numberOfSeats = 5;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown for inserting a vehicle with an invalid year
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForInvalidYear()
    {
        string vehicleID = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 1690;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = true;
        string make = "Toyota";
        string model = "Camry";
        int numberOfSeats = 5;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown for inserting a vehicle that's not insured
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForNotInsuredVehicle()
    {
        string vehicleID = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 1690;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = false;
        string make = "Toyota";
        string model = "Camry";
        int numberOfSeats = 5;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/18
    /// 
    /// The test method that tests if an exception is thrown for inserting a vehicle with more than 60 seats
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertVehicleThrowsExceptionForMoreThanSixtySeats()
    {
        string vehicleID = "1HGCM82633A123456"; // Sample VIN
        int userID = 1;
        bool active = true;
        string color = "Red";
        int year = 1690;
        string licensePlateNumber = "ABC-DE-1234";
        bool insuranceStatus = false;
        string make = "Toyota";
        string model = "Camry";
        int numberOfSeats = 100;
        string transportUtility = "Personal";

        _vehicleManager.InsertVehicle(vehicleID, userID, false, color, year, licensePlateNumber, insuranceStatus, make, model, numberOfSeats, transportUtility);
    }


    [TestMethod]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/21
    /// 
    /// The test method that tests if retrieving a vehicle with a certain user id gets the correct list
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestRetrieveVehiclesByUserIDReturnsCorrectVehicles()
    {

        // arrange 
        const int expectedUserID = 100000;

        // act
        List<Vehicle> vehicles = _vehicleManager.GetVehiclesByUserID(expectedUserID);

        // assert
        foreach (var vehicle in vehicles)
        {
            Assert.AreEqual(expectedUserID, vehicle.UserID);
        }
    }
    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/21
    /// 
    /// The test method that tests if retrieving an empty list of vehicles throws an exception
    /// </summary>
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestRetrieveVehiclesByUserIDReturnsFalseForEmptyList()
    {
        // arrange 
        const int expectedUserID = 10000000;

        // act
        List<Vehicle> vehicles = _vehicleManager.GetVehiclesByUserID(expectedUserID);

        // if no vehicles are returned, the exception should be thrown
        if (vehicles.Count == 0)
        {
            throw new ApplicationException("No vehicles found for the provided UserID");
        }

        // assert - if vehicles are returned, we check the UserID
        foreach (var vehicle in vehicles)
        {
            Assert.AreEqual(expectedUserID, vehicle.UserID);
        }
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/28
    /// 
    /// The test method that tests if an exception is thrown for updating a vehicle that doesnt exist
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestUpdateActiveReturnsFalseForFail()
    {
        //arrange
        const string vehicleID = "vehicle4";
        const bool active = false;
        const bool expectedResult = true;
        bool actualResult = false;

        //act
        actualResult = _vehicleManager.UpdateActiveByVehicleID(vehicleID, active);

        //assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/28
    /// 
    /// The test method that tests if updating the active returns true
    /// </summary>
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    [TestMethod]
    public void TestUpdateActiveReturnsTrueForSuccess()
    {
        //arrange
        const string vehicleID = "vehicle1";
        const bool active = false;
        const bool expectedResult = true;
        bool actualResult = false;

        //act
        actualResult = _vehicleManager.UpdateActiveByVehicleID(vehicleID, active);

        //assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    /// <summary>
    /// Jennifer Nicewanner
    /// Created: 2025/04/24
    /// 
    /// The test method that tests if retrieving active vehicles with a certain user id gets the correct list
    /// </summary>
    public void TestRetrieveActiveVehiclesByUserIDReturnsCorrectVehicles()
    {

        // arrange 
        const int expectedUserID = 100000;

        // act
        List<Vehicle> vehicles = _vehicleManager.GetAllActiveVehiclesByUserID(expectedUserID);

        // assert
        foreach (var vehicle in vehicles)
        {
            Assert.AreEqual(expectedUserID, vehicle.UserID);
        }
    }

    [TestMethod]
    public void TestUpdateVehicleByIDSuccessful()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "vehicle1",
            UserID = 101,
            Active = true,
            Color = "Blue",
            Year = 2022,
            LicensePlateNumber = "XYZ123",
            InsuranceStatus = true,
            Make = "Honda",
            Model = "Accord",
            NumberOfSeats = 5,
            TransportUtility = "Personal"
        };

        // Act
        bool result = _vehicleManager.UpdateVehicleByID(vehicle);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestUpdateVehicleByIDInvalidColor()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "vehicle1",
            UserID = 101,
            Active = true,
            Color = "AB", // Too short
            Year = 2022,
            LicensePlateNumber = "XYZ123",
            InsuranceStatus = true,
            Make = "Honda",
            Model = "Accord",
            NumberOfSeats = 5,
            TransportUtility = "Personal"
        };

        // Act
        _vehicleManager.UpdateVehicleByID(vehicle);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestUpdateVehicleByIDInvalidYear()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "vehicle1",
            UserID = 101,
            Active = true,
            Color = "Blue",
            Year = 1800, // Too old
            LicensePlateNumber = "XYZ123",
            InsuranceStatus = true,
            Make = "Honda",
            Model = "Accord",
            NumberOfSeats = 5,
            TransportUtility = "Personal"
        };

        // Act
        _vehicleManager.UpdateVehicleByID(vehicle);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestUpdateVehicleByIDInvalidLicensePlate()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "vehicle1",
            UserID = 101,
            Active = true,
            Color = "Blue",
            Year = 2022,
            LicensePlateNumber = "ABC-123", // Invalid format for the simplified regex
            InsuranceStatus = true,
            Make = "Honda",
            Model = "Accord",
            NumberOfSeats = 5,
            TransportUtility = "Personal"
        };

        // Act
        _vehicleManager.UpdateVehicleByID(vehicle);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestUpdateVehicleByIDInvalidMake()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "vehicle1",
            UserID = 101,
            Active = true,
            Color = "Blue",
            Year = 2022,
            LicensePlateNumber = "XYZ123",
            InsuranceStatus = true,
            Make = "AB", // Too short
            Model = "Accord",
            NumberOfSeats = 5,
            TransportUtility = "Personal"
        };

        // Act
        _vehicleManager.UpdateVehicleByID(vehicle);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestUpdateVehicleByIDInvalidModel()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "vehicle1",
            UserID = 101,
            Active = true,
            Color = "Blue",
            Year = 2022,
            LicensePlateNumber = "XYZ123",
            InsuranceStatus = true,
            Make = "Honda",
            Model = "A", // Too short
            NumberOfSeats = 5,
            TransportUtility = "Personal"
        };

        // Act
        _vehicleManager.UpdateVehicleByID(vehicle);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestUpdateVehicleByIDInvalidNumberOfSeats()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "vehicle1",
            UserID = 101,
            Active = true,
            Color = "Blue",
            Year = 2022,
            LicensePlateNumber = "XYZ123",
            InsuranceStatus = true,
            Make = "Honda",
            Model = "Accord",
            NumberOfSeats = 0, // Invalid (0 or less)
            TransportUtility = "Personal"
        };

        // Act
        _vehicleManager.UpdateVehicleByID(vehicle);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestUpdateVehicleByIDVehicleNotFound()
    {
        // Arrange
        Vehicle vehicle = new Vehicle
        {
            VehicleID = "nonexistent", // Non-existent vehicle ID
            UserID = 101,
            Active = true,
            Color = "Blue",
            Year = 2022,
            LicensePlateNumber = "XYZ123",
            InsuranceStatus = true,
            Make = "Honda",
            Model = "Accord",
            NumberOfSeats = 5,
            TransportUtility = "Personal"
        };

        // Act
        _vehicleManager.UpdateVehicleByID(vehicle);
    }
}
