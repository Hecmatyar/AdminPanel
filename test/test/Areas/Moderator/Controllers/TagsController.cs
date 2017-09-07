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
    public class TagsController : ControllerBase
    {
        // GET: Moderator/Tags
        public ActionResult Tags()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditTag(int id)
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult CreateTag()
        {
            return View();
        }
    }
}