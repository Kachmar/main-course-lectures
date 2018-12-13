namespace EfCoreFluent
{
    using System;
    using System.Collections.Generic;


    public class Lecturer
    {
        public Guid LecturerId { get; set; }

        public string Name { get; set; }

        public virtual List<LecturerStudent> LecturerStudents { get; set; }
    }
}
