namespace EfBasicsCore.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LecturerStudent
    {
        public int LecturerId { get; set; }

        public int StudentId { get; set; }

        public Lecturer Lecturer { get; set; }

        public Student Student { get; set; }
    }
}