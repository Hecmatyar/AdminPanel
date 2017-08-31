using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{    
    /// <summary>
    /// модель ролей пользователя
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// id роли в бд
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя роли
        /// </summary>
        public RolesEnum Name { get; set; }
        /// <summary>
        /// список пользователей с данной ролью
        /// </summary>
        public virtual List<User> UserRole { get; set; }
        public Roles()
        {
            UserRole = new List<User>();
        }

        /// <summary>
        /// преобразование к классу RolesModel
        /// </summary>
        /// <param name="v">Roles, который надо привести к RolesModel</param>
        public static explicit operator RolesModel(Roles v)
        {
            return new RolesModel
            {
                Id = v.Id,
                Name = v.Name
            };
        }        
    }
}
