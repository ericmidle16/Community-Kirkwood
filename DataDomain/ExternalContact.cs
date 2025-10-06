/// <summary>
/// Jacob McPherson
/// Created: 2025/02/18
/// 
/// Class for Holding ExternalContact Data
/// </summary>
///
/// <remarks>
/// Last Updated By: Jacob McPherson
/// Last Updated: 2025/04/17
/// What was updated: Added Data Validation Annotations
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataDomain
{
    public class ExternalContact
    {

        public int ExternalContactID { get; set; }

        [DisplayName("Type")]
		[Required]
		[MaxLength(50)]
        public string ExternalContactTypeID { get; set; }

        [DisplayName("Contact Name")]
		[Required]
		[MaxLength(100)]
        public string ContactName { get; set; }

        [DisplayName("Given Name")]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? GivenName { get; set; }

        [DisplayName("Family Name")]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? FamilyName { get; set; }

        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]*$", ErrorMessage = "Please enter a valid email address")]
        [MaxLength(250)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? Email { get; set; }

        [DisplayName("Phone Number")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Please Enter a Valid Phone Number")]
        [MaxLength(13)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? PhoneNumber { get; set; }

        [DisplayName("Job Title")]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? JobTitle { get; set; }
		
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? Address { get; set; }
		
		[MaxLength(250)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? Description { get; set; }

        [DisplayName("Added By")]
        public int AddedBy { get; set; }

        [DisplayName("Last Updated By")]
        public int LastUpdatedBy { get; set; }
		
        public bool Active { get; set; }
    }

    public class ExternalContactVM : ExternalContact
    {
        public string TypeDescription { get; set; } // for External Contact Type
    }

    public class ExternalContactType
    {
        [Required]
        [MaxLength(50)]
        public string ExternalContactTypeID { get; set; }
		
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
