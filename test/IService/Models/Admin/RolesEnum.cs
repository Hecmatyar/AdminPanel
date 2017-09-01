using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models
{
    /// <summary>
    /// роли пользователей в системе
    /// </summary>
    public enum RolesEnum
    {
        /// <summary>
        /// зарегестрированный пользователь
        /// </summary>
        Authorized = 0,
        /// <summary>
        /// админ системы
        /// </summary>
        Admin = 1,
        /// <summary>
        /// менеджер 
        /// </summary>
        Moderator = 2,
        /// <summary>
        /// клиент
        /// </summary>
        Client = 3
    }
}
