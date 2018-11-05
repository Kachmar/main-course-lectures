using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfBasics.EF
{
   public class Exam
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //virtual - to support lazy loading
        //public Student Student { get; set; }
        public virtual Student Student { get; set; }

    }
}
