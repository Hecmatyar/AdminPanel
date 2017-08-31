using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IService;

namespace Data.Models
{
    public class UserContext : DbContext
    {      
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> RolesEnums { get; set; }
    }
}