using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// модель установки нового пароля для пользователя
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// новый пароль пользователя
        /// </summary>
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
        /// <summary>
        /// подтверждение пароля
        /// </summary>
        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// токен
        /// </summary>
        public string Token { get; set; }
    }
}