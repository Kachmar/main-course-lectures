using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ASP.NET.Demo.Models;

    public class CourseController : Controller
    {

        // GET
        public IActionResult Courses()
        {
            return View(CourseContainer.CourseCollection);
        }

        public IActionResult Delete(int id)
        {
            var courseToRemove = CourseContainer.CourseCollection.SingleOrDefault(p => p.Id == id);
            if (courseToRemove != null)
            {
                CourseContainer.CourseCollection.Remove(courseToRemove);
            }

            return View("Courses", CourseContainer.CourseCollection);
        }

        public IActionResult Create()
        {
            ViewData["action"] = nameof(this.Create);
            return this.View("Edit");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = CourseContainer.CourseCollection.SingleOrDefault(p => p.Id == id);
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
                return this.NotFound();
            }
            var course = CourseContainer.CourseCollection.SingleOrDefault(p => p.Id == courseParameter.Id);
            if (course == null)
            {
                return this.NotFound();
            }

            course.Name = courseParameter.Name;
            course.EndDate = courseParameter.EndDate;
            course.StartDate = courseParameter.StartDate;
            course.HomeTasksCount = courseParameter.HomeTasksCount;
            course.PassCredits = courseParameter.PassCredits;

            return this.RedirectToAction(nameof(Courses));
        }

        [HttpPost]
        public IActionResult Create(Course courseParameter)
        {
            if (courseParameter == null)
            {
                return this.BadRequest();
            }

            int maxCurrentId = CourseContainer.CourseCollection.Max((course) => course.Id);
            courseParameter.Id = ++maxCurrentId;
            CourseContainer.CourseCollection.Add(courseParameter);
            return this.RedirectToAction(nameof(Courses));
        }
    }
}