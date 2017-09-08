using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using test.App_Start;
using test.Models;
using static test.Areas.Admin.Controllers.ControllerBase;

namespace test
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            AutofacConfig.ConfigureContainer();            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //в байты картинки
            ModelBinders.Binders.Remove(typeof(byte[]));
            ModelBinders.Binders.Add(typeof(byte[]), new CustomByteArrayModelBinder());
        }
    }
}
