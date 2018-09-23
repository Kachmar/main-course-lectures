﻿namespace Models.Models
{
    using System;
    using System.Collections.Generic;

    public class HomeTask
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public Course Course { get; set; }

        public List<HomeTaskAssessment> HomeTaskAssessments { get; set; }
    }
}