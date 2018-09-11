namespace ASP.NET.Demo.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PassCredits { get; set; }

        public int HomeTasksCount { get; set; }

        public List<HomeTask> HomeTasks { get; set; }

        public List<Lecturer> Lecturers { get; set; }
    }
}