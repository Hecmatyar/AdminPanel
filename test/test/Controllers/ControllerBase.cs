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

namespace test.Controllers
{
    public abstract class ControllerBase : Controller
    {       
        protected IAuthenticationService _AuthenticationRequest;
        protected IAdminService _AdminService;
        protected IAuthorizeManager manager;
        //protected AuthorizeManager _AuthorizeManager;

        public ControllerBase()
        {            
            _AuthenticationRequest = DependencyResolver.Current.GetService<IAuthenticationService>();
            _AdminService = DependencyResolver.Current.GetService<IAdminService>();
            manager = DependencyResolver.Current.GetService<IAuthorizeManager>();
        }

        public UserModel CurrentUser
        {
            get
            {
                //return DependencyResolver.Current.GetService<AuthorizeManager>().CurrentUser;
                return DependencyResolver.Current.GetService<IAuthorizeManager>().CurrentUser;
            }
        }

        protected bool IsAuthenticated
        {
            get { return CurrentUser != null; }
        }
        
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