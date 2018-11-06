namespace EntityFramework_Fluent
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    public class UniversityContext : DbContext
    {
        public UniversityContext() : base("DemoConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<UniversityContext>());
            Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("FluentDemo");

            SetPrimaryKeyAndCompositeKey(modelBuilder);

            SetGuidGenerationOnColumn(modelBuilder);

            SetColumnSpecificType(modelBuilder);

            SetOneToOneRelation(modelBuilder);

            //SetTableNameToSomethingDifferentThanEntityName(modelBuilder);

            //SetColumnNameToSomethingDifferentThanOriginalName(modelBuilder);

            // SetOneEntityTo2Tables(modelBuilder);

            SetTablePerDerivedClass(modelBuilder);
        }

        private void SetTablePerDerivedClass(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyClassA>().ToTable("ClassA");
            modelBuilder.Entity<MyClassB>().ToTable("ClassB");
        }

        private void SetOneEntityTo2Tables(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .Map(map =>
                    {
                        map.Properties(p => new
                        {
                            p.Name
                        });
                        map.ToTable("Exam");
                    })
                // Map to the Users table  
                .Map(map =>
                    {
                        map.Properties(p => new
                        {
                            p.Duration,
                            p.Location
                        });
                        map.ToTable("examDetails");
                    });
        }

        private void SetColumnNameToSomethingDifferentThanOriginalName(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().Property(p => p.Name).HasColumnName("ExamName");
        }

        private void SetTableNameToSomethingDifferentThanEntityName(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().ToTable("ExamInfo");
        }

        private static void SetPrimaryKeyAndCompositeKey(DbModelBuilder modelBuilder)
        {
            //Same a using [Key]
            modelBuilder.Entity<Student>().HasKey(student => student.Id);
            //Composite key
            //modelBuilder.Entity<Student>().HasKey(
            //    key => new
            //    {
            //        key.Name,
            //        key.BirthDate
            //    });
        }

        private static void SetGuidGenerationOnColumn(DbModelBuilder modelBuilder)
        {
            //Since we changed Student.Id to Guid we need to specify that is should be 
            //somehow generated
            modelBuilder.Entity<Student>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private static void SetColumnSpecificType(DbModelBuilder modelBuilder)
        {
            //This would map to not NVarchar but Varchar(100)
            modelBuilder.Entity<Student>().Property(p => p.Name).IsUnicode(false).HasMaxLength(100);
        }

        private static void SetOneToOneRelation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAddress>().HasKey(p => p.StudentId);
            modelBuilder.Entity<Student>().HasOptional(student => student.StudentAddress).WithRequired(addr => addr.Student);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<StudentAddress> StudentAddresses { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<MyBaseClass> MyBaseClasses { get; set; }
    }
}
