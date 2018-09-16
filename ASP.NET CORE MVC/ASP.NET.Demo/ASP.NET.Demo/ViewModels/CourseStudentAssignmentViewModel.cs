namespace ASP.NET.Demo.ViewModels
{
    using System;
    using System.Collections.Generic;

    using ASP.NET.Demo.Models;

    public class CourseStudentAssignmentViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PassCredits { get; set; }

        public int HomeTasksCount { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }

    public class StudentViewModel
    {
        public int StudentId { get; set; }

        public string StudentFullName { get; set; }

        public bool IsAssigned { get; set; }
    }
}
