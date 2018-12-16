using System;
using System.Collections.Generic;
using DataAccess.EF;
using Models.Models;

namespace Services
{
    public class CourseService
    {
        private readonly UniversityRepository<Lecturer> lecturerRepository;
        private readonly UniversityRepository<Course> courseRepository;
        private readonly UniversityRepository<Student> studentRepository;

        public CourseService(UniversityRepository<Lecturer> lecturerRepository, UniversityRepository<Course> courseRepository, UniversityRepository<Student> studentRepository)
        {
            this.lecturerRepository = lecturerRepository;
            this.courseRepository = courseRepository;
            this.studentRepository = studentRepository;
        }

        public List<Course> GetAllCourses()
        {
            return courseRepository.GetAll();
        }

        public void DeleteCourse(int id)
        {
            this.courseRepository.Remove(id);
        }

        public Course GetCourse(int id)
        {
            return this.courseRepository.GetById(id);
        }

        public void UpdateCourse(Course course)
        {
            this.courseRepository.Update(course);
        }

        public void CreateCourse(Course course)
        {
            this.courseRepository.Create(course);
        }

        public void SetStudentsToCourse(int courseId, IEnumerable<int> studentIds)
        {
            var course = this.courseRepository.GetById(courseId);
            course.Students.Clear();
            foreach (var studentId in studentIds)
            {
                var student = this.studentRepository.GetById(studentId);
                course.Students.Add(new StudentCourse() { Course = course, Student = student });
            }
            this.courseRepository.Update(course);
        }

        public void SetLecturersToCourse(int courseId, IEnumerable<int> lecturerIds)
        {
            var course = this.courseRepository.GetById(courseId);
            course.Lecturers.Clear();
            foreach (var lecturerId in lecturerIds)
            {
                var lecturer = this.lecturerRepository.GetById(lecturerId);
                course.Lecturers.Add(new LecturerCourse() { Course = course, Lecturer = lecturer });
            }
            this.courseRepository.Update(course);
        }
    }
}
