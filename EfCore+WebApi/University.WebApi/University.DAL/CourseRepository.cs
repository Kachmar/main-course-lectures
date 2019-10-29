using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var existingCourse = _universityContext.Courses.Find(course.Id);
            existingCourse.Name = course.Name;
            _universityContext.Update(existingCourse);
            _universityContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var course = _universityContext.Courses.Find(id);
            _universityContext.Courses.Remove(course);
           // _universityContext.Entry(course).State = EntityState.Deleted;
            _universityContext.SaveChanges();

        }

        public Course GetById(int id)
        {
            var course = _universityContext.Courses.Include(p => p.HomeTasks).FirstOrDefault(p => p.Id == id);
            return course;
        }

        public IEnumerable<Course> GetAll()
        {
            return _universityContext.Courses.Include(p => p.HomeTasks).ToList();
        }

        public Course Create(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
