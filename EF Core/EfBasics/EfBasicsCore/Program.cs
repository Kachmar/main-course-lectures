using System;

namespace EfBasicsCore
{
    using System.Collections.Generic;
    using System.Linq;

    using EfBasicsCore.EF;

    using EFSaving.RelatedData;

    using Microsoft.EntityFrameworkCore;

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new UniversityContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
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

            //ShowLazyLoading();
            ShowDataSeedingUsingModelCreation();
            ShowDataSeedingByDbContextSaveChanges();
            //ShowInvalidDataError();

            ShowDeferredLoading();

            //One-to-Many
            TryOneToMany();

            //one-to-one
            TryOneToOne();

            //many-to-many
            TryManyToMany();

            TryInheritance();

            Console.ReadKey();
        }

        private static void ShowDataSeedingByDbContextSaveChanges()
        {
            //The issue with such approach is that we need to control, that this code would be excuted only once, 
            // and that wan we have many same apps, we need to handle concurrency.
            using (var context = new UniversityContext())
            {
                // the cool thing is that we do not need to worry about the Id keys
                // on SaveChanges the EF will resolve and set the keys.
                var student1 = new Student()
                {
                    Speciality = "Physics",
                    BirthDate = DateTime.Now,
                    Name = "John Smith",
                    StudentAddress = new StudentAddress() { AddressLine = "Lukasha" }
                };
                context.Students.Add(student1);
                var student2 = new Student()
                {
                    Speciality = "Physics",
                    BirthDate = DateTime.Now,
                    Name = "Michael Smith",
                    StudentAddress = new StudentAddress() { AddressLine = "Stryjska" }
                };

                Exam exam1 = new Exam() { Name = "Literature", Student = student1 };

                context.Exams.Add(exam1);

                //context.Exams.Add(new Exam()
                //{
                //    Name = "History",
                //    Student = student1
                //});
                //var student2 = .Entity;
                ////we can do either assign student to address or address to student
                //addr1.Student = student1;
                //addr2.Student = student2;
                //context.Exams.Add(new Exam()
                //{
                //    Name = "Math",
                //    Student = student2
                //});

                //var lecturer1 = context.Lecturers.Add(new Lecturer() { Name = "Peter" }).Entity;
                //var lecturer2 = context.Lecturers.Add(new Lecturer() { Name = "Ivan" }).Entity;

                //lecturer1.LecturerStudents =
                //    new List<LecturerStudent>() { new LecturerStudent() { Lecturer = lecturer1, Student = student1 } };
                //lecturer2.LecturerStudents =
                //    new List<LecturerStudent>() { new LecturerStudent() { Lecturer = lecturer2, Student = student2 } };

                //context.Students.Add(
                //    new DistantStudent()
                //    {
                //        Speciality = "Physics",
                //        BirthDate = DateTime.Now,
                //        Name = "George Smith",
                //        DistantLocation = "Turka"
                //    });

                context.SaveChanges();
            }
        }

        private static void ShowDeferredLoading()
        {
            //Actual call to the DB is done when we call ToList()
            //This is called deferred loading
            using (var context = new UniversityContext())
            {
                var studentsQuery = context.Students.Where(p => p.Name == "John");
                //no db call 

                var students = studentsQuery.ToList();
            }
        }

        private static void ShowDataSeedingUsingModelCreation()
        {
            //No need to worry that ModelCreation would run multiple times, since it saves changes to db, only on DB Create
            //The issue with this type of aproach, is that we need to set Ids explicitly
            using (var context = new UniversityContext())
            {
                var studentsQuery = context.Students.Where(p => p.Name == "John");

                var students = studentsQuery.ToList();
            }
        }

        private static void ShowLazyLoading()
        {
            using (var context = new UniversityContext())
            {
                context.Students.Add(new Student() { BirthDate = DateTime.Now, Name = "John" });
                context.SaveChanges();
                var studentsQuery = context.Students.Where(p => p.Name == "John");
                //no db call ...lets check logs

                var students = studentsQuery.ToList();
                //  ShowInvalidDataError(context);
            }
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
                Console.WriteLine($"LecturerStudents of the lecturer {lecturer.Name}");

                foreach (var lecturerStudent in lecturer.LecturerStudents)
                {
                    Console.WriteLine(lecturerStudent.Student.Name);
                }

                var student = context.Students.Find(1);
                Console.WriteLine($"LecturerStudents of the student {student.Name}");
                foreach (var studentLecturer in student.LecturerStudents)
                {
                    Console.WriteLine(studentLecturer.Lecturer.Name);
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

        private static void ShowInvalidDataError()
        {
            using (var context = new UniversityContext())
            {
                Student student = new Student()
                {
                    BirthDate = DateTime.Now,
                    Name =
                                              "LongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100CharsLongLongMoreThan100Chars",
                    Speciality = "Math"
                };
                context.Students.Add(student);
                context.SaveChanges();
            }
        }
    }
}
