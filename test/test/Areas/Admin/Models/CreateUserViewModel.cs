using IService;
using IService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Areas.Admin.Models
{
    /// <summary>
    /// модель создания нового пользвоателя
    /// </summary>
    public class CreateUserViewModel
    {
        /// <summary>
        /// имя пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        /// <summary>
        /// пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Пароль")]
        public string UserPassword { get; set; }        
    }
}