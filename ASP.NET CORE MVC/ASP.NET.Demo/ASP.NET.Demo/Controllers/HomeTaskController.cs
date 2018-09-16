using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ASP.NET.Demo.Models;
    using ASP.NET.Demo.ViewModels;

    using Microsoft.AspNetCore.Routing;

    public class HomeTaskController : Controller
    {
        [HttpGet]
        public IActionResult Create(int courseId)
        {
            ViewData["Action"] = "Create";
            ViewData["CourseId"] = courseId;
            var model = new HomeTask();
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Create(HomeTask homeTask, int courseId)
        {
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);

            //TODO add logic
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var homeTask = CourseContainer.CourseCollection.SelectMany(p => p.HomeTasks).SingleOrDefault(h => h.Id == id);
            if (homeTask == null)
                return this.NotFound();
            ViewData["Action"] = "Edit";

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
            var homeTask = CourseContainer.CourseCollection.SelectMany(p => p.HomeTasks).SingleOrDefault(h => h.Id == homeTaskId);
            if (homeTask == null)
            {
                return this.NotFound();
            }
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);

            CourseContainer.CourseCollection.ForEach(course => course.HomeTasks.Remove(homeTask));
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        [HttpGet]
        public IActionResult Evaluate(int id)
        {
            var homeTask = CourseContainer.CourseCollection.SelectMany(p => p.HomeTasks).SingleOrDefault(h => h.Id == id);

            if (homeTask == null)
            {
                return this.NotFound();
            }

            HomeTaskAssessmentViewModel assessmentViewModel =
                new HomeTaskAssessmentViewModel
                {
                    Date = homeTask.Date,
                    Description = homeTask.Description,
                    Title = homeTask.Title,
                    HomeTaskStudents = new List<HomeTaskStudentViewModel>(),
                    HomeTaskId = homeTask.Id
                };

            if (homeTask.HomeTaskAssessments.Any())
            {
                foreach (var homeTaskHomeTaskAssessment in homeTask.HomeTaskAssessments)
                {
                    assessmentViewModel.HomeTaskStudents.Add(new HomeTaskStudentViewModel()
                    {

                        StudentFullName = homeTaskHomeTaskAssessment.Student.Name,
                        StudentId = homeTaskHomeTaskAssessment.Student.Id,
                        IsComplete = homeTaskHomeTaskAssessment.IsComplete
                    });
                }

            }
            else
            {
                foreach (var student in homeTask.Course.Students)
                {
                    assessmentViewModel.HomeTaskStudents.Add(new HomeTaskStudentViewModel() { StudentFullName = student.Name, StudentId = student.Id });
                }
            }

            return this.View(assessmentViewModel);
        }

        public IActionResult SaveEvaluation(HomeTaskAssessmentViewModel model)
        {
            var homeTask = CourseContainer.CourseCollection.SelectMany(p => p.HomeTasks).SingleOrDefault(h => h.Id == model.HomeTaskId);

            if (homeTask == null)
            {
                return this.NotFound();
            }

            if (homeTask.HomeTaskAssessments.Any())
            {
                foreach (var homeTaskStudent in model.HomeTaskStudents)
                {
                    var homeTaskAssessment =
                        homeTask.HomeTaskAssessments.Single(p => p.Student.Id == homeTaskStudent.StudentId);
                    homeTaskAssessment.IsComplete = homeTaskStudent.IsComplete;
                }
            }
            else
            {
                foreach (var homeTaskStudent in model.HomeTaskStudents)
                {
                    var student = CourseContainer.CourseCollection.SelectMany(p => p.Students)
                        .Single(p => p.Id == homeTaskStudent.StudentId);
                    homeTask.HomeTaskAssessments.Add(
                        new HomeTaskAssessment
                        {
                            Id = new Random().Next(),
                            HomeTask = homeTask,
                            IsComplete = homeTaskStudent.IsComplete,
                            Student = student
                        });
                }
            }


            return RedirectToAction("Courses", "Course");
        }
    }
}