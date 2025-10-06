/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the iinoice manager class that  interface the invoice class
/// It make a connection to invoice class to 
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IInvoiceManager
    {
        public bool InsertInvoice(Invoice invoice);
        public bool EditChangeInvoiceStatusByInvoiceID(int invoiceid, string status);
        public Invoice GetInvoiceByInvoiceID(int invoiceid);
        List<Invoice> GetAllInvoicesByProjectID(int projectId);
        public List<Invoice> RetriveAllInvoices();
    }
}
