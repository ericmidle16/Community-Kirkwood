/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/18
/// 
/// Testing class for Invoices manager
/// 
/// </summary>
///
/// <remarks>
/// Last Updated By: Eric Idle & Kate Rich
/// Last Updated: 2025-04-11
/// What Was Changed:
///     Updated the TestGetInvoiceByInvoiceIDReturnsRTrueForCorrectInvoiceID method
///     to correspond to the correct data fake objects.
///     
/// 
///     /// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-25
/// Update Change: TestGetAllInvoicesReturnInvoices
/// </remarks>
using DataAccessFakes;
using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class InvoiceManagerTests
    {
        private IInvoiceManager _invoiceManager;
        private Invoice _invoice = new Invoice();

        [TestInitialize]
        public void TestSetup()
        {
            _invoiceManager = new InvoiceManager(new InvoiceAccessorFakes());
            
        }

        [TestMethod]
        //Author:Akoi Kollie
        //This a method that test the insert invoice and if the invoice was submitted. 
        public void InsertInvoice()
        {
            //Arrange
            Invoice invoice = new Invoice();
            invoice.InvoiceID = 12;
            invoice.InvoiceNumber = "test02";
            invoice.ExpenseID = 1;
            invoice.ProjectID = 10000003;
            invoice.InvoiceDate = DateTime.Now;
            invoice.Status = "Pending";
            invoice.Description = "Do";
            const bool expectedResult = true;
            bool actualResult = false;
            //Act
            actualResult = _invoiceManager.InsertInvoice(invoice);
            //Assert 
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        //Author: Akoi Kollie
        public void TestEditChangeInvoiceStatusByInvoiceID()
        {
            //Arrange
            // Invoice invoice = new Invoice();
            const int invoiceid = 2000;
            const string status = "Paid";
            const bool expectedResult = false;
            bool actualResult = true;
            //Act
            actualResult = _invoiceManager.EditChangeInvoiceStatusByInvoiceID(invoiceid, status);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        //Author: Akoi Kollie
        public void TestGetInvoiceByInvoiceIDReturnsRTrueForCorrectInvoiceID()
        {
            //Arrange
            const int invoiceID = 2; // Expected InvoiceID, the ID used to get the Invoice.
            int actualInvoiceID = 1;
            //Act
            actualInvoiceID = _invoiceManager.GetInvoiceByInvoiceID(invoiceID).InvoiceID;
            //Assert
            Assert.AreEqual(invoiceID, actualInvoiceID);
        }

        [TestMethod]
        public void TestGetAllInvoicesByProjectID_CorrectProjectID() {
            int projectId = 1000002;
            int numberOfInvoices = 2;
            List<Invoice> invoices = _invoiceManager.GetAllInvoicesByProjectID(projectId);
            Assert.AreEqual(numberOfInvoices, invoices.Count());
        }

        [TestMethod]
        public void TestGetAllInvoicesByProjectID_WrongProjectID()
        {
            int projectId = 1000109;
            int numberOfInvoices = 67;
            List<Invoice> invoices = _invoiceManager.GetAllInvoicesByProjectID(projectId);
            Assert.AreNotEqual(numberOfInvoices, invoices.Count());
        }
        //Author: Akoi Kollie
        [TestMethod]
        public void TestGetAllInvoicesByProjectID_WrongInvoiceCount()
        {
            int projectId = 1000002;
            int numberOfInvoices = 5;
            List<Invoice> invoices = _invoiceManager.GetAllInvoicesByProjectID(projectId);
            Assert.AreNotEqual(numberOfInvoices, invoices.Count());
        }
        //Author: Akoi Kollie
        [TestMethod]
        public void TestGetAllInvoicesReturnInvoices()
        {
            const int numberOfInvoice = 3;
            List<Invoice> invoice = _invoiceManager.RetriveAllInvoices();
            Assert.AreEqual(numberOfInvoice, invoice.Count);
        }

    }
}
