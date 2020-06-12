using System.Collections.Generic;
using System.Linq;
using DataAccess.ADO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.ViewModels;

namespace WebApiDemo.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Repository _repository;

        public CoursesController(Repository repository)
        {
            _repository = repository;
        }
        [Authorize(Roles = "Admin")]


        [HttpGet]
        [Authorize(Policy = "UkrainiansOrAdmin")]
        public IActionResult Assign(int id)
        {
            var allCourses = _repository.GetAllCourses();
            var student = _repository.GetStudentById(id);


            var model = new StudentCoursesAssignmentViewModel();

            model.StudentId = id;
            model.StudentName = student.Name;
            model.Courses = new List<CourseViewModel>();
            foreach (var course in allCourses)
            {
                model.Courses.Add(new CourseViewModel()
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    PassCredits = course.PassCredits,
                    IsAssigned = student.Courses.Any(p => p.Id == course.Id)
                }
                );
            }
            return View(model);
        }

        [Authorize(Policy = "UkrainiansOrAdmin")]

        [HttpPost]
        public IActionResult Assign(StudentCoursesAssignmentViewModel model)
        {
            return RedirectToAction("Students", "Students");
        }
    }
}