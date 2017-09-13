using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using IService.Models;

namespace test.AuthCustom
{
    /// <summary>
    /// управление аутентификацией пользователя
    /// </summary>
    public class AuthorizeManager
    {
        protected IAuthenticationService _AuthenticationRequest;
        public AuthorizeManager()
        {
            _AuthenticationRequest = DependencyResolver.Current.GetService<IAuthenticationService>();
        }

        private const string AuthCookieName = "AuthCookie";

        /// <summary>
        /// логин пользователя в системе
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="rememberMe">оставаться в системе</param>
        public void Login(UserModel user, bool rememberMe)
        {
            DateTime expiresDate = DateTime.Now.AddMinutes(30);
            if (rememberMe)
                expiresDate = expiresDate.AddDays(10);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.Id.ToString(),
                DateTime.Now,
                expiresDate, rememberMe, user.Id.ToString());
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            SetValue(AuthCookieName, encryptedTicket, expiresDate);

            _currentUser = user;
        }
        /// <summary>
        /// выход 
        /// </summary>
        public void Logoff()
        {
            SetValue(AuthCookieName, null, DateTime.Now.AddYears(-1));
            _currentUser = null;
        }        

        /// <summary>
        /// текущий пользователь системы
        /// </summary>
        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        object cookie = HttpContext.Current.Request.Cookies[AuthCookieName] != null ? HttpContext.Current.Request.Cookies[AuthCookieName].Value : null;
                        if (cookie != null && !string.IsNullOrEmpty(cookie.ToString()))
                        {
                            var ticket = FormsAuthentication.Decrypt(cookie.ToString());
                            _currentUser = _AuthenticationRequest.GetUserById(int.Parse(ticket.Name));
                        }

                    }
                    catch (Exception ex)
                    {
                        _currentUser = null;
                    }
                }
                return _currentUser;
            }
        }
        /// <summary>
        /// установка значения для login и logoff
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieObject"></param>
        /// <param name="dateStoreTo"></param>
        public static void SetValue(string cookieName, string cookieObject, DateTime dateStoreTo)
        {

            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                cookie.Path = "/";
            }
            cookie.Value = cookieObject;
            cookie.Expires = dateStoreTo;
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }
}