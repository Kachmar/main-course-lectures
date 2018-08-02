
namespace ADO.NET
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using System.Linq;

    using ADO.NET.Models;

    class Program
    {
        static void Main(string[] args)
        {
            // Insert
            List<Group> groups = Repository.GetAllGroups();
            List<Course> courses = Repository.GetAllCourses();
            Student newStudent = new Student()
            {
                Courses = courses.Take(1).ToList(),
                Group = groups.First(),
                BirthDate = DateTime.Now,
                Email = "Test",
                GitHubLink = "Test",
                Name = "test",
                PhoneNumber = "000"
            };
            var insertedStudent = Repository.InsertStudent(newStudent);

            //Update
            var students = Repository.GetAllStudents();
            var student = students.SingleOrDefault(p => p.Name == "test");
            student.Notes += "; Is employed";
            student.Courses = courses.Take(2).ToList();
            student.Group = groups.Last();

            Repository.UpdateStudent(student);
            //Delete new student
            Repository.DeleteStudent(insertedStudent.Id);

            CaseWhenGroupWasDeletedByAnotherUser();
        }

        private static void CaseWhenGroupWasDeletedByAnotherUser()
        {
            List<Course> courses = Repository.GetAllCourses();

            Group newGroup = Repository.CreateGroup("Demo");
            Student newStudent = new Student()
            {
                Courses = courses.Take(1).ToList(),
                Group = newGroup,
                BirthDate = DateTime.Now,
                Email = "Test",
                GitHubLink = "Test",
                Name = "test",
                PhoneNumber = "000"
            };
            Repository.DeleteGroup(newGroup.Id);
            var insertedStudent = Repository.InsertStudent(newStudent);
        }
    }
}