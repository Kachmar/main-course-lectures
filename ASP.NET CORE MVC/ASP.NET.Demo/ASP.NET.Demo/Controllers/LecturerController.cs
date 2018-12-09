namespace ASP.NET.Demo.Controllers
{
    using DataAccess.ADO;

    using Microsoft.AspNetCore.Mvc;

    using Models.Models;

    public class LecturerController : Controller
    {
        private readonly Repository repository;

        public LecturerController(Repository repository)
        {
            this.repository = repository;
        }

        // GET
        public IActionResult Lecturers()
        {
            return View(repository.GetAllLecturers());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Lecturer lecturer = repository.GetLecturerById(id);
            ViewData["Action"] = "Edit";
            return this.View(lecturer);
        }

        [HttpPost]
        public IActionResult Edit(Lecturer model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";
                return this.View("Edit", model);
            }
            this.repository.UpdateLecturer(model);

            return RedirectToAction("Lecturers");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.repository.DeleteLecturer(id);
            return RedirectToAction("Lecturers");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            var lecturer = new Lecturer();
            return this.View("Edit", lecturer);
        }


        [HttpPost]
        public IActionResult Create(Lecturer model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                return this.View("Edit", model);
            }

            this.repository.CreateLecturer(model);
            return RedirectToAction("Lecturers");
        }
    }
}