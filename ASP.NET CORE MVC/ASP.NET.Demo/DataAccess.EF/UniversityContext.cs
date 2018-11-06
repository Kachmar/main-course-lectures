namespace DataAccess.EF
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using Models.Models;

    public class UniversityContext : DbContext
    {


        public UniversityContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<HomeTask> HomeTasks { get; set; }
    }
}
