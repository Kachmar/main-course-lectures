using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_Fluent
{
    public abstract class MyBaseClass
    {
        public int Id { get; set; }
        public string PropertyBase { get; set; }
    }

    class MyClassA : MyBaseClass
    {
        public string PropertyA { get; set; }
    }

    class MyClassB : MyBaseClass
    {
        public string PropertyB { get; set; }
    }
}
