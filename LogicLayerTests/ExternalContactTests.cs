/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/24
/// Summary:  The tests for viewing external contacts
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/28
/// What was Changed: 	Added tests for viewing single contact
/// </summary>

using DataAccessInterfaces;
using DataAccessFakes;
using LogicLayer;
using DataDomain;

namespace LogicLayerTests;

[TestClass]
public class ExternalContactTests
{
    IExternalContactManager _externalContactsManager;

    [TestInitialize]
    public void TestSetup()
    {
        _externalContactsManager = new ExternalContactManager(new ExternalContactAccessorFake());
    }

    [TestMethod]
    public void TestViewAllExternalContactsIsSuccessful()
    {
        // assign
        int expectedCount = 4;
        int actualCount = 0;

        // act
        actualCount = _externalContactsManager.ViewAllExternalContacts().Count();

        // assert
        Assert.AreEqual(expectedCount, actualCount);

    }

    [TestMethod]
    public void TestViewSingleExternalContactIsSuccessful()
    {
        // assign
        string expectedName = "Test1";
        string actualName = "";
        int contactID = 1;

        // act
        actualName = _externalContactsManager.ViewSingleExternalContact(contactID).ContactName;

        // assert
        Assert.AreEqual(expectedName, actualName);

    }
    [TestMethod]
    public void TestViewSingleExternalContactReturnsNullForInvalidExternalContactID()
    {
        // assign
        ExternalContactVM viewModel;
        int contactID = 99;

        // act
        viewModel = _externalContactsManager.ViewSingleExternalContact(contactID);

        // assert
        Assert.AreEqual(viewModel, null);

    }

    [TestMethod]
    public void TestViewSingleExternalContactReturnsNullForInactiveContact()
    {
        // assign
        ExternalContactVM viewModel;
        int contactID = 3;

        // act
        viewModel = _externalContactsManager.ViewSingleExternalContact(contactID);

        // assert
        Assert.AreEqual(viewModel, null);


    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/02/18
    /// 
    /// Test for GetAllExternalContactTypes
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    [TestMethod]
    public void TestGetAllExternalContactTypes()
    {
        int actualValue;
        int expectedValue = 3;

        actualValue = _externalContactsManager.GetAllExternalContactTypes().Count;

        Assert.AreEqual(expectedValue, actualValue);
    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/02/18
    /// 
    /// Test for AddExternalContact
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    [TestMethod]
    public void TestInsertExternalContact()
    {
        ExternalContact contact = new ExternalContact();
        Assert.IsTrue(_externalContactsManager.AddExternalContact(contact));
    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/02/18
    /// 
    /// Test for AddExternalContactType
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    [TestMethod]
    public void TestInsertExternalContactType()
    {
        ExternalContact contact = new ExternalContact();
        Assert.IsTrue(_externalContactsManager.AddExternalContact(contact));
    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/03/04
    /// 
    /// Test for EditExternalContactType
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    [TestMethod]
    public void TestUpdateExternalContact()
    {
        ExternalContact contact = new ExternalContact() { ExternalContactID = 100002 };
        ExternalContact contact_old = new ExternalContact();

        Assert.IsTrue(_externalContactsManager.EditExternalContact(100000, contact, contact_old));
    }

    /// <summary>
    /// Jacob McPherson
    /// Created: 2025/03/04
    /// 
    /// Test for DeactivateExternalContact
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    [TestMethod]
    public void TestDeactivateExternalContact()
    {
        int id = 100002;

        Assert.IsTrue(_externalContactsManager.DeactivateExternalContact(id));
    }
}
