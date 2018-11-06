using System;

namespace EntityFramework_Fluent
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new UniversityContext())
            {
                context.Students.Add(new Student()
                {
                    BirthDate = DateTime.Now,
                    Name = "Flui"
                });
                context.Exams.Add(new Exam() { Duration = 5, Location = "317", Name = "Math" });

                context.MyBaseClasses.Add(new MyClassA() { PropertyA = "a", PropertyBase = "base" });
                context.MyBaseClasses.Add(new MyClassB() { PropertyB = "b", PropertyBase = "base" });

                context.SaveChanges();
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
