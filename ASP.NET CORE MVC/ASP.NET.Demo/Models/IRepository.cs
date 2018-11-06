using System.Collections.Generic;
using Models.Models;

namespace DataAccess.ADO
{
    public interface IRepository
    {
        Course CreateCourse(Course course);
        HomeTask CreateHomeTask(HomeTask homeTask, int courseId);
        void CreateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments);
        Student CreateStudent(Student student);
        void DeleteCourse(int courseId);
        void DeleteHomeTask(int homeTaskId);
        void DeleteStudent(int studentId);
        List<Course> GetAllCourses();
        List<Student> GetAllStudents();
        Course GetCourse(int id);
        HomeTask GetHomeTaskById(int id);
        Student GetStudentById(int id);
        void SetStudentsToCourse(int courseId, IEnumerable<int> studentsId);
        void SetStudentToCourses(IEnumerable<int> coursesId, int studentId);
        void UpdateCourse(Course course);
        void UpdateHomeTask(HomeTask homeTask);
        void UpdateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments);
        void UpdateStudent(Student student);

        void SaveChanges();
    }
}