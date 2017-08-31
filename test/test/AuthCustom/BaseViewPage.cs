using IService;
using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.AuthCustom
{
    public class BaseViewPage : WebViewPage
    {        
        /// <summary>
        /// текущий пользователь
        /// </summary>
        public virtual new UserModel User
        {
            get
            {
                return DependencyResolver.Current.GetService<IAuthorizeManager>().CurrentUser;               
            }
        }
        /// <summary>
        /// аутентифицирован ли пользователь в системе
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        /// <summary>
        /// дает ли роль пользователя доступ
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool IsInRole(RolesEnum roles)
        {
            if (User == null)
                return false;

            return User.IsInRole(roles);
        }

        public override void Execute() { throw new NotImplementedException(); }
    }

    public class BaseViewPage<TModel> : WebViewPage<TModel>
    {       
        /// <summary>
        /// текущий пользователь
        /// </summary>
        public virtual new UserModel User
        {
            get
            {
                return DependencyResolver.Current.GetService<IAuthorizeManager>().CurrentUser;               
            }
        }

        /// <summary>
        /// аутентифицирован ли пользователь в системе
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        /// <summary>
        /// дает ли роль пользователя доступ
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool IsInRole(RolesEnum roles)
        {
            if (User == null)
                return false;

            return User.IsInRole(roles);
        }

        public override void Execute() { throw new NotImplementedException(); }
    }
}