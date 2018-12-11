namespace EfBasicsCore.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Lecturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<LecturerStudent> LecturerStudents { get; set; }
    }
}