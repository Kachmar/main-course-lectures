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
        public ActionResult<IEnumerable<Course>> Get()
        {
            return _courseRepository.GetAll().ToList();
        }

        // GET api/values/
        [HttpGet("{id}")]
        public ActionResult<Course> Get(int id)
        {
            return _courseRepository.GetById(id);
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
