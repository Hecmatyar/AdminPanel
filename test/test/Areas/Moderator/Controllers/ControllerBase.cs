using IService.Models;
using IService.Moderator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.AuthCustom;

namespace test.Areas.Moderator.Controllers
{
    public class ControllerBase : Controller
    {
        protected IModeratorService _ModeratorService;
        protected AuthorizeManager manager;
        /// <summary>
        /// отпределение зависимостей
        /// </summary>
        public ControllerBase()
        {
            _ModeratorService = DependencyResolver.Current.GetService<IModeratorService>();
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
    }
}