﻿using IService.Models;
using IService.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Admin
{
    /// <summary>
    /// интерфейс для методов администрирования пользователей ресурса
    /// добавление новых пользователей, редактирование и удаление имеющихся пользователей
    /// получение списка имеющихся пользователей
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// получение данных пользователя для смены пароля
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модель с данными для смены пароля</returns>
        UserPasswordModel GetUserPasswordById(int id);

        /// <summary>
        /// получение данных пользователя для смены ролей
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модель с данными для смены ролей</returns>
        UserRolesModel GetUserRolesById(int id);

        /// <summary>
        /// получение личных данных пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модель с данными для их редактирования</returns>
        UserInfoModel GetUserInfoById(int id);

        /// <summary>
        /// получение данных пользователя после его аутентификации в системе
        /// </summary>
        /// <param name="id">id пользователя в бд</param>
        /// <returns></returns>
        UserModel GetUserById(int id);

        /// <summary>
        /// список пользователей
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество элементов на странице</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <returns></returns>
        List<UserModel> GetUserList(string search, int pageSize, int pageIndex);

        /// <summary>
        /// количество страниц в таблице
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество элементов на станицу</param>
        /// <returns></returns>
        int GetPageCount(string search, int pageSize);

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
        void DeleteUser(int id);

        /// <summary>
        /// добавление нового пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <param name="password">пароль пользователя</param>      
        void RegisterUser(string userName, string password);
    }
}
