using System.Collections.Generic;
using Models.Models;

namespace Models
{
    public interface ICourseRepository
    {
        void Update(Course course);
        void Delete(int id);
        Course GetById(int id);
        IEnumerable<Course> GetAll();
        Course Create(Course course);
    }
}
