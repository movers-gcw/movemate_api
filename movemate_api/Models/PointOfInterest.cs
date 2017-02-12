using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class PointOfInterest
    {
        public int PoiId { get; set; }
        public Department Department { get; set; }
        public String PlaceId { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Key]
        public String Address { get; set; }
    }
}