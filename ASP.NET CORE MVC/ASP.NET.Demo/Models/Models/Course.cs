namespace Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/2/2004", "3/4/3004",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/2/2004", "3/4/3004",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Pass credits")]
        [Range(1, 100)]
        public int PassCredits { get; set; }

        public virtual List<HomeTask> HomeTasks { get; set; }

        public virtual List<LecturerCourse> Lecturers { get; set; }

        public virtual List<StudentCourse> Students { get; set; }
    }
}