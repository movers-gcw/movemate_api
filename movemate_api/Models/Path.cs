using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Path
    {
        public int PathId { get; set; }
        public String PathName { get; set; }
        public Student Maker { get; set; }
        public List<Student> Participants { get; set; }
        public List<Section> Sections { get; set; }
    }
}