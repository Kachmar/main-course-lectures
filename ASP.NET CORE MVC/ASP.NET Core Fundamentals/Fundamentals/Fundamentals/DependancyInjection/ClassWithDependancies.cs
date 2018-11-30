using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals.DependancyInjection
{
    public class ClassWithDependancies
    {
        private readonly ClassDependingOnInterface classDependingOnInterface;

        public ClassWithDependancies(ClassDependingOnInterface classDependingOnInterface)
        {
            this.classDependingOnInterface = classDependingOnInterface;
        }
    }
}
