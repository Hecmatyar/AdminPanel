using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace test
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default_Post",
                url: "post/{urlTitle}",
                defaults: new { controller = "Home", action = "Post" }
            );           
            routes.MapRoute(
               name: "Default_category",
               url: "category/{category}/page{page}",
               defaults: new
               {
                   controller = "Home",
                   action = "Index"
               }
            );
            routes.MapRoute(
              name: "Default_tag",
              url: "tag/{tag}/page{page}",
              defaults: new
              {
                  controller = "Home",
                  action = "Index"
              }
            );
            routes.MapRoute(
             name: "Default_page",
             url: "page{page}",
             defaults: new
             {
                 controller = "Home",
                 action = "Index"
             }
           );
            routes.MapRoute(
              name: "Default_search",
              url: "search/page{page}",
              defaults: new
              {
                  controller = "Home",
                  action = "Index"
              }
            );           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );                        
        }
    }
}
