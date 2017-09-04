using IService;
using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.AuthCustom;
using test.Letters;
using test.Models;

namespace test.Controllers
{
    public class HomeController : ControllerBase
    {        
        public ActionResult Index()
        {  
            var post = _ModeratorService.GetPostList(null, 10, 1, new TagModel { Name = null }, new CategoryModel { Name = null});
            return View(post);
        }

        [AuthenticateAttribute(RolesEnum.Admin)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        [AuthenticateAttribute(RolesEnum.Moderator)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}