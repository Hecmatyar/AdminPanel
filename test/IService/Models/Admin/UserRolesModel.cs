using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models.Admin
{
    public class UserRolesModel
    {   
        /// <summary>
        /// id пользователя
        /// </summary>
        public int UserId { get; set; }        
        /// <summary>
        /// имя пользователя
        /// </summary>       
        public string UserName { get; set; }  
        /// <summary>
        /// список ролей пользователя
        /// </summary>
        public List<RolesModel> UserRoles { get; set; }
        /// <summary>
        /// список ролей пользователя
        /// </summary>
        public List<RolesEnum> UnUserRoles { get; set; }
        /// <summary>
        /// выбранные роли
        /// </summary>
        public int[] SelectedRole { get; set; }
    }
}
