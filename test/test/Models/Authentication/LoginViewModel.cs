using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// модель логина пользователя
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// имя пользователя
        /// </summary>       
        public string UserName { get; set; }
        /// <summary>
        /// пароль пользователя
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// запонить вход
        /// </summary>
        public bool RememberMe { get; set; }
    }
}