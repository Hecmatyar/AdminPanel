namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "ParentId" });
            AlterColumn("dbo.Comments", "ParentId", c => c.Int());
            CreateIndex("dbo.Comments", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "ParentId" });
            AlterColumn("dbo.Comments", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ParentId");
        }
    }
}
