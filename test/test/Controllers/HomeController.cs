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
        /// <summary>
        /// вывод ленты постов по страницам
        /// </summary>
        /// <param name="posts">модель постов с данными поиска</param>
        /// <returns>посты, удовлетворяющие параметрам поиска</returns>
        public ActionResult Index(Posts posts)
        {
            int pageSize = 2;
            int pageNumber = (posts.PageNumber ?? 1);
            if (posts.TagName != null || posts.CategoryName != null)
                pageNumber = 1;

            int countPage = _DisplayContent.GetPageCountPost(
                posts.SearchField,
                new TagModel { Name = posts.TagName ?? null },
                new CategoryModel { Name = posts.CategoryName ?? null },
                pageSize);

            if (pageNumber > countPage)
                pageNumber = countPage;

            posts.PageCount = countPage;
            posts.PageNumber = pageNumber;

            posts.PostsList = _DisplayContent.GetPostList(posts.SearchField,
                pageSize,
                pageNumber,
                new TagModel { Name = posts.TagName ?? null },
                new CategoryModel { Name = posts.CategoryName ?? null });

            return View(posts);
        }       
        /// <summary>
        /// страница с выбранным постом
        /// </summary>
        /// <param name="id">id выбранного поста</param>
        /// <returns>страница с постом</returns>
        [HttpGet]
        public ActionResult Post(int id)
        {
            Posts posts = new Posts
            {
                CurrentPosts = _DisplayContent.GetPostById(id)
            };
            return View(posts);
        }
        /// <summary>
        /// частичное представление со списком категорий
        /// </summary>
        /// <returns>список категорий</returns>
        [HttpGet]
        public ActionResult CategoryList()
        {
            var category = _DisplayContent.GetCategoryList(null, 10, 1);
            return View("_CategoryList", category);
        }
        /// <summary>
        /// частичное представление со спсиком тэгов
        /// </summary>
        /// <returns>список тэгов</returns>
        [HttpGet]
        public ActionResult TagList()
        {
            var tags = _DisplayContent.GetTagList(null, 10, 1);
            return View("_TagList", tags);
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