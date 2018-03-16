using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XML
{
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> ImportStudents = new List<Student>();

            Import(ImportStudents);
            Serialize(ImportStudents);
            var result = Export(ImportStudents);
        }

        private static void Serialize(List<Student> importStudents)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, importStudents);
            }
        }

        private static XDocument Export(List<Student> importStudents)
        {
            XDocument document = new XDocument();
            XElement root = new XElement("Students");
            document.Add(root);

            foreach (var student in importStudents)
            {
                XElement studentElement = new XElement("Student", new XAttribute("firstName", student.FirstName), new XAttribute("lastName", student.LastName));
                root.Add(studentElement);
                studentElement.Add(new XElement("BirthDate", GetBirthDate(student.Birthday)));
                studentElement.Add(new XElement("Email", student.Email));
                studentElement.Add(new XElement("PhoneNumber", student.Phone));
                XElement extraDataElement = new XElement("ExtraData");
                studentElement.Add(extraDataElement);
                foreach (var extraData in student.ExtraData)
                {
                    extraDataElement.Add(new XElement("ExtraDataElement", new XAttribute("name", extraData.Name), extraData.Value));
                }
                XElement coursesElement = new XElement("Courses");
                studentElement.Add(coursesElement);
                foreach (var course in student.Courses)
                {
                    coursesElement.Add(new XElement("Course", course));
                }
            }
            return document;
        }

        private static string GetBirthDate(DateTime studentBirthday)
        {
            return studentBirthday.ToString("dd.MM.yyy", CultureInfo.InvariantCulture);
        }

        private static void Import(List<Student> ImportStudents)
        {
            XDocument document = XDocument.Load("input_students.xml");
            var studentsElement = document.Elements().First();
            foreach (var student in studentsElement.Elements())
            {
                Student importStudent = new Student();
                importStudent.FirstName = student.Attribute("firstName").Value;
                importStudent.LastName = student.Attribute("lastName").Value;
                importStudent.Birthday = ImportStudentBirthday(student);
                importStudent.SetPhone(student.Element("PhoneNumber").Value);
                importStudent.Email = student.Element("Email").Value;
                ProcessExtraData(importStudent, student.Elements().SelectMany(p => p.Elements("ExtraDataElement")));
                ImportStudents.Add(importStudent);
                ImportCourses(importStudent, student.Elements().SelectMany(p => p.Elements("Course")));
            }
        }

        private static void ImportCourses(Student importStudent, IEnumerable<XElement> courseElements)
        {
            foreach (var courseElement in courseElements)
            {
                importStudent.Courses.Add(courseElement.Value);
            }
        }

        private static DateTime ImportStudentBirthday(XElement student)
        {
            string date = student.Element("BirthDate").Value;
            string[] splitDate = date.Split('.');
            int year = int.Parse(splitDate[2]);
            int month = int.Parse(splitDate[1]);
            int day = int.Parse(splitDate[0]);

            return new DateTime(year, month, day);
        }

        private static void ProcessExtraData(Student importStudent, IEnumerable<XElement> extraDataElements)
        {
            foreach (XElement extraDataElement in extraDataElements)
            {
                importStudent.ExtraData.Add(new ExtraData(extraDataElement.Attribute("name").Value, extraDataElement.Value));
            }
        }
    }
    [Serializable]
    public class Student
    {
        public Student()
        {
            ExtraData = new List<ExtraData>();
            Courses = new List<string>();
        }
        public List<ExtraData> ExtraData { get; }
        public List<string> Courses { get; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime Birthday { get; set; }

        public void SetPhone(string value)
        {
            this.Phone = value;
        }

        public string Phone { get; set; }

        public string Email { get; set; }
    }

    [Serializable]
    public class ExtraData
    {
        public ExtraData()
        {

        }
        public ExtraData(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}