namespace ASP.NET.Demo.Controllers
{
    using DataAccess.EF;

    using Microsoft.AspNetCore.Mvc;

    using Models.Models;

    using Service;

    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        // GET
        public IActionResult Students()
        {
            return View(this.studentService.GetAllStudents());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = this.studentService.GetStudentById(id);
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