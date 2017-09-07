using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Data.Models;
using System.Data.Entity;
using System.Web.Security;
using System.Web;
using IService.Models;
using System.Security.Cryptography;
using Data.Models.Admin;

namespace Data
{
    /// <summary>
    /// аутентификация пользователя
    /// регистрация, сброс пароля, смена пароля, получение личных данных
    /// </summary>   

    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// существует ли пользователь в базе данных
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns>существует ли такой пользователь в системе</returns>
        public bool UserIsExist(string login)
        {
            using (var db = new DataContext())
            {
                return db.Users.Any(_ => _.UserName == login);
            }
        }

        /// <summary>
        /// подтвердил ли пользователь свой почтовый адрес
        /// </summary>
        /// <param name="token">токен</param>
        /// <param name="email">почтовый адрес</param>
        /// <returns>подтвердил ли пользователь свой почтовый адрес</returns>
        public bool UserIsConfirmedEmail(string token, string email)
        {
            using (var db = new DataContext())
            {
                return db.Users.Any(_ => _.UserConfirmedEmail == true && _.UserEmail == email && _.UserToken == token);
            }
        }

        /// <summary>
        /// подтверждение адреса почты
        /// </summary>
        /// <param name="token">токен</param>
        /// <param name="email">почтовый адрес</param>
        public void UserConfirmedEmail(string token, string email)
        {
            using (var db = new DataContext())
            {
                User user = db.Users.First(_ => _.UserToken == token && _.UserEmail == email);
                user.UserConfirmedEmail = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// получение пользователя по токену
        /// </summary>
        /// <param name="token">токен</param>
        /// <returns></returns>
        public UserModel GetUserByToken(string token)
        {
            using (var db = new DataContext())
            {
                return (UserModel)db.Users.First(_ => _.UserToken == token);
            }
        }

        /// <summary>
        /// получение пользователя по почте, для сброса пароля
        /// </summary>
        /// <param name="email">почтовый адрес</param>
        /// <returns>пользователь с данным почтовым адресом</returns>
        public UserModel GetUserByEmail(string email)
        {
            using (var db = new DataContext())
            {
                return (UserModel)db.Users.First(_ => _.UserEmail == email);
            }
        }

        /// <summary>
        /// получение пользователя при логине в системе
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns>пользователь с данным логином и паролем</returns>
        public UserModel GetUserByLP(string login, string password)
        {
            using (var db = new DataContext())
            {
                User user = db.Users.FirstOrDefault(_ => _.UserName == login);
                string userPassword = GeneratePassword(password, user.UserSalt);
                UserModel um = (UserModel)db.Users.FirstOrDefault(_ => _.UserName == login && _.UserPassword == userPassword);
                return um;
            }
        }

        /// <summary>
        /// получение пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>пользователь с данным id</returns>
        public UserModel GetUserById(int id)
        {
            using (var db = new DataContext())
            {
                return (UserModel)db.Users.First(_ => _.Id == id);
            }
        }

        /// <summary>
        /// регистрация пользователя в системе
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <param name="email">почтовый адрес пользователя</param>
        /// <param name="photo">ааватар пользователя</param>
        /// <param name="birth">дата рождения пользователя</param>
        public void RegisterUser(string userName, string password, string email, byte[] photo, DateTime birth)
        {
            using (var db = new DataContext())
            {
                if (!db.Users.Any(_ => _.UserName.Equals(userName) && _.UserEmail.Equals(email)))
                {
                    //генерация токена
                    byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                    byte[] key = Guid.NewGuid().ToByteArray();
                    string token = Convert.ToBase64String(time.Concat(key).ToArray());

                    List<Models.Admin.Roles> role = new List<Models.Admin.Roles>
                    {
                        db.RolesEnums.FirstOrDefault(a => a.Name == RolesEnum.Authorized)
                    };

                    string salt = GetStringFromHash(GetSalt());
                    password = GeneratePassword(password, salt);

                    User user = new User
                    {
                        UserName = userName,
                        UserPassword = password,
                        UserEmail = email,
                        UserConfirmedEmail = false,
                        UserToken = token,
                        UserRoles = role,
                        UserSalt = salt,
                        //UserBirth = UserBirth,
                        UserPhoto = photo
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// смена пароля пользователя в системе
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <param name="oldPassword">старый пароль пользователя</param>
        /// <param name="newPassword">новый пароль поьзователя</param>
        /// <param name="token">токен</param>
        public void ChangePassword(string userName, string oldPassword, string newPassword, string token)
        {
            using (var db = new DataContext())
            {
                if (UserIsExist(userName))
                {
                    User user = db.Users.First(_ => _.UserToken == token);
                    user.UserPassword = GeneratePassword(newPassword, user.UserSalt);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// сброс пароля пользователя
        /// </summary>
        /// <param name="token">токен</param>
        /// <param name="newPassword">новый пароль пользователя</param>
        public void ResetPassword(string token, string newPassword)
        {
            using (var db = new DataContext())
            {
                User user = db.Users.First(_ => _.UserToken == token);
                user.UserPassword = GeneratePassword(newPassword, user.UserSalt);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
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
