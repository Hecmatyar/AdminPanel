namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class url : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "UrlName", c => c.String());
            AddColumn("dbo.Posts", "UrlTitle", c => c.String());
            AddColumn("dbo.Tags", "UrlName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "UrlName");
            DropColumn("dbo.Posts", "UrlTitle");
            DropColumn("dbo.Categories", "UrlName");
        }
    }
}
