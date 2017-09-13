using IService.Models;
using IService.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService.Models.Public;
using Data.Models.Pubplic;

namespace Data.Service.Public
{
    public class DisplayContentService : IDisplayContentService
    {
        /// <summary>
        /// получение поста по его id
        /// </summary>
        /// <param name="UrlTitle">UrlTitle поста</param>
        /// <returns>пост с данными id</returns>
        public PostModel GetPostByUrl(string UrlTitle)
        {
            using (var db = new DataContext())
            {
                return (PostModel)db.Posts.First(_ => _.UrlTitle == UrlTitle);
            }
        }

        /// <summary>
        /// получение поста по его id
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <returns>пост с данными id</returns>
        public PostModel GetPostById(int idPost)
        {
            using (var db = new DataContext())
            {
                return (PostModel)db.Posts.First(_ => _.Id == idPost);
            }
        }

        /// <summary>
        /// количество страниц, которые будут выведены
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="tag">требуемый тэг</param>
        /// <param name="category">требуемая категория</param>
        /// <param name="pageSize">количество постов на страницу</param>
        /// <returns>количество страниц</returns>
        public int GetPageCountPost(string search, TagModel tag, CategoryModel category, int pageSize)
        {
            using (var db = new DataContext())
            {
                var posts = db.Posts.ToList();
                if (!string.IsNullOrEmpty(search))
                    posts = posts.Where(_ => _.Title.Contains(search)).ToList();
                if (tag.UrlName != null)
                    posts = posts.Where(_ => _.Tags.Any(a => a.UrlName == tag.UrlName)).ToList();
                if (category.UrlName != null)
                    posts = posts.Where(_ => _.Category.UrlName.Equals(category.UrlName)).ToList();

                return (int)Math.Ceiling(posts.Count() / (double)pageSize);
            }
        }

        /// <summary>
        /// получение списка постов
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество постов на странице</param>
        /// <param name="pageIndex">номер страниццы</param>
        /// <param name="tag">требуемый тэг</param>
        /// <param name="category">требемая категория</param>
        /// <returns>список постов, которые содержат указанный тэг или категорию
        /// поиск осуществляется по заголовкам постов</returns>
        public List<PostModel> GetPostList(string search, int pageSize, int pageIndex, TagModel tag, CategoryModel category)
        {
            using (var db = new DataContext())
            {
                var posts = db.Posts.ToList();
                if (!string.IsNullOrEmpty(search))
                    posts = posts.Where(_ => _.Title.Contains(search)).ToList();
                if (tag.UrlName != null)
                    posts = posts.Where(_ => _.Tags.Any(a => a.UrlName == tag.UrlName)).ToList();
                if (category.UrlName != null)
                    posts = posts.Where(_ => _.Category.UrlName.Equals(category.UrlName)).ToList();
                return posts.OrderByDescending(_ => _.Published).ToList().Select(_ => (PostModel)_)
                     .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// получение списка тэгов удовлетворяющих поиску и параметрам страницы
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество постов на странице</param>
        /// <param name="pageIndex">номер страниццы</param>
        /// <returns>список тэгов</returns>
        public List<TagModel> GetTagList(string search, int pageSize, int pageIndex)
        {
            using (var db = new DataContext())
            {
                return db.Tags.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Select(_ => (TagModel)_).ToList()
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// количетсво страниц с тэгами
        /// </summary>
        /// <param name="search">строка посика</param>
        /// <param name="pageSize">количество элементов на странице</param>
        /// <returns>количество страниц</returns>
        public int GetPageCountTag(string search, int pageSize)
        {
            using (var db = new DataContext())
            {
                return (int)Math.Ceiling(db.Tags.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Count() / (double)pageSize);
            }
        }

        /// <summary>
        /// получение списка категорий удовлетворяющих поиску и параметрам страницы
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество постов на странице</param>
        /// <param name="pageIndex">номер страниццы</param>
        /// <returns>список категорий</returns>
        public List<CategoryModel> GetCategoryList(string search, int pageSize, int pageIndex)
        {
            using (var db = new DataContext())
            {
                return db.Categories.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Select(_ => (CategoryModel)_).ToList()
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// количество страниц с категориями
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество элемнтов на стрнице</param>
        /// <returns>количество страниц</returns>
        public int GetPageCountCategory(string search, int pageSize)
        {
            using (var db = new DataContext())
            {
                return (int)Math.Ceiling(db.Categories.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Count() / (double)pageSize);
            }
        }

        /// <summary>
        /// получение списка комментариев к посту
        /// </summary>
        /// <param name="idPost">id просматриваемоо поста</param>
        /// <returns>список комментариев</returns>
        public List<CommentModel> GetCommentFromPost(int idPost)
        {
            using (var db = new DataContext())
            {
                var comments = db.Comment.Where(a => a.PostId == idPost).ToList().Select(_ => (CommentModel)_).ToList();
                foreach (var item in comments)
                {
                    if (item.ParentId != null)
                    {
                        int id = (item.ParentId ?? 1);
                        item.ParentAuthorName = GetCommentById(id).Author.UserName + ",";
                    }
                    else item.ParentAuthorName = "";
                }
                return comments;
            }
        }

        /// <summary>
        /// добавление комментария к посту
        /// </summary>
        /// <param name="authorId">автор поста</param>
        /// <param name="parentId">ответ к другому комментарию</param>
        /// <param name="body">текст комментария</param>
        /// <param name="postId">пост, под которым оставляют комментарии</param>
        public void AddCommentToPost(int authorId, int? ParentId, string body, int postId)
        {
            using (var db = new DataContext())
            {
                var comment = new Comment();
                comment.ParentId = null;
                comment.Body = body;
                if (body[0] == '@')
                {
                    var parent = body.Split(',')[0];
                    if (parent.Contains('#'))
                    {
                        try
                        {
                            var parentId = parent.Split('#')[1];
                            if (parentId.Length>0)
                            {
                                var intId = Convert.ToInt32(parentId);
                                if (db.Comment.Any(_ => _.Id == intId))
                                {
                                    comment.ParentId = intId;
                                    comment.Body = body.Replace(parent + ", ", "");
                                }
                            }                            
                        }
                        catch (Exception)
                        {
                            comment.ParentId = null;
                            comment.Body = body;
                        }
                    }
                }               
                comment.AuthorId = authorId;
                comment.PostId = postId;                
                comment.Published = DateTime.UtcNow;
                db.Comment.Add(comment);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// получение комментария по его id
        /// </summary>
        /// <param name="idComment">id требуемого комментария</param>
        /// <returns>искомый комментарий</returns>
        public CommentModel GetCommentById(int id)
        {
            using (var db = new DataContext())
            {
                var d = (CommentModel)db.Comment.First(a => a.Id == id);
                return (CommentModel)db.Comment.First(a => a.Id == id);
            }
        }
    }
}
