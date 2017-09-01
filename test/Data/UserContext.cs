using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IService;
using Data.Models.Admin;

namespace Data.Models
{
    /// <summary>
    /// контекст пользователей
    /// </summary>
    public class UserContext : DbContext
    {      
        /// <summary>
        /// пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// роли пользователей
        /// </summary>
        public DbSet<Roles> RolesEnums { get; set; }
    }
}