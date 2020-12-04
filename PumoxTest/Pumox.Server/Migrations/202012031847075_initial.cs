namespace Pumox.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        JobTitle = c.String(nullable: false, maxLength: 50),
                        Enterprise_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enterprises", t => t.Enterprise_Id, cascadeDelete: true)
                .Index(t => t.Enterprise_Id);
            
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EstablishmentYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            Sql("ALTER TABLE dbo.Employees ADD CONSTRAINT CK_JobTitle CHECK (JobTitle IN('Administrator', 'Developer','Architect','Manager'))");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Enterprise_Id", "dbo.Enterprises");
            DropIndex("dbo.Employees", new[] { "Enterprise_Id" });
            DropTable("dbo.Enterprises");
            DropTable("dbo.Employees");
        }
    }
}
