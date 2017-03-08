using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class StudentSpecifiedView : StudentView
    {
        public StudentSpecifiedView()
        {
            this.CreatedPaths = new HashSet<PathView>();
            this.JoinedPaths = new HashSet<PathView>();
        }
        public HashSet<PathView> CreatedPaths { get; set; }
        public HashSet<PathView> JoinedPaths { get; set; }
    }
}