namespace EfCoreFluent
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;

    public class UniversityContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FluentCourseDemo;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SetManyToManyRelation(modelBuilder);
            modelBuilder.HasDefaultSchema("FluentDemo");

            SetOneTableFor2Entities(modelBuilder);


            SetColumnSpecificType(modelBuilder);

            SetOneToOneRelation(modelBuilder);



            SetTableNameToSomethingDifferentThanEntityName(modelBuilder);

            SetColumnNameToSomethingDifferentThanOriginalName(modelBuilder);

            this.SetInheritance(modelBuilder);
        }

        private void SetOneTableFor2Entities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<StudentAddress>().ToTable("Student");
        }

        private void SetManyToManyRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LecturerStudent>().HasKey(k => new { k.LecturerId, k.StudentId });
        }

        private void SetInheritance(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyClassA>().HasBaseType<MyBaseClass>();
            modelBuilder.Entity<MyClassB>().HasBaseType<MyBaseClass>();
        }

        private void SetColumnNameToSomethingDifferentThanOriginalName(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().Property(p => p.Name).HasColumnName("ExamSpecialColumn");
        }

        private void SetTableNameToSomethingDifferentThanEntityName(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().ToTable("ExamInfo");
        }

        private static void SetColumnSpecificType(ModelBuilder modelBuilder)
        {
            //This would map to not NVarchar but Varchar(100)
            modelBuilder.Entity<Student>().Property(p => p.Name).IsUnicode(false).HasMaxLength(100);
        }

        private static void SetOneToOneRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAddress>().HasKey(p => p.StudentId);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Exam> Exams { get; set; }

        //public DbSet<StudentAddress> StudentAddresses { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<MyBaseClass> MyBaseClasses { get; set; }
    }
}
