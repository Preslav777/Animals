namespace AnimalsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Animals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Age = c.Single(nullable: false),
                        BreedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Breeds", t => t.BreedId, cascadeDelete: true)
                .Index(t => t.BreedId);
            
            CreateTable(
                "dbo.Breeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "BreedId", "dbo.Breeds");
            DropIndex("dbo.Animals", new[] { "BreedId" });
            DropTable("dbo.Breeds");
            DropTable("dbo.Animals");
        }
    }
}
