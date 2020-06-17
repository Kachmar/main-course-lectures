using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using University.WebApi.Dto;

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
        public ActionResult<List<CourseDto>> Get()
        {
            return _courseRepository.GetAll().Select(CourseDto.FromModel).ToList();
        }

        // GET api/values/
        [HttpGet("{id}")]
        public ActionResult<CourseDto> Get(int id)
        {
            var fromDb = _courseRepository.GetById(id);
            if (fromDb == null)
            {
                return NotFound();
            }
            var course = new CourseDto() { Id = fromDb.Id, Name = fromDb.Name };
            fromDb.HomeTasks.ForEach(ht => course.HomeTasks.Add(new HomeTaskDto { Id = ht.Id, Title = ht.Title }));
            return course;
        }


        [HttpPost]
        public ActionResult<CourseDto> Post([FromBody] CourseDto courseDto)
        {
            var course = new Course()
            {
                Name = courseDto.Name
            };
            course.HomeTasks.AddRange(courseDto.HomeTasks.Select(p => new HomeTask()
            {
                Title = p.Title
            }));

            var newCourse= _courseRepository.Create(course);
            return CourseDto.FromModel(newCourse);

        }

        // PUT 
        [HttpPut("{id}")]
        public void Put([FromBody] CourseDto course)
        {
            _courseRepository.Update(course.ToModel());
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _courseRepository.Delete(id);
        }
    }
}
