namespace DataAccess.ADO
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    using Microsoft.Extensions.Options;

    using Models.Models;

    public class Repository
    {
        public string ConnectionString { get; set; }

        public Repository(IOptions<RepositoryOptions> options)
        {
            this.ConnectionString = options.Value.DefaultConnectionString;
        }

        public List<Student> GetAllStudents()
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

                        student.Courses = GetStudentCourses(student.Id);
                        student.HomeTaskAssessments = this.GetHomeTaskAssessmentsByStudentId(student.Id);
                        students.Add(student);
                    }
                }

                return students;
            }
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"
UPDATE [dbo].[HomeTask]
   SET [Date] = @Date
      ,[Description] = @Description
      ,[Number] = @Number
      ,[Title] = @Title
 WHERE Id = @Id
",
                    connection);
                sqlCommand.Parameters.AddWithValue("@Date", homeTask.Date);
                sqlCommand.Parameters.AddWithValue("@Description", homeTask.Description);
                sqlCommand.Parameters.AddWithValue("@Number", homeTask.Number);
                sqlCommand.Parameters.AddWithValue("@Title", homeTask.Title);
                sqlCommand.Parameters.AddWithValue("@Id", homeTask.Id);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteHomeTask(int homeTaskId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[HomeTask]
                WHERE Id={homeTaskId}", connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(@"
UPDATE [dbo].[HomeTaskAssessment]
   SET [Date] = @Date
      ,[IsComplete] = @IsComplete
 WHERE Id = @Id", connection, transaction);
                foreach (var homeTaskHomeTaskAssessment in homeTaskHomeTaskAssessments)
                {
                    sqlCommand.Parameters.Clear();

                    sqlCommand.Parameters.AddWithValue("@Id", homeTaskHomeTaskAssessment.Id);
                    sqlCommand.Parameters.AddWithValue("@Date", homeTaskHomeTaskAssessment.Date);
                    sqlCommand.Parameters.AddWithValue("@IsComplete", homeTaskHomeTaskAssessment.IsComplete);
                    sqlCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        public void CreateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(@"
INSERT INTO [dbo].[HomeTaskAssessment]
           ([IsComplete]
           ,[Date]
           ,[StudentId]
           ,[HomeTaskId])
     VALUES
           (@IsComplete
           ,@Date
           ,@StudentId
           ,@HomeTaskId);",
                    connection,
                    transaction);
                sqlCommand.Parameters.Clear();
                foreach (var homeTaskHomeTaskAssessment in homeTaskHomeTaskAssessments)
                {
                    sqlCommand.Parameters.AddWithValue("@Date", homeTaskHomeTaskAssessment.Date);
                    sqlCommand.Parameters.AddWithValue("@IsComplete", homeTaskHomeTaskAssessment.IsComplete);
                    sqlCommand.Parameters.AddWithValue("@HomeTaskId", homeTaskHomeTaskAssessment.HomeTask.Id);
                    sqlCommand.Parameters.AddWithValue("@StudentId", homeTaskHomeTaskAssessment.Student.Id);
                    sqlCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        public List<Course> GetAllCourses()
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
              FROM [dbo].[Course]", connection);
                ReadCoursesFromCommand(sqlCommand, result);
            }

            return result;
        }

        public void DeleteStudent(int studentId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[Student]
                WHERE Id={studentId}", connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteCourse(int courseId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[Course]
                WHERE Id={courseId}", connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection connection = GetConnection())
            {
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
                    connection);
                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                sqlCommand.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                AddWithNullableValue(sqlCommand.Parameters, "@GitHubLink", student.GitHubLink);
                AddWithNullableValue(sqlCommand.Parameters, "@Notes", student.Notes);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"
UPDATE [dbo].[Course]
   SET [Name] = @Name
      ,[StartDate] = @StartDate
      ,[EndDate] = @EndDate
      ,[PassCredits] = @PassCredits
 WHERE Id = @Id
",
                    connection);
                sqlCommand.Parameters.AddWithValue("@Name", course.Name);
                sqlCommand.Parameters.AddWithValue("@StartDate", course.StartDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", course.EndDate);
                sqlCommand.Parameters.AddWithValue("@PassCredits", course.PassCredits);
                sqlCommand.Parameters.AddWithValue("@Id", course.Id);
                sqlCommand.ExecuteNonQuery();
            }
        }

        //private void SetHomeTasksToCourse(int courseId, IEnumerable<int> homeTaskIds, SqlTransaction transaction)
        //{
        //    SqlCommand sqlCommand = new SqlCommand($@"DELETE FROM [dbo].[StudentCourse]
        //    WHERE CourseId = {courseId}", transaction.Connection, transaction);
        //    sqlCommand.ExecuteNonQuery();
        //    foreach (var studentId in studentsId)
        //    {
        //        sqlCommand = new SqlCommand(
        //            $@"INSERT INTO [dbo].[StudentCourse]
        //   ([CourseId]
        //   ,[StudentId])
        //    VALUES
        //   ({courseId},{studentId})",
        //            transaction.Connection,
        //            transaction);
        //        if (sqlCommand.ExecuteNonQuery() == 0)
        //        {
        //            transaction.Rollback();
        //        }
        //    }
        //}

        public Student CreateStudent(Student student)
        {
            using (SqlConnection connection = GetConnection())
            {
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
                    connection);
                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                AddWithNullableValue(sqlCommand.Parameters, "@GitHubLink", student.GitHubLink);
                AddWithNullableValue(sqlCommand.Parameters, "@Notes", student.Notes);
                int identity = (int)sqlCommand.ExecuteScalar();
                if (identity == 0)
                {
                    return null;
                }

                student.Id = identity;
            }

            return student;
        }

        public Course CreateCourse(Course course)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"
INSERT INTO [dbo].[Course]
           ([Name]
           ,[StartDate]
           ,[EndDate]
           ,[PassCredits])
     VALUES
           (@Name
           ,@StartDate
           ,@EndDate
           ,@PassCredits);
SELECT CAST(scope_identity() AS int)
",
                    connection);
                sqlCommand.Parameters.AddWithValue("@Name", course.Name);
                sqlCommand.Parameters.AddWithValue("@StartDate", course.StartDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", course.EndDate);
                sqlCommand.Parameters.AddWithValue("@PassCredits", course.PassCredits);
                int identity = (int)sqlCommand.ExecuteScalar();
                if (identity == 0)
                {
                    return null;
                }

                course.Id = identity;
            }

            return course;
        }

        public Course GetCourse(int id)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
                  ,[Name]
                  ,[StartDate]
                  ,[EndDate]
                  ,[PassCredits]
              FROM [dbo].[Course]
              WHERE [ID] = {id}", connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();

                    Course course = new Course();
                    course.Id = reader.GetInt32(0);
                    course.Name = reader.GetString(1);
                    course.StartDate = reader.GetDateTime(2);
                    course.EndDate = reader.GetDateTime(3);
                    course.PassCredits = reader.GetInt32(4);
                    course.Students = this.GetStudentsByCourseId(course.Id);
                    course.HomeTasks = this.GetHomeTasksByCourseId(course.Id);
                    course.Lecturers = this.GetLecturersByCourseId(course.Id);
                    return course;
                }
            }
        }

        private List<Lecturer> GetLecturersByCourseId(int courseId)
        {
            List<Lecturer> result = new List<Lecturer>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT l.[Id]
                  ,[Name]
                  ,[BirthDate]
              FROM [dbo].[Lecturer] as l
               JOIN [LecturerCourse] as lc
                ON l.Id=lc.LecturerId
                WHERE lc.CourseId={courseId}", connection);

                this.ReadLecturersFromCommand(sqlCommand, result);
            }
            return result;
        }

        public Student GetStudentById(int id)
        {
            var allStudents = GetAllStudents();
            return allStudents.SingleOrDefault(p => p.Id == id);
        }

        public HomeTask CreateHomeTask(HomeTask homeTask, int courseId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"
INSERT INTO [dbo].[HomeTask]
           ([Title]
           ,[Date]
           ,[Description]
           ,[Number]
           ,[CourseId])
     VALUES
           (@Title
           ,@Date
           ,@Description
           ,@Number
           ,@CourseId);
SELECT CAST(scope_identity() AS int)
",
                    connection);
                sqlCommand.Parameters.AddWithValue("@Title", homeTask.Title);
                sqlCommand.Parameters.AddWithValue("@Date", homeTask.Date);
                sqlCommand.Parameters.AddWithValue("@Description", homeTask.Description);
                sqlCommand.Parameters.AddWithValue("@Number", homeTask.Number);
                sqlCommand.Parameters.AddWithValue("@CourseId", courseId);

                int identity = (int)sqlCommand.ExecuteScalar();
                if (identity == 0)
                {
                    return null;
                }

                homeTask.Id = identity;
            }

            return homeTask;
        }

        public HomeTask GetHomeTaskById(int id)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
                  ,[Title]
                  ,[Date]
                  ,[Description]
                  ,[Number]
                  ,[CourseId]
              FROM [dbo].[HomeTask]
              WHERE [Id] = {id}", connection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();
                    HomeTask homeTask = new HomeTask();
                    homeTask.Id = reader.GetInt32(0);
                    homeTask.Title = reader.GetString(1);
                    homeTask.Date = reader.GetDateTime(2);
                    homeTask.Description = reader.GetString(3);
                    homeTask.Number = reader.GetInt16(4);
                    int courseId = reader.GetInt32(5);
                    homeTask.HomeTaskAssessments = GetHomeTaskAssessmentsByHomeTaskId(homeTask.Id);
                    homeTask.Course = GetCourse(courseId);
                    return homeTask;
                }
            }
        }

        public void SetStudentsToCourse(int courseId, IEnumerable<int> studentsId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[StudentCourse]
            WHERE CourseId = {courseId}",
                    connection, transaction);
                sqlCommand.ExecuteNonQuery();
                foreach (var studentId in studentsId)
                {
                    sqlCommand = new SqlCommand(
                        $@"INSERT INTO [dbo].[StudentCourse]
           ([CourseId]
           ,[StudentId])
            VALUES
           ({courseId},{studentId})",
                        connection);
                    sqlCommand.ExecuteNonQuery();
                }
                transaction.Commit();
            }
        }

        private List<Course> GetStudentCourses(int studentId)
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
  FROM [dbo].[Course] as c
  join StudentCourse as sc
  on c.Id=sc.CourseId
  where sc.StudentId = {studentId}", connection);

                ReadCoursesFromCommand(sqlCommand, result);
            }

            return result;
        }

        private List<Course> GetLecturerCourses(int lecturerId)
        {
            List<Course> result = new List<Course>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                  SELECT c.[Id]
      ,[Name]
      ,[StartDate]
      ,[EndDate]
      ,[PassCredits]
  FROM [dbo].[Course] as c
  join LecturerCourse as lc
  on c.Id=lc.CourseId
  where lc.LecturerId = {lecturerId}", connection);

                ReadCoursesFromCommand(sqlCommand, result);
            }

            return result;
        }

        private static void ReadCoursesFromCommand(SqlCommand sqlCommand, List<Course> result)
        {
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
                    result.Add(course);
                }
            }
        }

        private SqlParameter AddWithNullableValue(SqlParameterCollection target, string parameterName, object value)
        {
            if (value == null)
            {
                return target.AddWithValue(parameterName, DBNull.Value);
            }

            return target.AddWithValue(parameterName, value);
        }

        //public void SetStudentToCourses(IEnumerable<int> coursesId, int studentId)
        //{
        //    using (SqlConnection connection = GetConnection())
        //    {
        //        SqlCommand sqlCommand = new SqlCommand(
        //            $@"DELETE FROM [dbo].[StudentCourse]
        //    WHERE StudentId = {studentId}",
        //            connection);
        //        sqlCommand.ExecuteNonQuery();
        //        foreach (var courseId in coursesId)
        //        {
        //            sqlCommand = new SqlCommand(
        //                $@"INSERT INTO [dbo].[StudentCourse]
        //   ([CourseId]
        //   ,[StudentId])
        //    VALUES
        //   ({courseId},{studentId})",
        //                connection);
        //            sqlCommand.ExecuteNonQuery();
        //        }
        //    }
        //}

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        private List<HomeTaskAssessment> GetHomeTaskAssessmentsByHomeTaskId(int homeTaskId)
        {
            List<HomeTaskAssessment> result = new List<HomeTaskAssessment>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
              ,[IsComplete]
              ,[Date]
              ,[StudentId]
              ,[HomeTaskId]
              FROM [dbo].[HomeTaskAssessment]
              where HomeTaskId =  {homeTaskId}", connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HomeTaskAssessment homeTaskAssessment = new HomeTaskAssessment();
                        homeTaskAssessment.Id = reader.GetInt32(0);
                        homeTaskAssessment.IsComplete = reader.GetBoolean(1);
                        homeTaskAssessment.Date = reader.GetDateTime(2);
                        int studentId = reader.GetInt32(3);
                        //homeTaskAssessment.Student = this.GetStudentById(studentId);
                        //homeTaskAssessment.HomeTask = GetHomeTaskById(homeTaskId);
                        result.Add(homeTaskAssessment);
                    }
                }
            }

            return result;
        }

        private List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId)
        {
            List<HomeTaskAssessment> result = new List<HomeTaskAssessment>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
              ,[IsComplete]
              ,[Date]
              ,[StudentId]
              ,[HomeTaskId]
              FROM [dbo].[HomeTaskAssessment]
              where StudentId =  {studentId}", connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HomeTaskAssessment homeTaskAssessment = new HomeTaskAssessment();
                        homeTaskAssessment.Id = reader.GetInt32(0);
                        homeTaskAssessment.IsComplete = reader.GetBoolean(1);
                        homeTaskAssessment.Date = reader.GetDateTime(2);
                        //int homeTaskId = reader.GetInt32(4);
                        //homeTaskAssessment.Student = this.GetStudentById(studentId);
                        //homeTaskAssessment.HomeTask = GetHomeTaskById(homeTaskId);
                        result.Add(homeTaskAssessment);
                    }
                }
            }

            return result;
        }

        private List<HomeTask> GetHomeTasksByCourseId(int courseId)
        {
            List<HomeTask> homeTasks = new List<HomeTask>();

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
                  ,[Title]
                  ,[Date]
                  ,[Description]
                  ,[Number]
              FROM [dbo].[HomeTask]
              WHERE [CourseId] = {courseId}", connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HomeTask homeTask = new HomeTask();
                        homeTask.Id = reader.GetInt32(0);
                        homeTask.Title = reader.GetString(1);
                        homeTask.Date = reader.GetDateTime(2);
                        homeTask.Description = reader.GetString(3);
                        homeTask.Number = reader.GetInt16(4);
                        homeTasks.Add(homeTask);
                    }
                }
            }

            return homeTasks;
        }

        private List<Student> GetStudentsByCourseId(int courseId)
        {
            List<Student> result = new List<Student>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                  SELECT  [Id]
                          ,[Name]
                          ,[BirthDate]
                          ,[PhoneNumber]
                          ,[Email]
                          ,[GitHubLink]
                          ,[Notes]
                      FROM [dbo].[Student] as s
                      join StudentCourse as sc
                      on s.Id=sc.StudentId
                      where sc.CourseId = {courseId}", connection);

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

                        result.Add(student);
                    }

                    return result;
                }
            }
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"
UPDATE [dbo].[Lecturer]
   SET [Name] = @Name
      ,[BirthDate] = @BirthDate
 WHERE Id = @Id
",
                    connection);
                sqlCommand.Parameters.AddWithValue("@Name", lecturer.Name);
                sqlCommand.Parameters.AddWithValue("@Id", lecturer.Id);
                sqlCommand.Parameters.AddWithValue("@BirthDate", lecturer.BirthDate);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteLecturer(int id)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[Lecturer]
                WHERE Id={id}",
                    connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Lecturer GetLecturerById(int id)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
                  ,[Name]
                  ,[BirthDate]
              FROM [dbo].[Lecturer]
              WHERE [ID] = {id}", connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();

                    Lecturer lecturer = new Lecturer();
                    lecturer.Id = reader.GetInt32(0);
                    lecturer.Name = reader.GetString(1);
                    lecturer.BirthDate = reader.GetDateTime(2);
                    lecturer.Courses = this.GetLecturerCourses(lecturer.Id);

                    return lecturer;
                }
            }
        }

        public List<Lecturer> GetAllLecturers()
        {
            List<Lecturer> result = new List<Lecturer>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $@"
                   SELECT [Id]
                  ,[Name]
                  ,[BirthDate]
              FROM [dbo].[Lecturer]", connection);

                this.ReadLecturersFromCommand(sqlCommand, result);
            }
            return result;
        }

        private void ReadLecturersFromCommand(SqlCommand sqlCommand, List<Lecturer> result)
        {
            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Lecturer lecturer = new Lecturer();
                    lecturer.Id = reader.GetInt32(0);
                    lecturer.Name = reader.GetString(1);
                    lecturer.BirthDate = reader.GetDateTime(2);
                    lecturer.Courses = this.GetLecturerCourses(lecturer.Id);
                    result.Add(lecturer);
                }
            }
        }

        public Lecturer CreateLecturer(Lecturer lecturer)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"
INSERT INTO [dbo].[Lecturer]
           ([Name]
           ,[BirthDate])
     VALUES
           (@Name
           ,@BirthDate);
SELECT CAST(scope_identity() AS int)
",
                    connection);
                sqlCommand.Parameters.AddWithValue("@Name", lecturer.Name);
                sqlCommand.Parameters.AddWithValue("@BirthDate", lecturer.BirthDate);
                int identity = (int)sqlCommand.ExecuteScalar();
                if (identity == 0)
                {
                    return null;
                }

                lecturer.Id = identity;
            }

            return lecturer;
        }

        public void SetLecturersToCourse(int courseId, IEnumerable<int> lecturerIds)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(
                    $@"DELETE FROM [dbo].[LecturerCourse]
            WHERE CourseId = {courseId}",
                    connection, transaction);
                sqlCommand.ExecuteNonQuery();
                foreach (var lecturerId in lecturerIds)
                {
                    sqlCommand = new SqlCommand(
                        $@"INSERT INTO [dbo].[LecturerCourse]
           ([CourseId]
           ,[LecturerId])
            VALUES
           ({courseId},{lecturerId})",
                        connection, transaction);
                    sqlCommand.ExecuteNonQuery();
                }
                transaction.Commit();
            }
        }
    }
}
