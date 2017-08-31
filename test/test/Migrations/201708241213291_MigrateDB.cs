namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDatas", "UserConfirmedEmail", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserDatas", "UserEmail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDatas", "UserEmail", c => c.String());
            DropColumn("dbo.UserDatas", "UserConfirmedEmail");
        }
    }
}
