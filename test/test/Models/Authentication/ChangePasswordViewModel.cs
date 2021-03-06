﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// смена пароля пользователя
    /// </summary>
    public class ChangePasswordViewModel
    {
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }

        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}