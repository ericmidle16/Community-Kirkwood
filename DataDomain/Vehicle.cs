/// <summary>
/// Ellie Wacker
/// Created: 2025-02-17
/// 
/// Class for the creation of Vehicle objects with set data fields.
/// </summary>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Vehicle
    {
        [Required(ErrorMessage = "VIN number is required.")]
        public string VehicleID { get; set; }
        public int UserID { get; set; }
        public bool? Active { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        [Required(ErrorMessage = "License plate number is required.")]
        public string LicensePlateNumber { get; set; }
        [Required(ErrorMessage = "Insurance status is required.")]
        public bool InsuranceStatus { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Required(ErrorMessage = "Number of seats are required.")]
        public int NumberOfSeats { get; set; }
        [Required(ErrorMessage = "Transportable items are required.")]
        public string TransportUtility { get; set; }
    }
}
