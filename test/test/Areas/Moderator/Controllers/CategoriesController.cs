using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.AuthCustom;

namespace test.Areas.Moderator.Controllers
{
    [AuthenticateAttribute(RolesEnum.Moderator)]
    public class CategoriesController : ControllerBase
    {
        // GET: Moderator/Categories
        public ActionResult Categories()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
    }
}