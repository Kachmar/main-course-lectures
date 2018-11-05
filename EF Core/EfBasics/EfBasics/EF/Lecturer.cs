using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfBasics.EF
{
    public class Lecturer
    {
        public int id { get; set; }

        public string Name { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}
