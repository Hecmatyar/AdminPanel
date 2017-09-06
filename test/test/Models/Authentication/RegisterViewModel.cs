using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// модель регистрации пользователя
    /// МОЖНО ПЕРЕДЕЛАТЬ
    /// </summary>
    public class RegisterViewModel
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
        /// почтовый адрес пользователя
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// запонить вход
        /// </summary>
        public bool RememberMe { get; set; }
        /// <summary>
        /// дата рождения 
        /// </summary>
        [DisplayName("Дата рождения")]
        public DateTime UserBirth { get; set; }
        /// <summary>
        /// картинка аватар пользователя
        /// </summary>
        [DisplayName("Аватар")]
        public byte[] UserPhoto { get; set; }
    }
}