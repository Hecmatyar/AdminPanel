using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models.Admin
{
    public class UserPasswordModel
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
        /// пароль пользователя
        /// </summary>       
        public string UserPassword { get; set; }
        /// <summary>
        /// новый пароль пользователя
        /// </summary>       
        public string NewUserPassword { get; set; }       
    }
}
