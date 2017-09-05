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
            var post = _ModeratorService.GetPostList(null, 10, 1, new TagModel { Name = null }, new CategoryModel { Name = null });
            return View(post);
        }
        /// <summary>
        /// частичное представление со списком категорий
        /// </summary>
        /// <returns>список категорий</returns>
        [HttpGet]
        public ActionResult CategoryList()
        {
            var category = _ModeratorService.GetCategoryList(null, 10, 1);
            return View("CategoryListPartial", category);
        }
        /// <summary>
        /// частичное представление со спсиком тэгов
        /// </summary>
        /// <returns>список тэгов</returns>
        [HttpGet]
        public ActionResult TagList()
        {
            var tags = _ModeratorService.GetTagList(null, 10, 1);
            return View("TagListPartial", tags);
        }
        /// <summary>
        /// просто так с доступом админа
        /// </summary>
        /// <returns></returns>
        [AuthenticateAttribute(RolesEnum.Admin)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        /// <summary>
        /// просто так с доступом модератора
        /// </summary>
        /// <returns></returns>
        [AuthenticateAttribute(RolesEnum.Moderator)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}