using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.AuthCustom;
using Data;
using IService;
using System.Web.Security;
using IService.Models;
using IService.Admin;
using IService.Moderator;

namespace test.Controllers
{
    /// <summary>
    /// базовый контроллер
    /// </summary>
    public abstract class ControllerBase : Controller
    {       
        protected IAuthenticationService _AuthenticationRequest;
        protected IAdminService _AdminService;
        protected IAuthorizeManager manager;
        protected IModeratorService _ModeratorService;
        /// <summary>
        /// отпределение зависимостей
        /// </summary>
        public ControllerBase()
        {            
            _AuthenticationRequest = DependencyResolver.Current.GetService<IAuthenticationService>();
            _AdminService = DependencyResolver.Current.GetService<IAdminService>();
            _ModeratorService = DependencyResolver.Current.GetService<IModeratorService>();
            manager = DependencyResolver.Current.GetService<IAuthorizeManager>();
        }
        /// <summary>
        /// текущий пользователь системы
        /// </summary>
        public UserModel CurrentUser
        {
            get
            {                
                return DependencyResolver.Current.GetService<IAuthorizeManager>().CurrentUser;
            }
        }

        /// <summary>
        /// авторизован ли пользователь
        /// </summary>
        protected bool IsAuthenticated
        {
            get { return CurrentUser != null; }
        }
        
        /// <summary>
        /// переводит частичное в строку для отправки по почте
        /// </summary>
        /// <param name="viewName">частичное представление</param>
        /// <param name="model">модель данных</param>
        /// <returns>строка для отправки в теле письма</returns>
        public string RenderRazorViewToString(string viewName, object model)
        {           
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,  viewName);
                ViewData.Model = model;
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ControllerContext.Controller.ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }       
    }
}