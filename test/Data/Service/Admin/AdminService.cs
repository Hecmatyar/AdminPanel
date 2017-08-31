using Data.Models;
using IService;
using IService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// выполнение функция администрирования пользователей ресурса
    /// </summary>
    public class AdminService : IAdminService
    {
        private UserContext db = new UserContext();
        AuthenticationService auth = new AuthenticationService();

        /// <summary>
        /// список всех зарегестрированных пользователей
        /// </summary>
        /// <returns>список пользователей</returns>
        public List<UserModel> GetUserList()
        {
            return db.Users.ToList().Select(_ => (UserModel)_).ToList();
        }

        /// <summary>
        /// удаление пользователя из бд
        /// </summary>
        /// <param name="token">токен</param>
        public void DeleteUser(string token)
        {
            User user = db.Users.First(_ => _.UserToken == token);
            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();
        }

        /// <summary>
        /// регистрация нового пользователя с уже подтвержденным почтовым адресом и заданной ролью
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="userRoles">выбранная роль для нового пользователя</param>
        /// <param name="photo">аватар пользователя</param>
        public void RegisterUser(string userName, string password, string email, RolesEnum[] userRoles, byte[] photo)
        {
            if (!db.Users.Any(_ => _.UserName.Equals(userName) && _.UserEmail.Equals(email)))
            {
                //генерация токена
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string UserToken = Convert.ToBase64String(time.Concat(key).ToArray());

                List<Roles> roles = new List<Roles>();
                for (int i = 0; i < userRoles.Length; i++)
                {
                    RolesEnum currenRole = userRoles[i];
                    roles.Add(db.RolesEnums.FirstOrDefault(a => a.Name == currenRole));
                }

                User user = new User
                {
                    UserName = userName,
                    UserPassword = password,
                    UserEmail = email,
                    UserConfirmedEmail = true,
                    UserToken = UserToken,
                    UserPhoto = photo,
                    UserRoles = roles
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// изменение личной информации пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="email">почта пользователя</param>
        /// <param name="birtday">даат рождения пользователя</param>
        /// <param name="photo">аватар пользователя</param>
        public void ChangeUserInfo(int id, string email, DateTime birtday, byte[] photo)
        {
            User user = db.Users.First(_ => _.Id == id);
            user.UserEmail = email;
            if (photo.Length > 0)
                user.UserPhoto = photo;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// сброс пароля пользователя на новый
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="newPassword">новый пароль пользователя</param>
        public void ChangeUserPassword(int id, string newPassword)
        {
            User user = db.Users.First(_ => _.Id == id);
            string newpass = auth.GeneratePassword(newPassword, user.UserSalt);
            user.UserPassword = newpass;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// изменение ролей пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="roles">список новых ролей пользователя</param>
        public void ChangeUserRoles(int id, RolesEnum[] roles)
        {
            User user = db.Users.First(_ => _.Id == id);
            List<Roles> userRoles = db.RolesEnums.Where(a => roles.Contains(a.Name)).ToList();
            user.UserRoles.Clear();
            user.UserRoles = userRoles;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
