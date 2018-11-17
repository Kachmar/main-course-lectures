namespace Service
{
    using System.Collections.Generic;
    using DataAccess.EF;

    using Models.Models;

    public class StudentService : IStudentService
    {
        private readonly IUniversityRepository<Student> studentRepository;

        public StudentService(IUniversityRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public void CreateStudent(Student student)
        {
            this.studentRepository.Create(student);
            this.studentRepository.SaveChanges();
        }

        public Student GetStudentById(int id)
        {
            return this.studentRepository.GetById(id);
        }

        public void UpdateStudent(Student student)
        {
            this.studentRepository.Update(student);
            this.studentRepository.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            var student = this.studentRepository.GetById(id);
            this.studentRepository.Remove(student);
            this.studentRepository.SaveChanges();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return this.studentRepository.GetAll();
        }
    }
}
