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
            int pageNumber = (posts.Page ?? 1);
            if (!string.IsNullOrEmpty(posts.q))
            {
                posts.Tag = null;
                posts.Category = null;
            }
            int countPage = _DisplayContent.GetPageCountPost(
                posts.q,
                new TagModel { UrlName = posts.Tag ?? null },
                new CategoryModel { UrlName = posts.Category ?? null },
                pageSize);

            if (pageNumber > countPage)
                pageNumber = countPage;

            posts.PageCount = countPage;
            posts.Page = pageNumber;

            posts.PostsList = _DisplayContent.GetPostList(posts.q,
                pageSize,
                pageNumber,
                new TagModel { UrlName = posts.Tag ?? null },
                new CategoryModel { UrlName = posts.Category ?? null });

            return View(posts);
        }
        /// <summary>
        /// страница с выбранным постом
        /// </summary>
        /// <param name="urlTitle">urlTitle выбранного поста</param>
        /// <returns>страница с постом</returns>
        [HttpGet]
        public ActionResult Post(string urlTitle)
        {
            var d = _DisplayContent.GetCommentFromPost(15);
            Posts posts = new Posts
            {
                CurrentPosts = _DisplayContent.GetPostByUrl(urlTitle)
            };
            return View(posts);
        }
        /// <summary>
        /// промежуточный метод, потом уберу
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCommentFromPost(int? idPost)
        {
            var comments = _DisplayContent.GetCommentFromPost(15);            
            return View("_CommentList", comments);
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