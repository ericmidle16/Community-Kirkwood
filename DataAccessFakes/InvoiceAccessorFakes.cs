/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a method that is use to interface IInvoice
/// for testing the InvoiceLogicLayer test
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>
using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class InvoiceAccessorFakes : IInvoiceAccessor
    {
        private List<Invoice> _invoices;

        public InvoiceAccessorFakes() {
            _invoices = new List<Invoice>();
            _invoices.Add(new Invoice()
            {
                InvoiceID = 1,
                InvoiceNumber = "AQWFEWRG2",
                ExpenseID = null,
                ProjectID = 1000001,
                InvoiceDate = DateTime.Now,
                Status = "Pending payment",
                Description = "Truckers Inc services"
            });
            _invoices.Add(new Invoice()
            {
                InvoiceID = 2,
                InvoiceNumber = "HJSAS3",
                ExpenseID = null,
                ProjectID = 1000002,
                InvoiceDate = DateTime.Now,
                Status = "Payment Sent",
                Description = "Suppliers Services"
            });
            _invoices.Add(new Invoice()
            {
                InvoiceID = 3,
                InvoiceNumber = "EGES4",
                ExpenseID = null,
                ProjectID = 1000002,
                InvoiceDate = DateTime.Now,
                Status = "Pending payment",
                Description = "Suppliers Services"
            });

        }


        public List<Invoice> SelectAllInvoicesByProjectID(int projectId)
        {
            List<Invoice> filteredInvoices = _invoices.Where(p => p.ProjectID == projectId).ToList();
            return filteredInvoices;
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: This usew to add new invoice to the invoice data table
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int InsertInvoice(Invoice invoice)
        {
            //This a code for adding a invoice to the invoice data table.
            int expectLength = _invoices.Count + 1;
            _invoices.Add(invoice);
            if (expectLength == _invoices.Count)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }


        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: check user record exist in the invoice table. if it does not exist throw
        /// and error message
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public Invoice SelectInvoiceByInvoiceID(int invoiceid)
        {
            foreach (Invoice V in _invoices)
            {
                if (V.InvoiceID == invoiceid)
                {
                    return V;
                }
            }
            throw new ArgumentException("This invoice is not found");
        }


        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: check if invoice status was updated, if updated returns rowsaffected
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int UpdateChangeInvoiceStatusByInvoiceID(int invoiceid, string status)
        {
            int rowAffected = 0;
            Invoice item = null;
            foreach (Invoice items in _invoices)
            {
                if (items.InvoiceID == invoiceid && items.Status == status)
                {
                    items.Status = status;
                    rowAffected++;
                }
            }
            return rowAffected;

        }
        public List<Invoice> GetInvoices()
        {
            return _invoices;

        }

    }
}
