using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class PathPubBlob : PathBlob
    {
        public Boolean Train { get; set; }
        public Boolean Bus { get; set; }
        public Boolean Metro { get; set; }
        public Boolean Tram { get; set; }
    }
}