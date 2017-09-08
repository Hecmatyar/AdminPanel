using Data.Models;
using Data.Models.Admin;
using IService.Admin;
using IService.Models;
using IService.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// выполнение функция администрирования пользователей ресурса
    /// </summary>
    public class AdminService : IAdminService
    {
        /// <summary>
        /// постраничный список всех зарегестрированных пользователей
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество элементов на странице</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <returns></returns>
        public List<UserModel> GetUserList(string search, int pageSize, int pageIndex)
        {
            using (var db = new DataContext())
            {
                return db.Users
                .Where(_ => string.IsNullOrEmpty(search) ? true : _.UserName.Contains(search))
                .ToList().Select(_ => (UserModel)_)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// количество страниц в таблице
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество элемнтов на странице</param>
        /// <returns></returns>
        public int GetPageCount(string search, int pageSize)
        {
            using (var db = new DataContext())
            {
                return (int)Math.Ceiling(
                db.Users.Where(_ => string.IsNullOrEmpty(search) ? true : _.UserName.Contains(search))
                .Count() / (double)pageSize);
            }
        }

        /// <summary>
        /// удаление пользователя из бд
        /// </summary>
        /// <param name="token">токен</param>
        public void DeleteUser(int id)
        {
            using (var db = new DataContext())
            {
                User user = db.Users.First(_ => _.Id == id);
                db.Entry(user).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// регистрация нового пользователя с уже подтвержденным почтовым адресом и заданной ролью
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="userRoles">выбранная роль для нового пользователя</param>
        /// <param name="photo">аватар пользователя</param>
        public void RegisterUser(string userName, string password)
        {
            using (var db = new DataContext())
            {
                if (!db.Users.Any(_ => _.UserName.Equals(userName)))
                {
                    byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                    byte[] key = Guid.NewGuid().ToByteArray();
                    string token = Convert.ToBase64String(time.Concat(key).ToArray());
                    var role = new List<Models.Admin.Roles>
                {
                    db.RolesEnums.FirstOrDefault(a => a.Name == RolesEnum.Authorized)
                };
                    string salt = GetStringFromHash(GetSalt());
                    password = GeneratePassword(password, salt);
                    User user = new User
                    {
                        UserName = userName,
                        UserPassword = password,
                        UserToken = token,
                        UserSalt = salt,
                        UserRoles = role,
                        UserConfirmedEmail = true
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
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
            using (var db = new DataContext())
            {
                User user = db.Users.First(_ => _.Id == id);
                user.UserEmail = email;
                user.UserConfirmedEmail = true;
                if (photo != null)
                    user.UserPhoto = photo;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// сброс пароля пользователя на новый
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="newPassword">новый пароль пользователя</param>
        public void ChangeUserPassword(int id, string newPassword)
        {
            using (var db = new DataContext())
            {
                User user = db.Users.First(_ => _.Id == id);
                string newpass = GeneratePassword(newPassword, user.UserSalt);
                user.UserPassword = newpass;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// изменение ролей пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="roles">список новых ролей пользователя</param>
        public void ChangeUserRoles(int id, RolesEnum[] roles)
        {
            using (var db = new DataContext())
            {
                User user = db.Users.First(_ => _.Id == id);
                List<Roles> userRoles = db.RolesEnums.Where(a => roles.Contains(a.Name)).ToList();
                user.UserRoles.Clear();
                user.UserRoles = userRoles;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// получение пользователя по его Id
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>пользователь с соответствующим id</returns>
        public UserModel GetUserById(int id)
        {
            using (var db = new DataContext())
            {
                return (UserModel)db.Users.First(_ => _.Id == id);
            }
        }

        /// <summary>
        /// получение данных пользователя для смены пароля
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модель с данными для смены пароля</returns>
        public UserPasswordModel GetUserPasswordById(int id)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.First(_ => _.Id == id);
                var userpass = new UserPasswordModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserPassword = user.UserPassword,
                    NewUserPassword = ""
                };
                return userpass;
            }
        }

        /// <summary>
        /// получение данных пользователя для смены ролей
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модель с данными для смены ролей</returns>
        public UserRolesModel GetUserRolesById(int id)
        {
            using (var db = new DataContext())
            {
                var ur = Enum.GetValues(typeof(RolesEnum)).Cast<RolesEnum>().ToList();
                var user = db.Users.First(_ => _.Id == id);
                var userpass = new UserRolesModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRoles = user.UserRoles.Select(a => (RolesModel)a).ToList(),
                    UnUserRoles = ur.Where(a => !user.UserRoles.Any(r => r.Name == a)).ToList()
                };
                return userpass;
            }
        }

        /// <summary>
        /// получение личных данных пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модель с данными для их редактирования</returns>
        public UserInfoModel GetUserInfoById(int id)
        {
            using (var db = new DataContext())
            {               
                var user = db.Users.First(_ => _.Id == id);
                var userpass = new UserInfoModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserPhoto = user.UserPhoto,
                    NewUserPhoto = null
                };
                return userpass;
            }
        }

        /// <summary>
        /// криптографический генератор случайных чисел
        /// </summary>
        private static int saltLengthLimit = 32;
        private byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        /// <summary>
        /// генерация пароля для пользователя
        /// </summary>
        /// <param name="pass">Original password</param>
        /// <param name="salt">User ID + " " + User.ID</param>       
        /// <returns></returns>
        private string GeneratePassword(string pass, string salt)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(pass + salt);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        /// <summary>
        /// получение строки из хеша
        /// </summary>
        /// <param name="hash">хеш</param>
        /// <returns>строка хеша</returns>
        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
