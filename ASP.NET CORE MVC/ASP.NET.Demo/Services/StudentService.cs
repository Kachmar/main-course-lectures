using DataAccess.EF;
using Models.Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class StudentService
    {
        private readonly UniversityRepository<Student> studentRepository;

        public StudentService(UniversityRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public List<Student> GetAllStudents()
        {
            return this.studentRepository.GetAll();
        }

        public Student GetStudentById(int studentId)
        {
            return this.studentRepository.GetById(studentId);
        }

        public void UpdateStudent(Student student)
        {
            this.studentRepository.Update(student);
        }

        public void DeleteStudent(int id)
        {
            this.studentRepository.Remove(id);
        }

        public void CreateStudent(Student student)
        {
            this.studentRepository.Create(student);
        }
    }
}
