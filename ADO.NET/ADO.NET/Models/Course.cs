using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Models
{
   public class Course
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PassCredits { get; set; }

        public int HomeTasksCount { get; set; }
    }
}
