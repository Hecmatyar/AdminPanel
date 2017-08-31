namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserSalt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserSalt");
        }
    }
}
