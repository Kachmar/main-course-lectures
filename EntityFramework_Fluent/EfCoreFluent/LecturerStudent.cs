namespace EfCoreFluent
{
    using System;

    public class LecturerStudent
    {
        public Guid LecturerId { get; set; }

        public int StudentId { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual Student Student { get; set; }
    }
}