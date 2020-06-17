using System.Collections.Generic;
using System.Linq;
using Models.Models;

namespace University.WebApi.Dto
{
    public class CourseDto
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public virtual List<HomeTaskDto> HomeTasks { get; set; } = new List<HomeTaskDto>();

        public Course ToModel()
        {
            return new Course()
            {
                Name = Name,
                Id = Id,
                HomeTasks = HomeTasks.Select(p => p.ToModel()).ToList()
            };
        }

        public static CourseDto FromModel (Course course)
        {
            return new CourseDto()
            {
                Id = course.Id, Name = course.Name,
                HomeTasks = course.HomeTasks.Select(HomeTaskDto.FromModel).ToList()
            };
        }

    }

}