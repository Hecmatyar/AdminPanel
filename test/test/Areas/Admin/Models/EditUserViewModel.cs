using IService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Areas.Admin.Models
{
    /// <summary>
    /// модель редактирования пользователя
    /// </summary>
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            File = new List<HttpPostedFileBase>();
        }
        public List<HttpPostedFileBase> File { get; set; }

        /// <summary>
        /// id пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// токен пользователя для идентификации
        /// </summary>
        public string UserToken { get; set; }
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
        /// <summary>
        /// новый пароль пользователя
        /// </summary>
        [Display(Name = "Пароль")]
        public string NewUserPassword { get; set; }
        /// <summary>
        /// почтовый адрес пользователя
        /// </summary>
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Display(Name = "EMail")]
        public string UserEmail { get; set; }
        /// <summary>
        /// список ролей пользователя
        /// </summary>
        public List<RolesModel> UserRoles { get; set; }
        /// <summary>
        /// аватар пользователя
        /// </summary>
        [Display(Name = "Аватар")]
        public byte[] UserPhoto { get; set; }
        /// <summary>
        /// новый аватар пользователя
        /// </summary>
        [Display(Name = "Аватар")]
        public byte[] NewUserPhoto { get; set; }
        /// <summary>
        /// роли пользователя
        /// </summary>
        public int[] SelectedRole { get; set; }
    }
}