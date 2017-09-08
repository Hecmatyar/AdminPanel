using IService.Admin;
using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.AuthCustom;

namespace test.Areas.Admin.Controllers
{
    public class ControllerBase : Controller
    {
        protected IAdminService _AdminService;
        protected AuthorizeManager manager;
        /// <summary>
        /// отпределение зависимостей
        /// </summary>
        public ControllerBase()
        {           
            _AdminService = DependencyResolver.Current.GetService<IAdminService>();
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

        public class CustomByteArrayModelBinder : ByteArrayModelBinder
        {
            public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var file = controllerContext.HttpContext.Request.Files[bindingContext.ModelName];

                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, fileBytes.Length);
                        return fileBytes;
                    }

                    return null;
                }

                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}