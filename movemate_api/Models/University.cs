using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class University
    {
        public int universityId { get; set; }
        public String Name { get; set; }
        public List<Department> DepartmentList { get; set; }
        public University getUniversity()
        {
            return this;
        }
    }
}