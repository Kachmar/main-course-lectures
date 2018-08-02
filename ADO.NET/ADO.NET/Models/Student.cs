using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Models
{
  public  class Student
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public Group Group { get; set; }

        public List<Course> Courses { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string GitHubLink { get; set; }

        public string Notes { get; set; }
    }
}