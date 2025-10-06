/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the method for testing the notification is send or not
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using DataAccessFakes;
using DataDomain;
using LogicLayer;

namespace LogicLayerTests;

[TestClass]
public class NotificationLogicLayerTest
{
    private INotificationManager? _notificationManager;

    [TestInitialize]
    public void TestSetup()
    {
        //the first test is run each you test the notification test method.
        _notificationManager = new NotificationManager(new NotificationAccessorFake());

    }
    [TestMethod]
    //Author: Akoi Kollie
    public void TestReturnTrueIfInsertNotificationIsCorrect()
    {
        //creating a fake notification method that need to be test
        //Arrange

        Notification notification = new Notification();
        notification.NotificationID = 1000001;
        notification.Name = "cat";
        notification.Sender = 1000001;
        notification.Receiver = 20;
        notification.Important = false;
        notification.IsViewed = false;
        notification.Date = DateTime.Now;
        notification.Content = "do";
        const bool expectedResult = true;
        bool actualResult = false;
        //Act
        actualResult = _notificationManager.InsertNotification(notification);
        //Assert
        Assert.AreEqual(expectedResult, actualResult);
    }
    [TestMethod]
    //Author: Akoi Kollie
    public void TestReturnFalseIfInsertNotificationIsNotCorrect()
    {
        //Arrange
        Notification notification = new Notification();
        notification.NotificationID = 10;
        notification.Name = "cat";
        notification.Sender = 1;
        notification.Receiver = 20;
        notification.Important = true;
        notification.IsViewed = false;
        notification.Date = DateTime.Now;
        notification.Content = "do";
        const bool expectedResult = true;
        bool actualResult = false;
        //Act
        actualResult = _notificationManager.InsertNotification(notification);
        //Assert
        Assert.AreEqual(expectedResult, actualResult);
    }
    [TestMethod]
    public void GetAllNotificationsBySenderUserIDReturnsCorrectNotifications()
    {
        // Arrange
        int expectedCount = 2;
        int actualCount = 0;

        // Act
        actualCount = _notificationManager.GetAllSenderNotificationsByUserID(5000).Count;

        // Assert
        Assert.AreEqual(expectedCount, actualCount);
    }
    [TestMethod]
    public void GetAllNotificationsByRecieverUserIDReturnsCorrectNotifications()
    {
        // Arrange
        const int userId = 1002;
        int actualResult = 0;
        const int expectedResult = 2;

        actualResult = _notificationManager.GetAllNotificationsByUserID(userId).Count();
        Assert.AreEqual(expectedResult, actualResult);
    }
}
