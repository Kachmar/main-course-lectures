namespace EfBasicsCore.EF
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;

    public class UniversityContext : DbContext
    {
    
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
                builder.AddConsole();
            }
        );

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)
                
                .UseSqlServer(@"Data Source=NB00MCF001\SQLEXPRESS;Initial Catalog=EntityFrameworkBasics;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Many to Many

            modelBuilder.Entity<LecturerStudent>().HasKey(k => new { k.LecturerId, k.StudentId });

            //Data Seeding
            //modelBuilder.Entity<Student>().HasData(new Student()
            //{
            //    BirthDate = new DateTime(2008, 12, 12),
            //    StudentId = 1,
            //    Name = "John",
            //    Speciality = "Math"
            //});
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<StudentAddress> StudentAddresses { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public override void Dispose()
        {
            //var deleted = Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
