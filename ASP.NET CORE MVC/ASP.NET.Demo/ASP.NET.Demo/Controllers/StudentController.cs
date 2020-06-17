
using System.Threading.Tasks;

namespace ASP.NET.Demo.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models.Models;
    using Services;

    public class StudentController : Controller
    {
        private readonly StudentService studentService;
        private readonly IAuthorizationService authorizationService;

        public StudentController(StudentService studentService, IAuthorizationService authorizationService)
        {
            this.studentService = studentService;
            this.authorizationService = authorizationService;
        }

        // GET
        public IActionResult Students()
        {
            return View(studentService.GetAllStudents());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = studentService.GetStudentById(id);
            var result = await authorizationService.AuthorizeAsync(User, student, "SameUserPolicy");
            if (result.Succeeded)
            {
                ViewData["Action"] = "Edit";
                return this.View(student);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";
                return this.View("Edit", model);
            }

            var result = await authorizationService.AuthorizeAsync(User, model, "SameUserPolicy");
            if (result.Succeeded)
            {
                this.studentService.UpdateStudent(model);

                return RedirectToAction("Students");
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this.studentService.DeleteStudent(id);
            return RedirectToAction("Students");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            var student = new Student();
            return this.View("Edit", student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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