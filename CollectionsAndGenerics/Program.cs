using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student() { GroupName = "LP1", Name = "Павло" };
            Student student2 = new Student() { GroupName = "LP1", Name = "Катерина" };
            Student student3 = new Student() { GroupName = "LP2", Name = "Андрій" };
            Student student4 = new Student() { GroupName = "LP2", Name = "Володя" };
            Student student5 = new Student() { GroupName = "LP3", Name = "Олексій" };
            Student student6 = new Student() { GroupName = "LP3", Name = "Денис" };
             

            Pupil pupil1 = new Pupil() { ClassName = "1B", Name = "Ira" };
            Pupil pupil2 = new Pupil() { ClassName = "2B", Name = "John" };

            //Imagine the situation that I have array of with 2 students and I want to add another student to this group

                //Student[] students = new[] { student1, student2 };

            //    // ?? students.Add
            // students[2] = student3;

            Student[] students = new Student[10];
            students[0] = student1;
            students[1] = student2;

            ////foreach (Student student in students)
            ////{
            ////    Console.WriteLine(student.GroupName);
            ////}

            ////    //So the problem is that I have to think of the number of elements beforehand.
            ////    //Arraylist comes to the rescue

            ////ArrayList<string> list =new 
            ////ArrayList arrayList = new ArrayList();
            ////arrayList.Add(student1);
            ////arrayList.Add(student2);
            ////arrayList.Add(pupil1);
            ////arrayList.Remove(student1);
            ////foreach (var item in arrayList)
            ////{
            ////    Student student = (Student) item;
            ////    Console.WriteLine(student.GroupName);
            ////}
            ////    //arrayList.Add(student3);
            ////    //But you can add not only student!! and this would break everything.
            ////    //Because the Arralist uses Object[] under the hood, the boxing/unboxing operation takes place
            ////    //arrayList.Add(pupil1);
            ////    //arrayList.Add(pupil2);

            ////    foreach (Student element in arrayList)
            ////    {
            ////        Console.WriteLine(element.GroupName);
            ////    }
            ////    //So how to ensure, that we can add dynamically items to collection and make sure that the type is checked, and without boxing and unboxing?
            ////    //Here comes to save a us: generics
            ////List<Pupil> pupils = new List<Pupil>();
            ////pupils.Add(pupil1);
            ////pupils.Add(pupil2);

            ////pupils.Remove(pupil1);
            ////var pupilAtFirstPosition = pupils.ElementAt(0);


            ////Queue<IHuman> studentsQueue = new Queue<IHuman>();
            ////studentsQueue.Enqueue(student1);
            ////studentsQueue.Enqueue(student2);
            ////studentsQueue.Enqueue(student3);
            ////studentsQueue.Enqueue(pupil1);

            ////var firstStudentFromQueue = studentsQueue.Dequeue();
            ////Console.WriteLine($"Only {studentsQueue.Count} left in the queue");

            //Stack<Student> fullMashrutka = new Stack<Student>();
            //fullMashrutka.Push(student1);
            //fullMashrutka.Push(student2);

            //fullMashrutka.Push(student3);

            //var studentFromMarshrutka = fullMashrutka.Pop();
            //Console.WriteLine($"First poped student {studentFromMarshrutka.Name}.");



            ////    //Lets imagine that we have many students, and we need to find the group, knowing the students name.
            ////    //Having a list of students, the algorithm should go through every student one by one.
            ////    //Dictionaries. Why dictionaries: are good, because they are at very efficiently finding an element
            //string searchName = "Катерина";

            //foreach (var student in students)
            //{
            //    if (student.Name == searchName)
            //    {
            //        Console.WriteLine(student.GroupName);
            //        break;
            //    }
            //}

            //Dictionary<string, Student> studentByName = new Dictionary<string, Student>()
            //    {
            //        {"Катерина",student1},
            //        {"Павло",student2},
            //        {"Андрій",student3},
            //        {"Володя",student4},
            //        {"Олексій",student5},
            //        {"Денис",student6},
            //    };

            //   Console.WriteLine(studentByName[searchName]);
            YieldDemo yieldDemo=new YieldDemo();
            foreach (var day in yieldDemo.GetWeekDays())
            {
                
            }

            yieldDemo.GetWeekDays();

            GoodsCollection<Student> collection = new GoodsCollection<Student>();
            collection.Add(student1);
            collection.Add(student2);
            collection.Add(student3);
            foreach (var item in collection)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in collection)
            {
                Console.WriteLine(item.ToString());
            }

            MyGenericClass<Pupil> classGEneric = new MyGenericClass<Pupil>(pupil1);

            //    //Dictionaries allow having unique key, so next command would give runtime exception:
            ////        studentByName.Add("Катерина", studentFromMarshrutka);
            //studentByName["new"] = new Student();
            ////    //Also we can have this syntax:
            ////    studentByName["New"] = new Student();
            //    Console.WriteLine(studentByName.ContainsKey("New"));

            //    //Now lets take a look at custom collection
            //    // In order to be able to use your Custom collection, it should implement the IEnumerable interface.
            //    //Also our new collection should allow Adding and Removing elements, and accessing them by index.
            //    StudentCollection coll=new 
            //    //MyFirstCourseCollection myFirstCourseCollection = new MyFirstCourseCollection();
            //    //myFirstCourseCollection.Add(student1);
            //    //myFirstCourseCollection.Add(student2);
            //    //myFirstCourseCollection.Add(student3);
            //    //foreach (Student element in myFirstCourseCollection)
            //    //{
            //    //    Console.WriteLine(element.GroupName);
            //    //}
            //    //Now Lets take a look at generic class
            //    MyGenericClass<int> intGenericClass = new MyGenericClass<int>(10);

            //    int val = intGenericClass.genericMethod(200);
            //    MyGenericClass<string> stringGenericClass = new MyGenericClass<string>("Hi ");
            //    string strinValue = stringGenericClass.genericMethod("there");

            //    MyFirstCourseCollection<MyGenericClass<int>> myFirstCourseCollection = new MyFirstCourseCollection<MyGenericClass<int>>();
            //    myFirstCourseCollection.Add(intGenericClass);
            //    foreach (MyGenericClass<int> myGenericClass in myFirstCourseCollection)
            //    {

            //    }

            //    //Lets see how yield works
            //    YieldDemo yieldDemo = new YieldDemo();
            //    foreach (var weekDay in yieldDemo.GetWeekDays())
            //    {
            //        Console.WriteLine(weekDay);
            //    }

            Console.ReadLine();
        }

    }
}
