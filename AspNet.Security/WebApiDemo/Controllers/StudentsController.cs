using System.Threading.Tasks;
using DataAccess.ADO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace WebApiDemo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Repository _repository;
        private readonly IAuthorizationService _authorizationService;

        public StudentsController(Repository repository, IAuthorizationService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }

        public IActionResult Students()
        {
            var model = _repository.GetAllStudents();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = _repository.GetStudentById(id);

            var result = await _authorizationService.AuthorizeAsync(User, student, "SameUserPolicy");
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            ViewBag.Title = "Edit";
            return View(student);
        }

        [HttpPost]

        public async Task<IActionResult> EditStudent(Student student)
        {
            var result = await _authorizationService.AuthorizeAsync(User, student, "SameUserPolicy");
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            _repository.UpdateStudent(student);
            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult DeleteStudent(int id)
        {
            _repository.DeleteStudent(id);
            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateStudent()
        {
            ViewBag.Title = "Create";

            return View("EditStudent", new Student());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateStudent(Student student)
        {
            _repository.CreateStudent(student);
            return RedirectToAction("Students");
        }
    }
}