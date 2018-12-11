namespace EfBasicsCore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int StudentId { get; set; }

        //Notice how DB schema of generated DB changes when adding this attributes
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        //Lets uncomment this property, and run the application and see it crashes
        //In Package manager console type enable-migrations and see what happens
        //Then use command update-database to actually update the DB schema
        //Change the type from string to int and try updating the DB.
        public string Speciality { get; set; }

        //virtual - to support lazy loading
        public virtual List<Exam> Exams { get; set; }

        //public List<Exam> Exams { get; set; }

        //One-to-One relation
        public virtual StudentAddress StudentAddress { get; set; }

        //Many-to-Many relation
        public List<LecturerStudent> LecturerStudents { get; set; }
    }

    public class DistantStudent : Student
    {
       public string DistantLocation { get; set; }
    }
}
