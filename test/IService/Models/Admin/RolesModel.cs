using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models
{
    /// <summary>
    /// модель ролей для пользователя
    /// 
    /// </summary>
    public class RolesModel
    {
        /// <summary>
        /// id роли
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя роли
        /// </summary>
        public RolesEnum Name { get; set; }
    }
}
