using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public PointOfInterest Start { get; set; }
        public PointOfInterest Destination { get; set; }
        public Path Path { get; set; }
        public int Vehicle { get; set; }
        public Boolean Train { get; set; }
        public Boolean Bus { get; set; }
        public Boolean Metro { get; set; }
        public Boolean Tram { get; set; }
        public String Price { get; set; }
        public String Description { get; set; }
        public int AvailableSeats { get; set; }
        public Boolean AvailableHeadgear { get; set; }
    }
}