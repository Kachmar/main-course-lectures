namespace EntityFramework_Fluent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("FluentDemo.Exams", "Duration", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("FluentDemo.Exams", "Duration", c => c.Int(nullable: false));
        }
    }
}
