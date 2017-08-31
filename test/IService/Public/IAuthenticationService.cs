using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    /// <summary>
    /// работа с пользователем
    /// регистрация, смена пароля, сброс пароля по почте
    /// подтсверждение пароля пользователя по почте
    /// </summary>
    public interface IAuthenticationService
    {   
        /// <summary>
        /// существует ли указанный пользователь в бд
        /// </summary>
        /// <param name="userName">логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns></returns>
        bool UserIsExist(string userName);    
        
        /// <summary>
        /// подтвердил ли пользователь свой почтовый адрес и завершил регистрацию
        /// </summary>
        /// <param name="token">токен</param>
        /// <param name="email">почтовый адрес пользователя</param>
        /// <returns></returns>
        bool UserIsConfirmedEmail(string token, string email);

        /// <summary>
        /// подтвержение пользователем своего почтового адреса
        /// </summary>
        /// <param name="token">токен</param>
        /// <param name="email">почтовый адрес</param>
        void UserConfirmedEmail(string token, string email);

        /// <summary>
        /// получение данных пользователя по токену
        /// </summary>
        /// <param name="token">токен</param>
        /// <returns></returns>
        UserModel GetUserByToken(string token);

        /// <summary>
        /// получение данных пользователя по его почтовому адресу
        /// нужно для сброса пароля
        /// </summary>
        /// <param name="email">почтовый адрес</param>
        /// <returns></returns>
        UserModel GetUserByEmail(string email);

        /// <summary>
        /// получение данных пользователя по его логину и паролю
        /// нужно для входа через соответсвующую форму
        /// </summary>
        /// <param name="userName">логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns></returns>
        UserModel GetUserByLP(string userName, string password);

        /// <summary>
        /// получение данных пользователя после его аутентификации в системе
        /// </summary>
        /// <param name="id">id пользователя в бд</param>
        /// <returns></returns>
        UserModel GetUserById(int id);

        /// <summary>
        /// регистрация нового пользователя
        /// почтовый адрес не подтвержден
        /// </summary>
        /// <param name="userName">логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <param name="email">почтовый адрес пользователя</param>
        void RegisterUser(string userName, string password, string email, byte[] photo, DateTime birth);

        /// <summary>
        /// смена пароля пользователя в личном кабинете
        /// </summary>
        /// <param name="userName">логин пользователя</param>
        /// <param name="oldPassword">старый пароль пользователя</param>
        /// <param name="newPassword">новый пароль пользователя</param>
        /// <param name="token">токен</param>
        void ChangePassword(string userName, string oldPassword, string newPassword, string token);

        /// <summary>
        /// сброс пароля пользователя
        /// </summary>
        /// <param name="token">токен пользователя</param>
        /// <param name="newPassword">новый пароль для пользователя</param>
        void ResetPassword(string token, string newPassword);
    }
}
