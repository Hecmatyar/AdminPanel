using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// модель для письма сброса пароля пользователя
    /// </summary>
    public class LetterResetPasswordViewModel
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
        /// ссылка для перехода на форму сброса пароля
        /// </summary>
        public string UserActivationLink { get; set; }
    }
}