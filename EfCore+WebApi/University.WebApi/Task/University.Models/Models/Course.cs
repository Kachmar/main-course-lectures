namespace Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public virtual List<HomeTask> HomeTasks { get; set; } = new List<HomeTask>();
    }
}