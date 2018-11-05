namespace DataAccess.EF
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using Models.Models;

    public class UniversityContext : DbContext
    {
        public string ConnectionString { get; set; }

        public UniversityContext(IOptions<RepositoryOptions> options) : base(options.Value.DefaultConnectionString)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<HomeTask> HomeTasks { get; set; }
    }
}
