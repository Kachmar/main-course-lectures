using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System.Linq;

    using ASP.NET.Demo.Models;
    using ASP.NET.Demo.ViewModels;

    public class StudentController : Controller
    {
        // GET
        public IActionResult Students()
        {
            var allStudents = CourseContainer.CourseCollection.SelectMany(p => p.Students).Distinct().ToList();

            return View(allStudents);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var allStudents = CourseContainer.CourseCollection.SelectMany(p => p.Students).Distinct().ToList();
            var student = allStudents.Single(p => p.Id == id);
            ViewData["Action"] = "Edit";
            return this.View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
            var allStudents = CourseContainer.CourseCollection.SelectMany(p => p.Students).Distinct().ToList();
            var student = allStudents.Single(p => p.Id == model.Id);
            student.BirthDate = model.BirthDate;
            student.Email = model.Email;
            student.GitHubLink = model.GitHubLink;
            student.Name = model.Name;
            student.Notes = model.Notes;
            student.PhoneNumber = model.PhoneNumber;

            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var allStudents = CourseContainer.CourseCollection.SelectMany(p => p.Students).Distinct().ToList();
            var student = allStudents.Single(p => p.Id == id);
            CourseContainer.CourseCollection.ForEach(p => p.Students.Remove(student));
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
            return RedirectToAction("Students");

        }
    }
}