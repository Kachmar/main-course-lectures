using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;

namespace University.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public ActionResult<List<Course>> Get()
        {
            return _courseRepository.GetAll().ToList();
        }

        // GET api/values/
        [HttpGet("{id}")]
        public ActionResult<Course> Get(int id)
        {
            var fromDb = _courseRepository.GetById(id);
            var course = new Course() { Id = fromDb.Id, Name = fromDb.Name };
            course.HomeTasks = new List<HomeTask>();
            fromDb.HomeTasks.ForEach(ht => course.HomeTasks.Add(new HomeTask { Id = ht.Id, Title = ht.Title }));
            return course;
        }


        [HttpPost]
        public ActionResult<Course> Post([FromBody] Course course)
        {
            return _courseRepository.Create(course);
        }

        // PUT 
        [HttpPut("{id}")]
        public void Put([FromBody] Course course)
        {
            _courseRepository.Update(course);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _courseRepository.Delete(id);
        }
    }
}
