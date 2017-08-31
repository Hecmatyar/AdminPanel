namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Models.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.Models.UserContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.RolesEnums.AddOrUpdate(             
            //  new Models.RolesEnum { Name = IService.UserRoles.Admin },
            //  new Models.RolesEnum { Name = IService.UserRoles.Client },
            //  new Models.RolesEnum { Name = IService.UserRoles.Manager },
            //  new Models.RolesEnum { Name = IService.UserRoles.None }
            //);

        }
    }
}
