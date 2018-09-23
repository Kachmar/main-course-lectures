using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ASP.NET.Demo.ViewModels;

    using DataAccess.ADO;

    using Models.Models;

    public class CourseController : Controller
    {
        private readonly Repository repository;

        public CourseController(Repository repository)
        {
            this.repository = repository;
        }

        // GET
        public IActionResult Courses()
        {
            return View(this.repository.GetAllCourses());
        }

        public IActionResult Delete(int id)
        {
            this.repository.DeleteCourse(id);

            return RedirectToAction("Courses");
        }

        public IActionResult Create()
        {
            ViewData["action"] = nameof(this.Create);
            return this.View("Edit", new Course());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course = this.repository.GetCourse(id);
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
                return this.BadRequest();
            }
            repository.UpdateCourse(courseParameter);
            return this.RedirectToAction(nameof(Courses));
        }

        [HttpPost]
        public IActionResult Create(Course courseParameter)
        {
            if (courseParameter == null)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewData["action"] = nameof(this.Create);

                return this.View("Edit", courseParameter);
            }
            this.repository.CreateCourse(courseParameter);
            return this.RedirectToAction(nameof(Courses));
        }

        [HttpGet]
        public IActionResult AssignStudents(int id)
        {
            var allStudents = this.repository.GetAllStudents();
            var course = this.repository.GetCourse(id);
            CourseStudentAssignmentViewModel model = new CourseStudentAssignmentViewModel();

            model.Id = id;
            model.EndDate = course.EndDate;
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
            this.repository.SetStudentsToCourse(assignmentViewModel.Id, assignmentViewModel.Students.Where(p => p.IsAssigned).Select(student => student.StudentId));

            return RedirectToAction("Courses");
        }
    }
}