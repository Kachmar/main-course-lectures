using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ASP.NET.Demo.Models;
    using ASP.NET.Demo.ViewModels;

    public class CourseController : Controller
    {

        // GET
        public IActionResult Courses()
        {
            return View(CourseContainer.CourseCollection);
        }

        public IActionResult Delete(int id)
        {
            var courseToRemove = CourseContainer.CourseCollection.SingleOrDefault(p => p.Id == id);
            if (courseToRemove != null)
            {
                CourseContainer.CourseCollection.Remove(courseToRemove);
            }

            return View("Courses", CourseContainer.CourseCollection);
        }

        public IActionResult Create()
        {
            ViewData["action"] = nameof(this.Create);
            return this.View("Edit", new Course());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = CourseContainer.CourseCollection.SingleOrDefault(p => p.Id == id);
            if (course == null)
            {
                return this.NotFound();
            }

            ViewData["action"] = nameof(this.Edit);

            return this.View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course courseParameter)
        {
            if (courseParameter == null)
            {
                return this.NotFound();
            }
            var course = CourseContainer.CourseCollection.SingleOrDefault(p => p.Id == courseParameter.Id);
            if (course == null)
            {
                return this.NotFound();
            }

            course.Name = courseParameter.Name;
            course.EndDate = courseParameter.EndDate;
            course.StartDate = courseParameter.StartDate;
            course.HomeTasksCount = courseParameter.HomeTasksCount;
            course.PassCredits = courseParameter.PassCredits;

            return this.RedirectToAction(nameof(Courses));
        }

        [HttpPost]
        public IActionResult Create(Course courseParameter)
        {
            if (courseParameter == null)
            {
                return this.BadRequest();
            }

            int maxCurrentId = CourseContainer.CourseCollection.Max((course) => course.Id);
            courseParameter.Id = ++maxCurrentId;
            CourseContainer.CourseCollection.Add(courseParameter);
            return this.RedirectToAction(nameof(Courses));
        }

        [HttpGet]
        public IActionResult AssignStudents(int id)
        {
            var allStudents = GetAllStudents();
            var course = CourseContainer.CourseCollection.Single(p => p.Id == id);
            CourseStudentAssignmentViewModel model = new CourseStudentAssignmentViewModel();

            model.Id = id;
            model.EndDate = course.EndDate;
            model.HomeTasksCount = course.HomeTasksCount;
            model.Name = course.Name;
            model.StartDate = course.StartDate;
            model.PassCredits = course.PassCredits;
            model.Students = new List<StudentViewModel>();

            foreach (var student in allStudents)
            {
                bool isAssigned = course.Students.Any(p => p.Id == student.Id);
                model.Students.Add(new StudentViewModel() { StudentId = student.Id, StudentFullName = student.Name, IsAssigned = isAssigned });
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult AssignStudents(CourseStudentAssignmentViewModel assignmentViewModel)
        {
            var allStudents = GetAllStudents();

            var course = CourseContainer.CourseCollection.Single(p => p.Id == assignmentViewModel.Id);
            course.Students.Clear();
            foreach (var studentViewModel in assignmentViewModel.Students)
            {
                var student = allStudents.Single(p => p.Id == studentViewModel.StudentId);
                if (studentViewModel.IsAssigned)
                {
                    course.Students.Add(student);
                }
            }

            return RedirectToAction("Courses");
        }

        private static List<Student> GetAllStudents()
        {
            return CourseContainer.CourseCollection.SelectMany(p => p.Students).Distinct().ToList();
        }
    }
}