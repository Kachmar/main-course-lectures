namespace EfCoreFluent
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        //Lets try with Guid
        //public Guid Id { get; set; }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual List<Exam> Exams { get; set; }

        //One-to-One relation
        public virtual StudentAddress StudentAddress { get; set; }

        //Many-to-Many relation
        public virtual List<LecturerStudent> LecturerStudents { get; set; }
    }

    public class DistantStudent : Student
    {
       public string DistantLocation { get; set; }
    }
}
