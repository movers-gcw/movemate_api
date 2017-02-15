using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class PathSpecifiedView : PathView
    {
        public StudentView Maker { get; set; }
        public List<StudentView> Participants { get; set; }
        public String Price { get; set; }
        public int Seats { get; set; }
        public Boolean Train { get; set; }
        public Boolean Bus { get; set; }
        public Boolean Metro { get; set; }
        public Boolean Tram { get; set; }
        public String Description { get; set; }
        public Boolean Head { get; set; }
    }
}