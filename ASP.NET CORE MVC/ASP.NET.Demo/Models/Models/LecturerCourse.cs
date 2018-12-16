using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
  public  class LecturerCourse
    {
        public int LecturerId { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Lecturer Lecturer { get; set; }
    }
}
