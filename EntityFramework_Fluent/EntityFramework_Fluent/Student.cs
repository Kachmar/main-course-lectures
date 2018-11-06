namespace EntityFramework_Fluent
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        //Lets try with Guid
        public Guid Id { get; set; }
        //public int Id { get; set; }


        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Speciality { get; set; }

        public virtual List<Exam> Exams { get; set; }

        //One-to-One relation
        public virtual StudentAddress StudentAddress { get; set; }

        //Many-to-Many relation
        public List<Lecturer> Lecturers { get; set; }
    }

    public class DistantStudent : Student
    {
       public string DistantLocation { get; set; }
    }
}
