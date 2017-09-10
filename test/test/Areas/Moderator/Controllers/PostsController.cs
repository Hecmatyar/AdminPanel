using IService.Models;
using IService.Models.Moderator;
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
            //var tagList = _ModeratorService.GetTagList(null, int.MaxValue, 1);
            //var post = _ModeratorService.GetPostById(id);
            //var categoryList = _ModeratorService.GetCategoryList(null, int.MaxValue, 1);

            //tagList = tagList.Where(a => !post.Tags.Any(r => r.Name == a.Name)).ToList();
            //EditCreatePost posts = new EditCreatePost
            //{
            //    CurrentPosts = post,
            //    TagsList = tagList,
            //    CategoriesList = categoryList
            //};
            var post = _ModeratorService.GetEditPostById(id);
            return View(post);
        }
        /// <summary>
        /// редактирование выбранного поста
        /// </summary>
        /// <param name="post">модель с новыми данными</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPost(EditCreatePostModel post)
        {
            _ModeratorService.EditPost(post.Id,
                post.Title,
                post.ShortDescription,
                post.Description,
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
            var post = _ModeratorService.GetEditPostById(0);
            //var tagList = _ModeratorService.GetTagList(null, int.MaxValue, 1);           
            //var categoryList = _ModeratorService.GetCategoryList(null, int.MaxValue, 1);
            
            //var posts = new EditCreatePost
            //{               
            //    TagsList = tagList,
            //    CategoriesList = categoryList
            //};
            return View(post);
        }
        /// <summary>
        /// создание нового поста
        /// </summary>
        /// <param name="post">модель с новыми данными</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreatePost(EditCreatePostModel post)
        {
            _ModeratorService.CreatePost(
                post.Title,
                post.ShortDescription,
                post.Description,
                post.SelectedCategory,
                post.SelectedTag,
                manager.CurrentUser.Id);
            return RedirectToAction("/Posts");
        }
        /// <summary>
        /// удаление поста
        /// </summary>
        /// <param name="id">id удаляемого поста</param>
        /// <returns></returns>
        public ActionResult DeletePost(int id)
        {
            _ModeratorService.DeletePost(id);
            return RedirectToAction("/Posts");
        }
    }
}