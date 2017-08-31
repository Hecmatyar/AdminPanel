using IService;
using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.AuthCustom
{
    /// <summary>
    /// авторизация пользователя в системе
    /// </summary>
    public interface IAuthorizeManager
    {
        /// <summary>
        /// логин в системе
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rememberMe"></param>
        void Login(UserModel user, bool rememberMe);

        /// <summary>
        /// выход из системы
        /// </summary>
        void Logoff();       

        /// <summary>
        /// текущий пользователь
        /// </summary>
        UserModel CurrentUser { get; }
    }
}