using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System.Linq;

    using ASP.NET.Demo.ViewModels;

    using DataAccess.ADO;

    using Models.Models;
    using Services;

    public class StudentController : Controller
    {
        private readonly StudentService studentService;

        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        // GET
        public IActionResult Students()
        {


            return View(studentService.GetAllStudents());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = studentService.GetStudentById(id);
            ViewData["Action"] = "Edit";
            return this.View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";
                return this.View("Edit", model);
            }
            this.studentService.UpdateStudent(model);

            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.studentService.DeleteStudent(id);
            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            var student = new Student();
            return this.View("Edit", student);
        }


        [HttpPost]
        public IActionResult Create(Student model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                return this.View("Edit", model);
            }

            this.studentService.CreateStudent(model);
            return RedirectToAction("Students");

        }
    }
}