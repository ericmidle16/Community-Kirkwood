/// <summary>
/// Kate Rich
/// Created: 2025-03-02
/// 
/// Class for the creation of Donation objects with set data fields.
/// 
/// Last Updated by : Christivie Mauwa
/// Updated by : 2025-03-28
/// WHat was changed : Added DonationVM : Donation
///
/// Last Updated by : Chase Hannen
/// Updated by : 2025-05-02
/// What was changed : Removed Amount [Required] and made Amount nullable
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Donation
    {
        public int DonationID { get; set; }
        [Required]
        public string DonationType { get; set; }
        [Required]
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public string Description { get; set; }
    }
    public class DonationSummaryVM : Donation
    {
        public string ProjectName { get; set; }
        public string ProjectLocation { get; set; }
        public decimal MonetaryDonationTotal { get; set; }
        public DateTime LastDonationDate { get; set; }
        public string FormattedLastDonationDate
        {
            get
            {
                return LastDonationDate.ToShortDateString();
            }
        }
    }
    // Author : Christivie Mauwa
    public class DonationVM : Donation
    {
        [Display(Name = "User Name")]
        public string ? UserName { get; set; }
        [Required]
        public string Name { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }

    }
}