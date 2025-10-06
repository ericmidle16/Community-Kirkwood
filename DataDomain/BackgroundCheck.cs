/// <summary>
/// Kate Rich
/// Created: 2025-02-10
/// 
/// Class for the creation of BackgroundCheck objects with set data fields.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-20 
/// What Was Changed:
///     Added the BackgroundCheckVM class.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-08
/// What Was Changed:
///     Made the following fields nullable - they will never actually be null, though - for
///     Model validation when editing:
///         InvestigatorGivenName, InvestigatorFamilyName, VolunteerGivenName,
///     VolunteerFamilyName, & ProjectName
///    
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class BackgroundCheck
    {
        public int BackgroundCheckID { get; set; }
        public int Investigator { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public string Status { get; set; }

        [Display(Name = "Details")]
        public string Description { get; set; }
    }
    public class BackgroundCheckVM : BackgroundCheck
    {
        public string? InvestigatorGivenName { get; set; }
        public string? InvestigatorFamilyName { get; set; }

        [Display(Name = "Investigator")]
        public string InvestigatorName
        {
            get
            {
                return InvestigatorGivenName + " " + InvestigatorFamilyName;
            }
        }
        public string? VolunteerGivenName { get; set; }
        public string? VolunteerFamilyName { get; set; }

        [Display(Name = "Volunteer")]
        public string VolunteerName
        {
            get
            {
                return VolunteerGivenName + " " + VolunteerFamilyName;
            }
        }

        [Display(Name = "Project")]
        public string? ProjectName { get; set; }
    }
}