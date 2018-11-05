namespace EfBasics.EF
{
    using System.Data.Entity;

    public class UniversityContext : DbContext
    {
        public UniversityContext() : base("DemoConnection")
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<StudentAddress> StudentAddresses { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }
    }
}
