/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary: 
///     Class for the creation of Location objects with set data fields.
/// 
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated:  2025-03-28
/// What Was Changed:  Added the Image and Image Mime Type fields
/// 
/// Last Updated By:  Chase Hannen
/// Last Updated:  2025-04-24
/// What Was Changed:  Added the Active field, removed Java constructors
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageMimeType { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
    }
}