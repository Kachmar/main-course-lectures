using DataAccess.EF;
using Models.Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class StudentService
    {
        private readonly UniversityRepository<Student> studentRepository;

        public StudentService()
        {
        }

        public StudentService(UniversityRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public virtual List<Student> GetAllStudents()
        {
            return this.studentRepository.GetAll();
        }

        public virtual Student GetStudentById(int studentId)
        {
            return this.studentRepository.GetById(studentId);
        }

        public virtual void UpdateStudent(Student student)
        {
            this.studentRepository.Update(student);
        }

        public virtual void DeleteStudent(int id)
        {
            this.studentRepository.Remove(id);
        }

        public virtual void CreateStudent(Student student)
        {
            this.studentRepository.Create(student);
        }
    }
}
