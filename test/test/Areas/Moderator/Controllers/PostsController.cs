using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Areas.Moderator.Models;
using test.AuthCustom;

namespace test.Areas.Moderator.Controllers
{
    [AuthenticateAttribute(RolesEnum.Moderator)]
    public class PostsController : ControllerBase
    {
        /// <summary>
        /// вывод ленты постов по страницам
        /// </summary>
        /// <param name="posts">модель постов с данными поиска</param>
        /// <returns>посты, удовлетворяющие параметрам поиска</returns>
        public ActionResult Posts(PostViewModel posts)
        {
            int pageSize = 5;
            int pageNumber = (posts.PageNumber ?? 1);
            if (posts.TagName != null || posts.CategoryName != null)
                pageNumber = 1;

            int countPage = _ModeratorService.GetPageCountPost(
                posts.SearchField,
                new TagModel { Name = posts.TagName ?? null },
                new CategoryModel { Name = posts.CategoryName ?? null },
                pageSize);

            if (pageNumber > countPage)
                pageNumber = countPage;

            posts.PageCount = countPage;
            posts.PageNumber = pageNumber;

            posts.PostsList = _ModeratorService.GetPostList(posts.SearchField,
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
        public ActionResult EditPost(int id)
        {
            var tagList = _ModeratorService.GetTagList(null, int.MaxValue, 1);
            var post = _ModeratorService.GetPostById(id);
            var categoryList = _ModeratorService.GetCategoryList(null, int.MaxValue, 1);

            tagList = tagList.Where(a => !post.Tags.Any(r => r.Name == a.Name)).ToList();
            CreateEditPost posts = new CreateEditPost
            {
                CurrentPosts = post,
                TagsList = tagList,
                CategoriesList = categoryList
            };
            return View(posts);
        }
        [HttpPost]
        public ActionResult EditPost(CreateEditPost post)
        {
            _ModeratorService.EditPost(post.CurrentPosts.Id,
                post.CurrentPosts.Title,
                post.CurrentPosts.ShortDescription,
                post.CurrentPosts.Description,
                post.SelectedCategory,
                post.SelectedTag,
                manager.CurrentUser.Id);
            return RedirectToAction("/Posts");
        }
        /// <summary>
        /// создание нового поста
        /// </summary>
        /// <returns>страница создания поста</returns>
        public ActionResult CreatePost()
        {
            var tagList = _ModeratorService.GetTagList(null, int.MaxValue, 1);           
            var categoryList = _ModeratorService.GetCategoryList(null, int.MaxValue, 1);
            
            CreateEditPost posts = new CreateEditPost
            {               
                TagsList = tagList,
                CategoriesList = categoryList
            };
            return View(posts);
        }
        [HttpPost]
        public ActionResult CreatePost(CreateEditPost post)
        {
            _ModeratorService.CreatePost(
                post.CurrentPosts.Title,
                post.CurrentPosts.ShortDescription,
                post.CurrentPosts.Description,
                post.SelectedCategory,
                post.SelectedTag,
                manager.CurrentUser.Id);
            return RedirectToAction("/Posts");
        }
        public ActionResult DeletePost(int id)
        {
            _ModeratorService.DeletePost(id);
            return RedirectToAction("/Posts");
        }
    }
}