﻿using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    /// <summary>
    /// интерфейс для методов администрирования пользователей ресурса
    /// добавление новых пользователей, редактирование и удаление имеющихся пользователей
    /// получение списка имеющихся пользователей
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// список пользователей
        /// </summary>
        /// <returns></returns>
        List<UserModel> GetUserList();

        /// <summary>
        /// редактирование личных данных пользователя
        /// </summary>
        /// <param name="id">id пользовтаеля</param>
        /// <param name="email">почтовый адрес пользователя</param>
        /// <param name="birtday">дата рождения пользователя</param>
        /// <param name="photo">аватар пользователя</param>
        void ChangeUserInfo(int id, string email, DateTime birtday, byte[] photo);

        /// <summary>
        /// смена пароля пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="newPassword">новый пароль пользователя</param>
        void ChangeUserPassword(int id, string newPassword);

        /// <summary>
        /// смена ролей пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="roles">роли пользователя</param>
        void ChangeUserRoles(int id, RolesEnum[] roles);

        /// <summary>
        /// удаление пользователя из бд
        /// </summary>
        /// <param name="token">токен</param>
        void DeleteUser(string token);

        /// <summary>
        /// добавление нового пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <param name="email">почтовый адрес пользователя</param>
        /// <param name="roles">список ролей пользователя</param>
        /// <param name="photo">аватар пользователя</param>
        void RegisterUser(string userName, string password, string email, RolesEnum[] roles, byte[] photo);

       
    }
}
