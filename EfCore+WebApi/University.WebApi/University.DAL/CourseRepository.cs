using System;
using System.Collections.Generic;
using Models;
using Models.Models;

namespace University.DAL
{
    public class CourseRepository : ICourseRepository
    {
        private readonly UniversityContext _universityContext;

        public CourseRepository(UniversityContext universityContext)
        {
            _universityContext = universityContext;
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Course GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAll()
        {
            throw new NotImplementedException();
        }

        public Course Create(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
