using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// письмо подтверждения почтового адреса
    /// </summary>
    public class LetterConfirmViewModel
    {
        /// <summary>
        /// имя пользователя
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// почтовый адрес пользователя
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// ссылка для активации аккаунта и завершения регистрации
        /// </summary>
        public string UserActivationLink { get; set; }
    }
}