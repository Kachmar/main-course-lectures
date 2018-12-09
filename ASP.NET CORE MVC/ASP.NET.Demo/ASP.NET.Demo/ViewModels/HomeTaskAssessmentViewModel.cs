namespace ASP.NET.Demo.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class HomeTaskAssessmentViewModel
    {
        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<HomeTaskStudentViewModel> HomeTaskStudents { get; set; }

        public int HomeTaskId { get; set; }
    }

    public class HomeTaskStudentViewModel
    {
        public int StudentId { get; set; }

        public string StudentFullName { get; set; }

        public bool IsComplete { get; set; }

        public int HomeTaskAssessmentId { get; set; }
    }
}
