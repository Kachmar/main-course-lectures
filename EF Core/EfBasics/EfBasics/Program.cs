using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfBasics
{
    using System.Data.Entity;

    using EfBasics.EF;

    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<UniversityContext>(new ContextInitializer());
            //Save changes are necessary to flush the changes
            //using (var context = new UniversityContext())
            //{
            //    Console.WriteLine(context.Students.Count());
            //    context.Students.Add(new Student() { BirthDate = DateTime.Now, Name = "John" });
            //    Console.WriteLine(context.Students.Count());
            //    context.SaveChanges();
            //    Console.WriteLine(context.Students.Count());
            //    Console.ReadKey();
            //}

            //Actual call to the DB is done when we call ToList()
            //This is called deffered loading
            using (var context = new UniversityContext())
            {
                context.Database.Log = msg => Console.WriteLine(msg);
                var studentsQuery = context.Students.Where(p => p.Name == "John");
                //no db call 

                var students = studentsQuery.ToList();
                //  TryInsertInvalidData(context);
            }

            //One-to-Many
            //TryOneToMany();

            //one-to-one
            //TryOneToOne();

            //many-to-many
            TryManyToMany();

            TryInheritance();

            Console.ReadKey();
        }

        private static void TryInheritance()
        {
            using (var context = new UniversityContext())
            {
                var student = context.Students.Find(3);
                var distanceStudent = student as DistantStudent;
                Console.WriteLine(distanceStudent.DistantLocation);
            }
        }

        private static void TryManyToMany()
        {
            //Lets also check the DB schema
            using (var context = new UniversityContext())
            {
                var lecturer = context.Lecturers.Find(1);
                Console.WriteLine($"Students of the lecturer {lecturer.Name}");

                foreach (var lecturerStudent in lecturer.Students)
                {
                    Console.WriteLine(lecturerStudent.Name);
                }

                var student = context.Students.Find(1);
                Console.WriteLine($"Lecturers of the student {student.Name}");
                foreach (var studentLecturer in student.Lecturers)
                {
                    Console.WriteLine(studentLecturer.Name);
                }
            }
        }

        private static void TryOneToOne()
        {
            //having just reference properties on the entities is not enough,
            //to set one-to-one, cause we will get an error, during the db update.
            using (var context = new UniversityContext())
            {
                var student = context.Students.Find(1);
                Console.WriteLine(student.StudentAddress.AddressLine);

                var address = context.StudentAddresses.Find(1);
                Console.WriteLine(address.Student.Name);
            }

        }

        private static void TryOneToMany()
        {
            using (var context = new UniversityContext())
            {
                foreach (var contextStudent in context.Students)
                {
                    Console.WriteLine(contextStudent.Name);
                    //but what would happen if we want to display exam info?
                    //We would get null ref exception, as we need to mark Exams with virtual keyword
                    foreach (var contextStudentExam in contextStudent.Exams)
                    {
                        Console.WriteLine(contextStudentExam.Name);
                    }
                }
            }
        }

        private static void TryInsertInvalidData(UniversityContext context)
        {
            Student student = new Student()
            {
                BirthDate = DateTime.Now,
                Name = "LongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100Chars",

                Speciality = "Math"
            };
            context.Students.Add(student);
            context.SaveChanges();
        }
    }
}
