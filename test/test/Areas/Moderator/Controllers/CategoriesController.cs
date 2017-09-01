using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Areas.Moderator.Controllers
{
    public class CategoriesController : test.Controllers.ControllerBase
    {
        // GET: Moderator/Categories
        public ActionResult Index()
        {
            return View();
        }
    }
}