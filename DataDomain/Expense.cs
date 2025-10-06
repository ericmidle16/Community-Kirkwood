/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/24
/// Summary:
///     The Expense object attributes
/// </summary>
///
/// <remarks>
/// Updater:    Chase Hannen
/// Updated:    2025/05/02
///         Changed amount to decimal
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    /// <summary>		
    /// Eric Idle
    /// Created: 2025/02/24
    /// Summary: Class for the Expense object
    /// </summary>
    public class Expense
    {
        public int ExpenseID { get; set; }
        public int ProjectID { get; set; }
        public string ExpenseTypeID { get; set; }
        public DateTime? Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}