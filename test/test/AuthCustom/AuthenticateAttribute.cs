using IService;
using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.AuthCustom
{
    /// <summary>
    /// можно ли пользователю находится на этой странице
    /// </summary>
    public class AuthenticateAttribute : AuthorizeAttribute
    {        
        public bool AllowAnonymus { get; set; }

        public RolesEnum AccessTole { get; set; }

        public AuthenticateAttribute()
        {
        }
        /// <summary>
        /// доступ неавторизаванных пользователей
        /// </summary>
        /// <param name="allowAnonymus"></param>
        public AuthenticateAttribute(bool allowAnonymus)
        {
            AllowAnonymus = allowAnonymus;
        }

        /// <summary>
        /// проверка соответсвия роли пользователя и роли ограничителя
        /// </summary>
        /// <param name="accessRole">роль доступа</param>
        public AuthenticateAttribute(RolesEnum accessRole) { AccessTole = accessRole; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (AllowAnonymus)
                return true;
            
            UserModel user = DependencyResolver.Current.GetService<IAuthorizeManager>().CurrentUser;
            if (user == null)
                return false;

            if (AccessTole == 0)
                return true;

            return user.IsInRole(AccessTole);
        }

        /// <summary>
        /// отправка на страницу логина
        /// </summary>
        /// <param name="filterContext">контекст</param>
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            filterContext.Result = new System.Web.Mvc.RedirectResult("/Account/login", false);
        }
    }
}