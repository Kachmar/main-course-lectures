using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System.Linq;

    using ASP.NET.Demo.ViewModels;

    using DataAccess.ADO;

    using Models.Models;

    public class StudentController : Controller
    {
        private readonly Repository repository;

        public StudentController(Repository repository)
        {
            this.repository = repository;
        }

        // GET
        public IActionResult Students()
        {


            return View(repository.GetAllStudents());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = repository.GetStudentById(id);
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
            this.repository.UpdateStudent(model);

            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.repository.DeleteStudent(id);
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

            this.repository.CreateStudent(model);
            return RedirectToAction("Students");

        }
    }
}