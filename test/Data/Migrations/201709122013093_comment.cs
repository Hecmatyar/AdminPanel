namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {           
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Body = c.String(),
                        Published = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Comments", t => t.ParentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.ParentId)
                .Index(t => t.AuthorId)
                .Index(t => t.PostId);                
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolesUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Posts", "Id", "dbo.Comments");
            DropForeignKey("dbo.Users", "Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "ParentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "ParentId" });
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.Posts", new[] { "Id" });
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Posts", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Comments");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Posts", "Id");
            AddForeignKey("dbo.RolesUsers", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
