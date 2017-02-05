﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public Boolean Verified { get; set; }
        
        public String VerificationCode { get; set; }
        public University University { get; set; }
        public Department Department { get; set; }
        public String FacebookId { get; set; }
        public String GoogleId { get; set; }
        
        public Student getStudent()
        {
            return this;
        }

      
    }
}