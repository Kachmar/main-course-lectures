namespace EntityFramework_Fluent
{
    using System.Collections.Generic;

    public class Lecturer
    {
        public int id { get; set; }

        public string Name { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}
