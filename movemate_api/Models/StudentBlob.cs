﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class StudentBlob
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String FacebookId { get; set; }
        public String PhotoUri { get; set; }
        public String PhoneNumber { get; set; }
    }
}