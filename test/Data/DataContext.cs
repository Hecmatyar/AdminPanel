using Data.Models.Admin;
using Data.Models.Moderator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// посты в блоге
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        /// <summary>
        /// категории постов
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// тэги постов
        /// </summary>
        public DbSet<Tag> Tags { get; set; }
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
