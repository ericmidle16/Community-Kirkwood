/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the invoice class that is for invoice that is need for Invoice table
/// It make connection to the invoice table.
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataDomain
{
    public class Invoice
    {
        //This is invoice class
        public int InvoiceID { get; set; }
        [Display(Name ="Invoice Number")]
        public string InvoiceNumber { get; set; }
        public int ? ExpenseID { get; set; }
        public int ProjectID { get; set; }
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
