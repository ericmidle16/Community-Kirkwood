/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a Test Method that interface the InvoiceAccessor
/// it is use the test invoice was submitted or not
/// </summary>
///
/// <remarks>
/// Updater  Akoi Kollie
/// Updated: 2025/04/25
/// </remarks>mm/dd
/// </remarks>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IInvoiceAccessor
    {
        public int InsertInvoice(Invoice invoice);
        int UpdateChangeInvoiceStatusByInvoiceID(int invoiceid, string status);
        Invoice SelectInvoiceByInvoiceID(int invoiceid);
        List<Invoice> SelectAllInvoicesByProjectID(int projectId);
        List<Invoice> GetInvoices();
    }
}
