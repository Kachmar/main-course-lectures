namespace Models.Models
{
    using System;
    using System.Collections.Generic;

    public class Lecturer
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual List<LecturerCourse> Courses { get; set; }
    }
}
