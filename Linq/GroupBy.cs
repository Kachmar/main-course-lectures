using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
  public  class GroupBy
    {
        public static void Main( )
        {
            var queryGradeLevel =
                from student in StudentClass.students
                group student by student.Year into newGroup
                select newGroup;

            foreach (var nameGroup in queryGradeLevel)
            {
                Console.WriteLine("Key: {0}", nameGroup.Key);
                foreach (var student in nameGroup)
                {
                    Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
                }
            }
            Console.ReadLine(  );
        }
    }
    public class StudentClass
    {
        public enum GradeLevel { FirstYear = 1, SecondYear, ThirdYear, FourthYear };
        public class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public GradeLevel Year;
        }

        public static List<Student> students = new List<Student>
                                                      {
                                                          new Student {FirstName = "Terry", LastName = "Adams",
                                                                          Year = GradeLevel.SecondYear},
                                                          new Student {FirstName = "Fadi", LastName = "Fakhouri",
                                                                          Year = GradeLevel.ThirdYear},
                                                          new Student {FirstName = "Hanying", LastName = "Feng",
                                                                          Year = GradeLevel.FirstYear},
                                                          new Student {FirstName = "Cesar", LastName = "Garcia", 
                                                                          Year = GradeLevel.FourthYear},
                                                          new Student {FirstName = "Debra", LastName = "Garcia", 
                                                                          Year = GradeLevel.ThirdYear},
                                                          new Student {FirstName = "Hugo", LastName = "Garcia",
                                                                          Year = GradeLevel.SecondYear},
                                                          new Student {FirstName = "Sven", LastName = "Mortensen", 
                                                                          Year = GradeLevel.FirstYear},
                                                          new Student {FirstName = "Claire", LastName = "O'Donnell", 
                                                                          Year = GradeLevel.FourthYear},
                                                          new Student {FirstName = "Svetlana", LastName = "Omelchenko", 
                                                                          Year = GradeLevel.SecondYear},
                                                          new Student {FirstName = "Lance", LastName = "Tucker", 
                                                                          Year = GradeLevel.ThirdYear},
                                                          new Student {FirstName = "Michael", LastName = "Tucker",
                                                                          Year = GradeLevel.FirstYear},
                                                          new Student {FirstName = "Eugene", LastName = "Zabokritski",
                                                                          Year = GradeLevel.FourthYear}
                                                      };
    }
}
