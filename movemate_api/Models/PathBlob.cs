using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public abstract class PathBlob
    {
        public int StudentId { get; set; }
        public Boolean ToFrom { get; set; }
        public String PathName { get; set; }
        public String Date { get; set; }
        public int DepId { get; set; }
        public String Address { get; set; }
        public int Vehicle { get; set; }
    }
}