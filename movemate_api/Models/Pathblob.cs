using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Controllers
{
    public class Pathblob
    {
        public int StudentId { get; set; }
        public Boolean ToFrom { get; set; }
        public String PathName { get; set; }
        public String Date { get; set; }
        public int DepId { get; set; }
        public String Address { get; set; }
        public int Vehicle { get; set; }
        public Boolean Train { get; set; }
        public Boolean Bus { get; set; }
        public Boolean Metro { get; set; }
        public Boolean Tram { get; set; }
        public int Seats { get; set; }
        public String Price { get; set; }
        public Boolean Head { get; set; }
        public String Description { get; set; }
        
    }
}