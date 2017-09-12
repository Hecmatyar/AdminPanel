using IService.Models.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IService.Models
{     
    /// <summary>
    /// информация о пользователе
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// id пользователя
        /// </summary>
        public int Id { get; set; }       
        /// <summary>
        /// имя пользователя
        /// </summary>
        [DisplayName("Логин")]
        public string UserName { get; set; }
        /// <summary>
        /// пароль пользователя
        /// </summary>
        [DisplayName("Пароль")]
        public string UserPassword { get; set; }
        /// <summary>
        /// соль для пароля
        /// </summary>
        public string UserSalt { get; set; }
        /// <summary>
        /// почтовый адрес пользователя
        /// </summary>
        [DisplayName("EMail")]
        public string UserEmail { get; set; }
        /// <summary>
        /// подтвержение почтового адреса
        /// </summary>
        public bool UserConfirmedEmail { get; set; }
        /// <summary>
        /// токен
        /// </summary>
        public string UserToken { get; set; }
        /// <summary>
        /// дата рождения пользователя
        /// </summary>
        [DisplayName("Дата рождения")]
        public DateTime UserBirth { get; set; }
        /// <summary>
        /// аватар пользователя
        /// </summary>
        [DisplayName("Аватар")]
        public byte[] UserPhoto { get; set; }
        /// <summary>
        /// список ролей пользователя
        /// </summary>
        public List<RolesModel> UserRoles { get; set; }
        /// <summary>
        /// комментарии пользователя
        /// </summary>
        //public List<CommentModel> Comments { get; set; }
        /// <summary>
        /// проверяет доступ пользователя
        /// </summary>
        /// <param name="accessTole">требуемая роль</param>
        /// <returns>есть ли доступ у пользователя</returns>
        public bool IsInRole(RolesEnum accessTole)
        {
            return UserRoles.Any(a => a.Name == accessTole ) ? true : false;            
        }
    }
}