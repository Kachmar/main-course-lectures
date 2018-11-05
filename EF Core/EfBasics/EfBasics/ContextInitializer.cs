namespace EfBasics
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using EfBasics.EF;

    public class ContextInitializer : DropCreateDatabaseAlways<UniversityContext>
    {
        public override void InitializeDatabase(UniversityContext context)
        {
            base.InitializeDatabase(context);
        }

        //protected override void Seed(UniversityContext context)
        //{
        //    context.Students.Add(
        //        new Student()
        //        {
        //            Speciality = "Physics",
        //            BirthDate = DateTime.Now,
        //            Name = "John Smith"
        //        });
        //    context.Students.Add(
        //        new Student()
        //        {
        //            Speciality = "Physics",
        //            BirthDate = DateTime.Now,
        //            Name = "Michael Smith"
        //        });
        //    context.SaveChanges();
        //    base.Seed(context);
        //}

        protected override void Seed(UniversityContext context)
        {
            // the cool thing is that we do not need to worry about the Id keys
            // on SaveChanges the EF will resolve and set the keys.
            var addr1 = context.StudentAddresses.Add(new StudentAddress() { AddressLine = "Lukasha", });

            var addr2 = context.StudentAddresses.Add(new StudentAddress() { AddressLine = "Stryjska" });
            var student1 = context.Students.Add(
                 new Student()
                 {
                     Speciality = "Physics",
                     BirthDate = DateTime.Now,
                     Name = "John Smith",
                     //StudentAddress = addr1
                 });
            context.Exams.Add(new Exam()
            {
                Name = "Literature",
                Student = student1
            });

            context.Exams.Add(new Exam()
            {
                Name = "History",
                Student = student1
            });
            var student2 = context.Students.Add(
                new Student()
                {
                    Speciality = "Physics",
                    BirthDate = DateTime.Now,
                    Name = "Michael Smith",
                    //StudentAddress = addr2
                });
            //we can do either assign student to address or address to student
            addr1.Student = student1;
            addr2.Student = student2;
            context.Exams.Add(new Exam()
            {
                Name = "Math",
                Student = student2
            });

            context.Lecturers.Add(
                new Lecturer() { Name = "Peter", Students = new List<Student>() { student2, student1 } });
            context.Lecturers.Add(
                new Lecturer() { Name = "Ivan", Students = new List<Student>() { student2, student1 } });

            context.Students.Add(
                new DistantStudent()
                {
                    Speciality = "Physics",
                    BirthDate = DateTime.Now,
                    Name = "George Smith",
                    DistantLocation = "Turka"
                });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
