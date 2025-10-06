/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02 
/// Summary:
///     Represents a project with relevant details such as 
/// name, type, location, user, status, and financials.
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/06
/// What was Changed: Initial creation
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-07
/// What Was Changed:
///     I removed the EndDate property from this class.
///     I added the AcceptsDonations & PayPalAccount properties.
///     
/// Last Updated By: christivie
/// Last Updated: 2025-03-28
/// What Was Changed:
///     i added the city, zip code, address and the famyly name mthod
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-02
/// What Was Changed:
///     Added System.ComponentModel.DataAnnotations & marked up the properties
///     needed for the View Single Project view.
///     Added ForumattedStartDate in the ProjectVM class for displaying the start date.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataDomain
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }

        [Display(Name = "Project Type")]
        public string ProjectTypeID { get; set; } 
        public int LocationID { get; set; }
        public int UserID { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public bool AcceptsDonations { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? PayPalAccount { get; set; }
        public decimal AvailableFunds { get; set; }
    }

    public class ProjectVM : Project
    {
        [Display(Name = "Start Date")]
        public string FormattedStartDate
        {
            get
            {
                return StartDate.ToShortDateString();
            }
        }

        public string GivenName { get; set; }
        public string FamilyName { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}