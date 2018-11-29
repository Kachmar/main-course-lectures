using System;

namespace ado.demo
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using ADO.NET.Models;

    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = new DateTime(2018, 9, 23);
            using (SqlConnection connection = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            )
            {
                
                connection.Open();
                var transaction = connection.BeginTransaction();

                Course course = new Course()
                {
                    Name = "Newly inserted Name",
                    StartDate = new DateTime(2018, 12, 28),
                    EndDate = new DateTime(2019, 3, 24),
                    PassCredits = 100
                };

                var insertedCourse = InsertNewCourse(course, connection, transaction);
                insertedCourse.Name = "Name changed";
                Update(insertedCourse, connection, transaction);
                Delete(4, connection, transaction);
                try
                {
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                }
            }
            //Lets not dive deep into configuration manager, since we will be learning .net core and there is no such class.



            //SqlCommand command = connection.CreateCommand();
            //Or

            //Now lets read single value from executed command

            //Now lets just execute the query that does not return results.

            //Now lets select the whole set of data from table and see how to use SqlDataReader

            //Lets Insert some data

            //Lets update some data

            //Lets delete some data

            //What if we try insert record, and one of it's value is null

            //Lets try transactions now

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void Update(Course insertedCourse, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand(
                    @"
UPDATE [dbo].[Course]
   SET [Name] = @Name
      ,[StartDate] = @StartDate      
      ,[EndDate] = @EndDate
      ,[PassCredits] = @PassCredits      
 WHERE Id = @Id
",
                    connection, transaction);
                sqlCommand.Parameters.AddWithValue("@Name", insertedCourse.Name);
                sqlCommand.Parameters.AddWithValue("@Id", insertedCourse.Id);
                sqlCommand.Parameters.AddWithValue("@StartDate", insertedCourse.StartDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", DateTime.MinValue);
                sqlCommand.Parameters.AddWithValue("@PassCredits", insertedCourse.PassCredits);

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception exd)
            {
                transaction.Rollback();
            }
        }

        private static void Delete(int idMoreThen, SqlConnection connection, SqlTransaction transaction)
        {



            SqlCommand sqlCommand = new SqlCommand(
                $@"DELETE FROM [dbo].[Course]
                WHERE Id>{idMoreThen}",
                connection, transaction);
            sqlCommand.ExecuteNonQuery();

        }


        private static Course InsertNewCourse(Course course, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand(
                @"
INSERT INTO [dbo].[Course]
([Name]
,[StartDate]
,[EndDate]
,[PassCredits])
VALUES
(@name
, @startDate
,@endDate
,@passCredits);
SELECT CAST(scope_identity() AS int);",
                connection, transaction);
            command.Parameters.AddWithValue("@name", course.Name);
            command.Parameters.AddWithValue("@startDate", course.StartDate);
            command.Parameters.AddWithValue("@endDate", course.EndDate);

            command.Parameters.AddWithValue("@passCredits", course.PassCredits);
            int id = (int)command.ExecuteScalar();

            course.Id = id;
            return course;
        }



    }
}

