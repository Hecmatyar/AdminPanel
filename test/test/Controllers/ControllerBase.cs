﻿using System;
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
using test.Letters;
using IService.Public;

namespace test.Controllers
{
    /// <summary>
    /// базовый контроллер
    /// </summary>
    public abstract class ControllerBase : Controller
    {       
        protected IAuthenticationService _AuthenticationRequest;
        protected IDisplayContentService _DisplayContent;
        protected AuthorizeManager manager;
        /// <summary>
        /// отпределение зависимостей
        /// </summary>
        public ControllerBase()
        {
            _DisplayContent = DependencyResolver.Current.GetService<IDisplayContentService>();
            _AuthenticationRequest = DependencyResolver.Current.GetService<IAuthenticationService>();             
            manager = new AuthorizeManager();
        }
        /// <summary>
        /// текущий пользователь системы
        /// </summary>
        public UserModel CurrentUser
        {
            get
            {                
                return manager.CurrentUser;
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
        public string RenderRazorViewToString(string viewName, object model, ControllerContext context)
        {
            try
            {
                using (var sw = new StringWriter())
                {                    
                    var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                    ViewData.Model = model;
                    var viewContext = new ViewContext(context, viewResult.View,
                                                 ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
}