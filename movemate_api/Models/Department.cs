using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Department
    {   
        public int DepartmentId { get; set; }
        public virtual University University { get; set; }
        public String DepartmentName { get; set; } 
        public String Address { get; set; }
        public String PlaceId { get; set; }
    }
}
