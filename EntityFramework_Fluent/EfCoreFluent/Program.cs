namespace EfCoreFluent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new UniversityContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            using (var context = new UniversityContext())
            {
                var student = new Student()
                {
                    BirthDate = DateTime.Now,
                    Name = "Flui",
                    StudentAddress = new StudentAddress() { AddressLine = "Galaxy" }
                };
                context.Students.Add(student);

                context.Exams.Add(new Exam() { Duration = 5, Location = "317", Name = "Math", Student = student });
                var lecturer = new Lecturer()
                {
                    Name = "Bill",
                    LecturerStudents =
                                           new List<LecturerStudent>()
                                               {
                                                   new LecturerStudent()
                                                       {
                                                           Student = student
                                                       }
                                               }
                };

                context.Lecturers.Add(lecturer);

                context.MyBaseClasses.Add(new MyClassA() { PropertyA = "a", PropertyBase = "base" });
                context.MyBaseClasses.Add(new MyClassB() { PropertyB = "b", PropertyBase = "base" });

                context.SaveChanges();
            }

            using (var context = new UniversityContext())
            {
                var student = context.Students.First();
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
