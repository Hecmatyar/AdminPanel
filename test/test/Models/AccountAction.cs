using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class LoginViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public bool RememberMe { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }
    public class SetPasswordViewModel
    {
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
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
    public class LetterConfirmViewModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserActivationLink { get; set; }
    }
}