﻿using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ASP.NET.Demo.ViewModels;

    using DataAccess.ADO;

    using Microsoft.AspNetCore.Routing;

    using Models.Models;

    public class HomeTaskController : Controller
    {
        private readonly Repository repository;

        public HomeTaskController(Repository repository)
        {
            this.repository = repository;
        }

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
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                ViewData["CourseId"] = courseId;
                return View("Edit", homeTask);
            }
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);

            this.repository.CreateHomeTask(homeTask, courseId);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HomeTask homeTask = this.repository.GetHomeTaskById(id);
            if (homeTask == null)
                return this.NotFound();
            ViewData["Action"] = "Edit";

            return View(homeTask);
        }

        [HttpPost]
        public IActionResult Edit(HomeTask homeTaskParameter)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";

                return View(homeTaskParameter);
            }

            var homeTask = this.repository.GetHomeTaskById(homeTaskParameter.Id);

            var routeValueDictionary = new RouteValueDictionary();
            this.repository.UpdateHomeTask(homeTaskParameter);
            routeValueDictionary.Add("id", homeTask.Course.Id);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        [HttpGet]
        public IActionResult Delete(int homeTaskId, int courseId)
        {
            this.repository.DeleteHomeTask(homeTaskId);

            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        [HttpGet]
        public IActionResult Evaluate(int id)
        {
            var homeTask = this.repository.GetHomeTaskById(id);

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
                        IsComplete = homeTaskHomeTaskAssessment.IsComplete,
                        HomeTaskAssessmentId = homeTaskHomeTaskAssessment.Id
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
            var homeTask = this.repository.GetHomeTaskById(model.HomeTaskId);

            if (homeTask == null)
            {
                return this.NotFound();
            }

            if (homeTask.HomeTaskAssessments.Any())
            {
                List<HomeTaskAssessment> assessments = new List<HomeTaskAssessment>();
                foreach (var homeTaskStudent in model.HomeTaskStudents)
                {
                    assessments.Add(new HomeTaskAssessment() { Id = homeTaskStudent.HomeTaskAssessmentId, Date = DateTime.Now, IsComplete = homeTaskStudent.IsComplete });
                }
                this.repository.UpdateHomeTaskAssessments(assessments);
            }
            else
            {
                foreach (var homeTaskStudent in model.HomeTaskStudents)
                {
                    var student = this.repository.GetStudentById(homeTaskStudent.StudentId);
                    homeTask.HomeTaskAssessments.Add(
                        new HomeTaskAssessment
                        {
                            HomeTask = homeTask,
                            IsComplete = homeTaskStudent.IsComplete,
                            Student = student,
                            Date = DateTime.Now

                        });
                }
                this.repository.CreateHomeTaskAssessments(homeTask.HomeTaskAssessments);

            }



            return RedirectToAction("Courses", "Course");
        }
    }
}