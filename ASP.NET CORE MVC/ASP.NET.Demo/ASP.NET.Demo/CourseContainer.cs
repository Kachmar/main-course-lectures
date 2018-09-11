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
            var homeTask = new HomeTask() { Id = 1, Title = "Demo" };
            var course = new Course()
            {
                EndDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                HomeTasksCount = 10,
                Id = 1,
                Name = "Main Course",
                PassCredits = 15,
                HomeTasks = new List<HomeTask>() { homeTask }
            };
            homeTask.Course = course;
            homeTask = new HomeTask() { Id = 2, Title = "Demo" };
            CourseCollection.Add(course);

            course = new Course()
            {
                EndDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                HomeTasksCount = 5,
                Id = 2,
                Name = "Evening Course",
                PassCredits = 15,
                HomeTasks = new List<HomeTask>() { homeTask }
            };
            homeTask.Course = course;
            homeTask = new HomeTask() { Id = 3, Title = "Demo" };
            CourseCollection.Add(course);

            course = new Course()
            {
                EndDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                HomeTasksCount = 4,
                Id = 3,
                Name = "Php Course",
                PassCredits = 20,
                HomeTasks = new List<HomeTask>() { homeTask }
            };
            CourseCollection.Add(course);
        }
    }
}
