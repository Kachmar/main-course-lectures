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
            Student student1 = new Student() { GroupName = "LP1" };
            Student student2 = new Student() { GroupName = "LP2" };
            Student student3 = new Student() { GroupName = "LP3" };

            Pupil pupil1 = new Pupil() { ClassName = "1B" };
            Pupil pupil2 = new Pupil() { ClassName = "2B" };

            // I want all the people to be in one collection we would use here boxing and unboxing, which is a slow operation
            //object[] array = new Object[] { student1, student2, student3, pupil1, pupil2 };
            //foreach (var element in array)
            //{

            //}
            //Same we can do with ArrayList, however we can iteratively add elements to list
            //ArrayList arrayList = new ArrayList();
            //arrayList.Add(student1);
            //arrayList.Add(student2);
            //arrayList.Add(student3);
            //arrayList.Add(pupil1);
            //arrayList.Add(pupil2);

            //foreach (Pupil element in arrayList)
            //{
            //    Console.WriteLine(element.ClassName);
            //}

            //Here comes to save a us: generics
            List<Pupil> pupils = new List<Pupil>();
            pupils.Add(pupil1);
            pupils.Add(pupil2);
            pupils.Remove(pupil1);
            var pupilAtFirstPosition = pupils.ElementAt(0);

            Queue<Student> studentsQueue = new Queue<Student>();
            studentsQueue.Enqueue(student1);
            studentsQueue.Enqueue(student2);
            studentsQueue.Enqueue(student3);

            Student firstStudentFromQueue = studentsQueue.Dequeue();
            Console.WriteLine($"Only {studentsQueue.Count} left in the queue");

            Stack<Student> fullMashrutka = new Stack<Student>();
            fullMashrutka.Push(student1);
            fullMashrutka.Push(student2);

            fullMashrutka.Push(student3);

            var studentFromMarshrutka = fullMashrutka.Pop();
            Console.WriteLine($"First poped student {studentFromMarshrutka.GroupName}.");

            //Dictionaries. Why dictionaries: are good, because they are at very efficiently finding an element
            //Dictionary<int, string> monthCodes = new Dictionary<int, string>();
            //monthCodes.Add(1, "January");
            //monthCodes.Add(2, "February");
            //monthCodes.Add(3, "March");
            //monthCodes.Add(4, "April");
            //monthCodes.Add(5, "May");
            //monthCodes.Add(6, "June");
            //monthCodes.Add(7, "July");
            //monthCodes.Add(8, "August");
            //monthCodes.Add(9, "September");
            ////add by index
            //monthCodes[9] = "September";
            //monthCodes.Add(10, "October");
            ////cannot contain duplicates
            //monthCodes.Add(10, "November");
            //monthCodes.Add(12, "December");

            //Console.WriteLine($"second month is {monthCodes[2]}");
            //if (monthCodes.ContainsKey(13))
            //{
            //    Console.WriteLine("Wow, extra month found");
            //}

            Dictionary<string, Pupil> pupilsByClasses = new Dictionary<string, Pupil>();
            pupilsByClasses["1B"] = pupil1;
            pupilsByClasses["2B"] = pupil2;

            //Now lets take a look at custom collection
            //MyFirstCourseCollection myFirstCourseCollection = new MyFirstCourseCollection();
            //myFirstCourseCollection.Add(student1);
            //myFirstCourseCollection.Add(student2);
            //myFirstCourseCollection.Add(student3);
            //foreach (Student element in myFirstCourseCollection)
            //{
            //    Console.WriteLine(element.GroupName);
            //}
            //Now Lets take a look at generic class
            MyGenericClass<int> intGenericClass = new MyGenericClass<int>(10);

            int val = intGenericClass.genericMethod(200);
            MyGenericClass<string> stringGenericClass = new MyGenericClass<string>("Hi ");
            string strinValue = stringGenericClass.genericMethod("there");

            MyFirstCourseCollection<MyGenericClass<int>> myFirstCourseCollection = new MyFirstCourseCollection<MyGenericClass<int>>();
            myFirstCourseCollection.Add(intGenericClass);
            foreach (MyGenericClass<int> myGenericClass in myFirstCourseCollection)
            {

            }

            //Lets see how yield works
            YieldDemo yieldDemo = new YieldDemo();
            foreach (var weekDay in yieldDemo.GetWeekDays())
            {
                Console.WriteLine(weekDay);
            }

            Console.ReadLine();
        }

        class Student : IHuman
        {
            public string GroupName { get; set; }
        }

        class Pupil : IHuman
        {
            public string ClassName { get; set; }
        }
        interface IHuman
        {

        }
        class MyGenericClass<T> where T : IHuman
        {
            private T genericMemberVariable;

            public MyGenericClass(T value)
            {
                genericMemberVariable = value;
            }

            public T genericMethod(T genericParameter)
            {
                Console.WriteLine("Parameter type: {0}, value: {1}", typeof(T).ToString(), genericParameter);
                Console.WriteLine("Return type: {0}, value: {1}", typeof(T).ToString(), genericMemberVariable);

                return genericMemberVariable;
            }

            public T genericProperty { get; set; }
        }

        class YieldDemo
        {
            public IEnumerable<string> GetWeekDays()
            {
                yield return "Mon";
                yield return "Tue";
                yield return "Wed";
                yield return "Thu";
                yield return "Fri";
                yield return "Sat";
                yield return "Sun";
            }
        }
        class MyFirstCourseCollection<T> : IEnumerator<T>, IEnumerable<T>
        {
            private T[] container;
            private int position = -1;

            public MyFirstCourseCollection()
            {
                this.container = new T[0];
            }

            public void Add(T element)
            {

                T[] newContainer = new T[this.container.Length + 1];
                for (int i = 0; i < this.container.Length; i++)
                {
                    newContainer[i] = this.container[i];
                }
                newContainer[this.container.Length] = element;
                this.container = newContainer;
            }

            public void Dispose()
            {
                this.container = null;
            }

            public bool MoveNext()
            {
                position++;
                return position < this.container.Length;
            }

            public void Reset()
            {
                this.position = 0;
            }

            T IEnumerator<T>.Current
            {
                get
                {
                    return this.container[this.position];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.container[this.position];
                }
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return this;
            }

            public IEnumerator GetEnumerator()
            {
                return this;
            }

        }

        //class MyFirstCourseCollection : IEnumerator, IEnumerable
        //{
        //    private Student[] container;
        //    private int position = -1;

        //    public MyFirstCourseCollection()
        //    {
        //        this.container = new Student[0];
        //    }

        //    public void Add(Student student)
        //    {
        //        if (!student.GroupName.EndsWith("1"))
        //        {
        //            return;
        //        }

        //        Student[] newContainer = new Student[this.container.Length + 1];
        //        for (int i = 0; i < this.container.Length; i++)
        //        {
        //            newContainer[i] = this.container[i];
        //        }
        //        newContainer[this.container.Length] = student;
        //        this.container = newContainer;
        //    }

        //    public void Dispose()
        //    {
        //        this.container = null;
        //    }

        //    public bool MoveNext()
        //    {
        //        position++;
        //        return position < this.container.Length;
        //    }

        //    public void Reset()
        //    {
        //        this.position = 0;
        //    }

        //    public Student Current
        //    {
        //        get
        //        {
        //            return this.container[this.position];
        //        }
        //    }

        //    object IEnumerator.Current
        //    {
        //        get
        //        {
        //            return this.Current;
        //        }
        //    }

        //    public IEnumerator GetEnumerator()
        //    {
        //        return this;
        //    }

        //}
    }
}
