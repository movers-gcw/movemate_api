﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class PathView
    {
        public int PathId { get; set; }
        public String PathName { get; set; }
        public int Vehicle { get; set; }
        public String StartAddress { get; set; }
        public String DestinationAddress { get; set; }
    }
}