using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class PointOfInterest
    {
        public int PointOfInterestId { get; set; }
        public String PlaceId { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Address { get; set; }
        public DateTime DateTime { get; set; }
        public Section Section { get; set; }
    }
}