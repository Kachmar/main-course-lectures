namespace Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HomeTask
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/2/2004", "3/4/3004",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Range(1, 100)]
        public int Number { get; set; }

        public virtual Course Course { get; set; }

        public virtual List<HomeTaskAssessment> HomeTaskAssessments { get; set; }
    }
}