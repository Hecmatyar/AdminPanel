using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// модель сброса пароля пользователя
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// адрес электронной почты для сброса пароля
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }   
}