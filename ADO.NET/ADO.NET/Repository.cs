using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    using System.Configuration;
    using System.Data.SqlClient;

    using ADO.NET.Models;

    public static class Repository
    {
        public static List<Student> GetAllStudents()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "select Id, Name, BirthDate, PhoneNumber, Email, GitHubLink, Notes from Student",
                    connection);
                List<Student> students = new List<Student>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.Id = reader.GetInt32(0);
                        student.Name = reader.GetString(1);
                        student.BirthDate = reader.GetDateTime(2);
                        student.PhoneNumber = reader.GetString(3);
                        student.Email = reader.GetString(4);
                        student.GitHubLink = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        student.Notes = reader.IsDBNull(6) ? "" : reader.GetString(6);

                        student.Group = GetStudentGroup(student.Id);
                        student.Courses = GetStudentCourses(student.Id);
                        students.Add(student);
                    }
                }


                return students;
            }
        }

        public static List<Course> GetAllCourses()
        {
            List<Course> result = new List<Course>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
                  ,[Name]
                  ,[StartDate]
                  ,[EndDate]
                  ,[PassCredits]
                  ,[HomeTasksCount]
              FROM [dbo].[Course]", connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course course = new Course();
                        course.Id = reader.GetInt32(0);
                        course.Name = reader.GetString(1);
                        course.StartDate = reader.GetDateTime(2);
                        course.EndDate = reader.GetDateTime(3);
                        course.PassCredits = reader.GetInt32(4);
                        course.HomeTasksCount = reader.GetInt32(5);
                        result.Add(course);
                    }
                }
            }

            return result;
        }

        public static List<Group> GetAllGroups()
        {
            List<Group> result = new List<Group>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    @"
              SELECT Id
              ,GroupName
              FROM [dbo].[Group]",
                    connection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Group group = new Group();
                        group.Id = reader.GetInt32(0);
                        group.Name = reader.GetString(1);
                        result.Add(group);
                    }
                }
            }

            return result;
        }

        public static List<Course> GetStudentCourses(int studentId)
        {
            List<Course> result = new List<Course>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
                  ,[Name]
                  ,[StartDate]
                  ,[EndDate]
                  ,[PassCredits]
                  ,[HomeTasksCount]
              FROM [dbo].[Course] as c
              join StudentCourse as sc on sc.CourseId=c.Id
              where sc.StudentId =  {studentId}", connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course course = new Course();
                        course.Id = reader.GetInt32(0);
                        course.Name = reader.GetString(1);
                        course.StartDate = reader.GetDateTime(2);
                        course.EndDate = reader.GetDateTime(3);
                        course.PassCredits = reader.GetInt32(4);
                        course.HomeTasksCount = reader.GetInt32(5);
                        result.Add(course);
                    }
                }
            }

            return result;
        }

        private static Group GetStudentGroup(int studentId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
              SELECT g.Id
              ,g.GroupName
              FROM [dbo].[Group] as g
              join StudentGroup as sg on sg.GroupId = g.Id
              where sg.StudentId =  {studentId}",
                    connection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();
                    Group group = new Group();
                    group.Id = reader.GetInt32(0);
                    group.Name = reader.GetString(1);
                    return group;
                }
            }
        }
        
        public static void DeleteStudent(int studentId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[Student]
                WHERE Id={studentId}", connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public static void DeleteCourse(int courseId)
        {
        }

        public static void DeleteGroup(int groupId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[Group]
                WHERE Id={groupId}", connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public static void UpdateStudent(Student student)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(@"
UPDATE [dbo].[Student]
   SET [Name] = @Name
      ,[BirthDate] = @BirthDate
      ,[PhoneNumber] = @PhoneNumber
      ,[Email] = @Email
      ,[GitHubLink] = @GitHubLink
      ,[Notes] = @Notes
 WHERE Id = @Id
",
                    connection,
                    transaction);
                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                sqlCommand.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                AddWithNullableValue(sqlCommand.Parameters, "@GitHubLink", student.GitHubLink);
                AddWithNullableValue(sqlCommand.Parameters, "@Notes", student.Notes);
                sqlCommand.ExecuteNonQuery();

                SetStudentToCourses(student.Courses.Select(p => p.Id), student.Id, transaction);
                SetStudentToGroup(student.Group.Id, student.Id, transaction);
                transaction.Commit();
            }
        }

        public static void UpdateCourse(Course course)
        {

        }
        public static void UpdateGroup(Group group) { }

        public static Student CreateStudent(Student student)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(@"
INSERT INTO [dbo].[Student]
           ([Name]
           ,[BirthDate]
           ,[PhoneNumber]
           ,[Email]
           ,[GitHubLink]
           ,[Notes])
     VALUES
           (@Name
           ,@BirthDate
           ,@PhoneNumber
           ,@Email
           ,@GitHubLink
           ,@Notes);
SELECT CAST(scope_identity() AS int)
",
                    connection,
                    transaction);
                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                AddWithNullableValue(sqlCommand.Parameters, "@GitHubLink", student.GitHubLink);
                AddWithNullableValue(sqlCommand.Parameters, "@Notes", student.Notes);
                int identity = (int)sqlCommand.ExecuteScalar();
                if (identity == 0)
                {
                    transaction.Rollback();
                    return null;
                }

                student.Id = identity;
                SetStudentToGroup(student.Group.Id, student.Id, transaction);
                SetStudentToCourses(student.Courses.Select(p => p.Id), student.Id, transaction);
                transaction.Commit();
            }

            return student;
        }

        public static Group CreateGroup(string groupName)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    @"
INSERT INTO [dbo].[Group]
           ([GroupName])
     VALUES
           (@GroupName);
SELECT CAST(scope_identity() AS int)
",
                    connection);
                sqlCommand.Parameters.AddWithValue("@GroupName", groupName);
                int identity = (int)sqlCommand.ExecuteScalar();
                if (identity == 0)
                {
                    return null;
                }

                return new Group() { Id = identity, Name = groupName };
            }
        }

        public static Course CreateCourse(Course course) { }

        //This one has problems with datetime conversion
        //        public void CreateStudent(Student student)
        //        {
        //            using (SqlConnection connection = GetConnection())
        //            {
        //                SqlTransaction transaction = connection.BeginTransaction();
        //                SqlCommand sqlCommand = new SqlCommand($@"
        //INSERT INTO [dbo].[Student]
        //           ([Name]
        //           ,[BirthDate]
        //           ,[PhoneNumber]
        //           ,[Email]
        //           ,[GitHubLink]
        //           ,[Notes])
        //     VALUES
        //           ({student.Name}
        //           ,{student.BirthDate}
        //           ,{student.PhoneNumber}
        //           ,{student.Email}
        //           ,{student.GitHubLink}
        //           ,{student.Notes}", connection, transaction);
        //                if (sqlCommand.ExecuteNonQuery() == 0)
        //                {
        //                    transaction.Rollback();
        //                    return;
        //                }
        //                SetStudentToGroup(student.Group.Id, student.Id, transaction);

        //                transaction.Commit();
        //            }
        //        }

        private static SqlParameter AddWithNullableValue(SqlParameterCollection target, string parameterName, object value)
        {
            if (value == null)
            {
                return target.AddWithValue(parameterName, DBNull.Value);
            }

            return target.AddWithValue(parameterName, value);
        }

        private static void SetStudentToGroup(int groupId, int studentId, SqlTransaction transaction)
        {
            SqlCommand sqlCommand = new SqlCommand($@"DELETE FROM [dbo].[StudentGroup]
            WHERE StudentId = {studentId}", transaction.Connection, transaction);
            sqlCommand.ExecuteNonQuery();

            sqlCommand = new SqlCommand($@"INSERT INTO [dbo].[StudentGroup]
           ([GroupId]
           ,[StudentId])
            VALUES
           ({groupId}
           ,{studentId})", transaction.Connection, transaction);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                transaction.Rollback();
            }
        }

        private static void SetStudentToCourses(IEnumerable<int> coursesId, int studentId, SqlTransaction transaction)
        {
            SqlCommand sqlCommand = new SqlCommand($@"DELETE FROM [dbo].[StudentCourse]
            WHERE StudentId = {studentId}", transaction.Connection, transaction);
            sqlCommand.ExecuteNonQuery();
            foreach (var courseId in coursesId)
            {
                sqlCommand = new SqlCommand(
                    $@"INSERT INTO [dbo].[StudentCourse]
           ([CourseId]
           ,[StudentId])
            VALUES
           ({courseId},{studentId})",
                    transaction.Connection,
                    transaction);
                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    transaction.Rollback();
                }
            }
        }

        private static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DemoConnection"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
