using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Department
    {   
        public int DepartmentId { get; set; }
        public String Name { get; set; }
        public List<PointOfInterest> CampusList { get; set; }
        public Department getDepartment()
        {
            return this;
        }
    }
}