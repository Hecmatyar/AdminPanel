using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{    
    /// <summary>
    /// пользователь с данными, которые хранятся в бд
    /// </summary>
    public class User
    {
        /// <summary>
        /// id пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя пользователя
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// пароль пользователя
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// соль для пароля
        /// </summary>
        public string UserSalt { get; set; }
        /// <summary>
        /// почтовый адрес пользователя
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// подтверждение почтового адреса
        /// </summary>
        public bool UserConfirmedEmail { get; set; }
        /// <summary>
        /// токен
        /// </summary>
        public string UserToken { get; set; }
        //public DateTime UserBirth { get; set; }
        /// <summary>
        /// аватар пользователя
        /// </summary>
        public byte[] UserPhoto { get; set; }
        /// <summary>
        /// список ролей пользователя
        /// </summary>
        public virtual List<Roles> UserRoles { get; set; }
        public User()
        {
            UserRoles = new List<Roles>();
        }
        /// <summary>
        /// приведение к классу UserModel
        /// </summary>
        /// <param name="v">User, который надо привести к UserModel</param>
        public static explicit operator UserModel(User v)
        {
            List<RolesModel> listB = v.UserRoles.Select(a => (RolesModel)a).ToList();
            return new UserModel
            {
                Id = v.Id,
                UserName = v.UserName,
                UserPassword = v.UserPassword,
                UserEmail = v.UserEmail,
                UserConfirmedEmail = v.UserConfirmedEmail,
                UserToken = v.UserToken,
                UserRoles = listB,
                UserPhoto = v.UserPhoto
            };
        }        
    }
}
