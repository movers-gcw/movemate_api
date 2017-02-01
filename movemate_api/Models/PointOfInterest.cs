using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class PointOfInterest
    {
        public int PointOfInterestId { get; set; }
        public String Name { get; set; }
        public String PlaceId { get; set; }
        public PointOfInterest getPointOfInterest()
        {
            return this;
        }
    }
}