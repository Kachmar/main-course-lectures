using Models.Models;

namespace Service
{
    using System.Collections.Generic;

    public interface IStudentService
    {
        void CreateStudent(Student student);

        IEnumerable<Student> GetAllStudents();

        Student GetStudentById(int id);

        void UpdateStudent(Student student);

        void DeleteStudent(int id);
    }
}