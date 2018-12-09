namespace ASP.NET.Demo.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class CourseLecturerAssignmentViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PassCredits { get; set; }

        public List<LecturersViewModel> Lecturers { get; set; }
    }

    public class LecturersViewModel
    {
        public int LecturerId { get; set; }

        public string Name { get; set; }

        public bool IsAssigned { get; set; }
    }
}
