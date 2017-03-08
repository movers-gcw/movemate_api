using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        [Required]
        public Student Student { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public double Rate { get; set; }

    }
}