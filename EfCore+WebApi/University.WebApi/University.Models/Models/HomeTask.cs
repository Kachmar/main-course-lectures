namespace Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HomeTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Course Course { get; set; }

    }
}