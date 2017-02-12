using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class University
    {
        public int UniversityId { get; set; }
        
        public String UniversityName { get; set; }
        public virtual List<Department> DepartmentList { get; set; }
        
    }
}