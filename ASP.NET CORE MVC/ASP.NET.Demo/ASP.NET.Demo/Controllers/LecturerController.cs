namespace ASP.NET.Demo.Controllers
{
    using DataAccess.ADO;

    using Microsoft.AspNetCore.Mvc;

    using Models.Models;
    using Services;

    public class LecturerController : Controller
    {
        private readonly LecturerService lecturerService;

        public LecturerController(LecturerService lecturerService)
        {
            this.lecturerService = lecturerService;
        }

        // GET
        public IActionResult Lecturers()
        {
            return View(lecturerService.GetAllLecturers());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Lecturer lecturer = lecturerService.GetLecturerById(id);
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
            this.lecturerService.UpdateLecturer(model);

            return RedirectToAction("Lecturers");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.lecturerService.DeleteLecturer(id);
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

            this.lecturerService.CreateLecturer(model);
            return RedirectToAction("Lecturers");
        }
    }
}