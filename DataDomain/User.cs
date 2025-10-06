/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/02
/// Summary: This class defines a User with properties
/// for user details, including ID, name, email, phone number, 
/// password hash, and active status.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-02-11
/// What Was Changed:
///		Class for the creation of User objects with set data fields.
///	Last Updated By: Brodie Pasker
/// Last Updated: 2025-03-11
/// What Was Changed:
///		Changed image datatype to byte[] and added ImageMimeType and Biography field
///		Made City, State, Image, ImageMimeType, and Reactivationg Date nullable
///		Also added a UserVM 
///  Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-03-13
/// What Was Changed:
///		Changed Image to a byte[].
///		Added ImageMimeType.
///		Changed ReactivationDate to DateTime data type
///		Added Biography
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace DataDomain
{
    public class User
    {
        public int UserID { get; set; }
        [Display(Name = "First Name")]
        public string? GivenName { get; set; }
        [Display(Name = "Last Name")]
        public string? FamilyName { get; set; }
        public string? Biography { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageMimeType { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? ReactivationDate { get; set; }
        public bool Suspended { get; set; }
        public bool ReadOnly { get; set; }
        public bool Active { get; set; }
        public string? RestrictionDetails { get; set; }

    }    
		
	public class UserVM : User
	{
		public List<string> Roles { get; set; }
        public List<UserProjectRole> ProjectRoles { get; set; }
    }

    public class VolunteerVM : User
    { 
        public List<Project> Projects { get; set; }

        public List<UserSkill> Skills { get; set; }

        public List<Availability> Availability { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }

}