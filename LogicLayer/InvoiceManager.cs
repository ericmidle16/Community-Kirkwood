/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/18
/// 
/// The manager to Invoice that uses methods
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class InvoiceManager : IInvoiceManager
    {
        private IInvoiceAccessor _invoiceAccessor;

        public InvoiceManager()
        {
            _invoiceAccessor = new InvoiceAccessor();
        }

        public InvoiceManager(IInvoiceAccessor invoiceAccessor)
        {
            _invoiceAccessor = invoiceAccessor;
        }

        /// <summary>
        /// Created: Akoi Kollie
        /// Created: 2025/02/10
        /// 
        /// This is to grab all invoices from the invoice accessor
        /// </summary>
        ///
        /// <remarks>
        /// Updater :Eric Idle
        /// Updated: 2025/02/18
        /// example:
        /// </remarks>
        public List<Invoice> GetAllInvoicesByProjectID(int projectId)
        {
            List<Invoice> invoices = null;

            try
            {
                invoices = _invoiceAccessor.SelectAllInvoicesByProjectID(projectId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data no available", ex);
            }
            return invoices;
        }

        // the insert invoice method that is to be test.
        //Author: Akoi Kollie
        public bool InsertInvoice(Invoice invoice)
        {
            //Testing if invoice is insert in the the database.
            bool results = false;
            try
            {
                results = (1 == _invoiceAccessor.InsertInvoice(invoice));

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Insert invoice failed", ex);
            }
            return results;
        }
        //Author: Akoi Kollie
        public Invoice GetInvoiceByInvoiceID(int invoiceid)
        {
            Invoice invoice = null;

            //bool results = false;
            try
            {
                invoice = _invoiceAccessor.SelectInvoiceByInvoiceID(invoiceid);
                if (invoice != null)
                {
                    return invoice;

                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Status not change", ex);
            }
            return invoice;

        }
        //Author: Akoi Kollie
        public bool EditChangeInvoiceStatusByInvoiceID(int invoiceid, string status)
        {
            bool isStatusUpdated = false;
            int rowsaffected = 0;
            //bool results = false;
            try
            {
                rowsaffected = _invoiceAccessor.UpdateChangeInvoiceStatusByInvoiceID(invoiceid, status);
                if (rowsaffected == 1)
                {
                    isStatusUpdated = true;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Status not change", ex);
            }
            return isStatusUpdated;

        }

        //Author: Akoi Kollie
        public List<Invoice> RetriveAllInvoices()
        {
            List<Invoice> invoiceList = null;
            try
            {
                invoiceList = _invoiceAccessor.GetInvoices();

            }
            catch (Exception ex)
            {
                throw new ArgumentException("All these are the invoices");

            }
            return invoiceList;
        }
    }
}
