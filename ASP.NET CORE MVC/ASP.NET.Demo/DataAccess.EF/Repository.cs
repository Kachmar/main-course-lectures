using System;
using System.Collections.Generic;

namespace DataAccess.EF
{
    using DataAccess.ADO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using Models.Models;

    public class Repository : IRepository, IDisposable
    {
        private UniversityContext context;

        public Repository(DbContextOptions options)
        {
            context = new UniversityContext(options);
        }

        public Course CreateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public HomeTask CreateHomeTask(HomeTask homeTask, int courseId)
        {
            throw new NotImplementedException();
        }

        public void CreateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments)
        {
            throw new NotImplementedException();
        }

        public Student CreateStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteHomeTask(int homeTaskId)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(int studentId)
        {
            this.context.Remove(this.context.Students.Find(studentId));
        }

        public List<Course> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public HomeTask GetHomeTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetStudentById(int id)
        {
            throw new NotImplementedException();
        }

        public void SetStudentsToCourse(int courseId, IEnumerable<int> studentsId)
        {
            throw new NotImplementedException();
        }

        public void SetStudentToCourses(IEnumerable<int> coursesId, int studentId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            throw new NotImplementedException();
        }

        public void UpdateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student student)
        {
            this.context.Attach(student).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}
