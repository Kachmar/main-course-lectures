using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System.Linq;

    using ASP.NET.Demo.Models;

    using Microsoft.AspNetCore.Routing;

    public class HomeTaskController : Controller
    {
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var homeTask = CourseContainer.CourseCollection.SelectMany(p => p.HomeTasks).SingleOrDefault(h => h.Id == id);
            if (homeTask == null)
                return this.NotFound();

            return View(homeTask);
        }

        [HttpPost]
        public IActionResult Edit(HomeTask homeTaskParameter)
        {
            if (homeTaskParameter == null)
            {
                return this.NotFound();
            }

            var homeTask = CourseContainer.CourseCollection.SelectMany(p => p.HomeTasks).SingleOrDefault(h => h.Id == homeTaskParameter.Id);

            if (homeTask == null)
            {
                return this.NotFound();
            }

            homeTask.Date = homeTask.Date;
            homeTask.Description = homeTaskParameter.Description;
            homeTask.Title = homeTaskParameter.Title;
            homeTask.Number = homeTaskParameter.Number;
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", homeTask.Course.Id);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        [HttpGet]
        public IActionResult Delete(int homeTaskId, int courseId)
        {
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);
            //TODO add logic
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }
    }
}