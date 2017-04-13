using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Path
    {
        public Path()
        {
            this.Students = new HashSet<Student>();
        }
        public int PathId { get; set; }
        public String PathName { get; set; }
        [Required]
        public Student Maker { get; set; }
        [ForeignKey("Maker")]
        public int MakerId { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public Boolean ToFrom { get; set; }
        public Boolean Open { get; set; }
        public PointOfInterest Start { get; set; }
        public PointOfInterest Destination { get; set; }
        public String DepartmentAddress { get; set; }
        public int Vehicle { get; set; }
        public Boolean Train { get; set; }
        public Boolean Bus { get; set; }
        public Boolean Metro { get; set; }
        public Boolean Tram { get; set; }
        public String Price { get; set; }
        public String Description { get; set; }
        public int AvailableSeats { get; set; }
        public Boolean AvailableHeadgear { get; set; }
        public int UniversityId { get; set; }
    }
}