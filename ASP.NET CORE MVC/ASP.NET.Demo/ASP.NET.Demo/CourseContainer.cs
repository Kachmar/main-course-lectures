using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.Demo
{
    using ASP.NET.Demo.Models;

    public static class CourseContainer
    {
        public static List<Course> CourseCollection = new List<Course>();





        static CourseContainer()
        {

            List<Student> studentsGroup1 = new List<Student>();
            studentsGroup1.Add(new Student() { Id = 11, Name = "Leo Neo 11" });
            studentsGroup1.Add(new Student() { Id = 12, Name = "Leo Neo 12" });
            studentsGroup1.Add(new Student() { Id = 13, Name = "Leo Neo 13" });

            List<Student> studentsGroup2 = new List<Student>();
            studentsGroup2.Add(new Student() { Id = 21, Name = "Leo Neo 21" });
            studentsGroup2.Add(new Student() { Id = 22, Name = "Leo Neo 22" });
            studentsGroup2.Add(new Student() { Id = 23, Name = "Leo Neo 23" });
            List<Student> studentsGroup3 = new List<Student>();
            studentsGroup3.Add(new Student() { Id = 31, Name = "Leo Neo 31" });
            studentsGroup3.Add(new Student() { Id = 32, Name = "Leo Neo 32" });
            studentsGroup3.Add(new Student() { Id = 33, Name = "Leo Neo 33" });
            var homeTask = new HomeTask() { Id = 1, Title = "Demo", HomeTaskAssessments = new List<HomeTaskAssessment>() };
            var course = new Course()
            {
                EndDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                HomeTasksCount = 10,
                Id = 1,
                Name = "Main Course",
                PassCredits = 15,
                HomeTasks = new List<HomeTask>() { homeTask },
                Students = studentsGroup1
            };
            homeTask.Course = course;
            homeTask = new HomeTask() { Id = 2, Title = "Demo", HomeTaskAssessments = new List<HomeTaskAssessment>() };
            CourseCollection.Add(course);

            course = new Course()
            {
                EndDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                HomeTasksCount = 5,
                Id = 2,
                Name = "Evening Course",
                PassCredits = 15,
                HomeTasks = new List<HomeTask>() { homeTask },
                Students = studentsGroup2
            };
            homeTask.Course = course;
            homeTask = new HomeTask() { Id = 3, Title = "Demo", HomeTaskAssessments = new List<HomeTaskAssessment>() };
            CourseCollection.Add(course);

            course = new Course()
            {
                EndDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                HomeTasksCount = 4,
                Id = 3,
                Name = "Php Course",
                PassCredits = 20,
                HomeTasks = new List<HomeTask>() { homeTask },
                Students = studentsGroup3
            };
            CourseCollection.Add(course);
        }
    }
}
