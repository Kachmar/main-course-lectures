namespace EntityFramework_Fluent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FluentDemo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Duration = c.Double(nullable: false),
                        Location = c.String(),
                        Student_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("FluentDemo.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "FluentDemo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        BirthDate = c.DateTime(nullable: false),
                        Speciality = c.String(),
                        DistantLocation = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FluentDemo.Lecturers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "FluentDemo.StudentAddresses",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        AddressLine = c.String(),
                        Student_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("FluentDemo.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "FluentDemo.MyBaseClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyBase = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FluentDemo.LecturerStudents",
                c => new
                    {
                        Lecturer_id = c.Int(nullable: false),
                        Student_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Lecturer_id, t.Student_Id })
                .ForeignKey("FluentDemo.Lecturers", t => t.Lecturer_id, cascadeDelete: true)
                .ForeignKey("FluentDemo.Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.Lecturer_id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "FluentDemo.ClassA",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PropertyA = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("FluentDemo.MyBaseClasses", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "FluentDemo.ClassB",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PropertyB = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("FluentDemo.MyBaseClasses", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("FluentDemo.ClassB", "Id", "FluentDemo.MyBaseClasses");
            DropForeignKey("FluentDemo.ClassA", "Id", "FluentDemo.MyBaseClasses");
            DropForeignKey("FluentDemo.StudentAddresses", "Student_Id", "FluentDemo.Students");
            DropForeignKey("FluentDemo.LecturerStudents", "Student_Id", "FluentDemo.Students");
            DropForeignKey("FluentDemo.LecturerStudents", "Lecturer_id", "FluentDemo.Lecturers");
            DropForeignKey("FluentDemo.Exams", "Student_Id", "FluentDemo.Students");
            DropIndex("FluentDemo.ClassB", new[] { "Id" });
            DropIndex("FluentDemo.ClassA", new[] { "Id" });
            DropIndex("FluentDemo.LecturerStudents", new[] { "Student_Id" });
            DropIndex("FluentDemo.LecturerStudents", new[] { "Lecturer_id" });
            DropIndex("FluentDemo.StudentAddresses", new[] { "Student_Id" });
            DropIndex("FluentDemo.Exams", new[] { "Student_Id" });
            DropTable("FluentDemo.ClassB");
            DropTable("FluentDemo.ClassA");
            DropTable("FluentDemo.LecturerStudents");
            DropTable("FluentDemo.MyBaseClasses");
            DropTable("FluentDemo.StudentAddresses");
            DropTable("FluentDemo.Lecturers");
            DropTable("FluentDemo.Students");
            DropTable("FluentDemo.Exams");
        }
    }
}
