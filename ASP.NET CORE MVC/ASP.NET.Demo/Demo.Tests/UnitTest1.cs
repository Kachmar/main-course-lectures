using System;
using System.Collections.Generic;
using ASP.NET.Demo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services;
using Xunit;

namespace Demo.Tests
{
    public class CourseControllerTest
    {
        [Fact]
        public void Courses_ReturnsViewResult_Succesfully()
        {
            //Arrange
           // CourseService courseServiceStub = Substitute.
            CourseController controller = new CourseController(new CourseServiceStub(), null, null);
            //Act
            var response = controller.Courses();
            //Assert
            Assert.IsType<ViewResult>(response);
        }


        [Fact]
        public void Courses_ReturnsViewResultWithCourseList_WhenCoursesExist()
        {
            //Arrange
            CourseController controller = new CourseController(new CourseServiceStub(), null, null);
            //Act
            var response = controller.Courses();
            //Assert
            ViewResult viewResult = response as ViewResult;
            List<Course> actuals = Assert.IsAssignableFrom<List<Course>>(viewResult.ViewData.Model);
            Assert.Equal(2, actuals.Count);
        }
    }

    internal class CourseServiceStub : CourseService
    {
        public override List<Course> GetAllCourses()
        {
            return new List<Course>() { new Course(), new Course() };
        }
    }
}
