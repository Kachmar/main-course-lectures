namespace Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        [Required(AllowEmptyStrings = false)]

        public string Name { get; set; }

        public int Id { get; set; }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/2/1004", "3/4/3004",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        public string GitHubLink { get; set; }

        public string Notes { get; set; }

        public virtual List<HomeTaskAssessment> HomeTaskAssessments { get; set; }

        public virtual List<StudentCourse> Courses { get; set; }

    }
}