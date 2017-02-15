using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Student
    {
        
        public Student()
        {
            this.CreatedPaths = new List<Path>();
            this.JoinedPaths = new List<Path>();
        }
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
        public List<Path> CreatedPaths { get; set; }
        public List<Path> JoinedPaths { get; set; }

      
    }
}