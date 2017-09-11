using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService.Models;
using IService.Moderator;
using System.Data.Entity;
using Data.Models.Moderator;
using Data.Models.Admin;
using IService.Models.Moderator;

namespace Data.Service.Moderator
{
    /// <summary>
    /// реализация интерфейса модератора
    /// </summary>
    public class ModeratorService : IModeratorService
    {
        /// <summary>
        /// удаление категории
        /// </summary>
        /// <param name="id">id категории</param>
        public void DeleteCategory(int id)
        {
            using (var db = new DataContext())
            {
                var category = db.Categories.First(_ => _.Id == id);
                db.Entry(category).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// удаление поста
        /// </summary>
        /// <param name="id">id поста</param>
        public void DeletePost(int id)
        {
            using (var db = new DataContext())
            {
                var post = db.Posts.First(_ => _.Id == id);
                db.Entry(post).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// удаление тэга
        /// </summary>
        /// <param name="id">id тэга</param>
        public void DeleteTag(int id)
        {
            using (var db = new DataContext())
            {
                var tag = db.Tags.First(_ => _.Id == id);
                db.Entry(tag).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// редактирование категории
        /// </summary>
        /// <param name="idCategory">id категории</param>
        /// <param name="name">новое имя категории</param>
        public void EditCategory(int idCategory, string name)
        {
            using (var db = new DataContext())
            {
                var category = db.Categories.FirstOrDefault(a => a.Id == idCategory);
                category.Name = name;
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// редактирование поста
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <param name="title">заголовок поста</param>
        /// <param name="shortdescription">краткое опсиание поста</param>
        /// <param name="description">полное описание поста</param>
        /// <param name="category">категория поста</param>
        /// <param name="tags">тэги поста</param>
        /// <param name="authorId">автор поста</param>
        public void EditPost(int idPost, string title, string titleUrl, string shortdescription, string description, int category, int[] tags, int authorId)
        {
            using (var db = new DataContext())
            {
                var Tags = db.Tags.Where(a => tags.Contains(a.Id)).ToList();
                var post = db.Posts.FirstOrDefault(a => a.Id == idPost);

                post.Title = title;
                post.ShortDescription = shortdescription;
                post.Description = description;
                post.CategoryId = category;
                post.Tags.Clear();
                post.Tags = Tags;
                post.Published = DateTime.UtcNow;
                post.UserId = authorId;
                post.UrlTitle = titleUrl;

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
            DeleteExcessTag();
        }
        /// <summary>
        /// редактирование тэга
        /// </summary>
        /// <param name="idTag">id тэга</param>
        /// <param name="name">имя тэга</param>
        public void EditTag(int idTag, string name)
        {
            using (var db = new DataContext())
            {
                var tag = db.Tags.FirstOrDefault(a => a.Id == idTag);
                tag.Name = name;
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// добавление категории
        /// </summary>
        /// <param name="name">имя новой категории</param>
        public void CreateCategory(string name)
        {
            using (var db = new DataContext())
            {
                var category = new Category()
                {
                    Name = name,
                };
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// добавление поста
        /// </summary>
        /// <param name="title">заголовок поста</param>
        /// <param name="shortdescription">краткое опсиание поста</param>
        /// <param name="description">полное описание поста</param>
        /// <param name="category">категория поста</param>
        /// <param name="tags">тэги поста</param>
        /// <param name="authorId">автор поста</param>
        public void CreatePost(string title, string titleUrl, string shortdescription, string description, int category, int[] tags, string[] stags, int authorId)
        {
            using (var db = new DataContext())
            {
                //var Tags = db.Tags.Where(a => tags.Contains(a.Id)).ToList();

                List<Tag> Tags = new List<Tag>();
                int[] newtag = new int[stags.Length];
                foreach (var item in stags)
                {
                    if (db.Tags.Any(a => a.Name == item))
                    {
                        Tags.Add(db.Tags.First(_ => _.Name == item));
                    }
                    else
                    {
                        db.Tags.Add(new Tag { Name = item });
                        db.SaveChanges();
                        Tags.Add(db.Tags.First(_ => _.Name == item));
                    }
                }
                Post post = new Post()
                {
                    Title = title,
                    UrlTitle = titleUrl,
                    ShortDescription = shortdescription,
                    Description = description,
                    CategoryId = category,
                    Tags = Tags,
                    Published = DateTime.UtcNow,
                    UserId = authorId
                };
                db.Posts.Add(post);
                db.SaveChanges();               
            }
            DeleteExcessTag();
        }
        /// <summary>
        /// добавление тэга
        /// </summary>
        /// <param name="name">имя нового тэга</param>
        public void CreateTag(string name)
        {
            using (var db = new DataContext())
            {
                var tag = new Tag()
                {
                    Name = name,
                };
                db.Tags.Add(tag);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// получение тэга по его id
        /// </summary>
        /// <param name="idTag">id тэга</param>
        /// <returns>тэг с данным id</returns>
        public TagModel GetTagById(int idTag)
        {
            using (var db = new DataContext())
            {
                return (TagModel)db.Tags.First(_ => _.Id == idTag);
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
        /// получение поста по его id
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <returns>пост с данными id</returns>
        public EditCreatePostModel GetEditPostById(int idPost)
        {
            using (var db = new DataContext())
            {
                var post = idPost != 0 ? (EditCreatePostModel)db.Posts.FirstOrDefault(a => a.Id == idPost) : new EditCreatePostModel();
                var tagslist = db.Tags.ToList();
                var categorieslist = db.Categories.ToList();
                if (idPost != 0)
                {
                    tagslist = tagslist.Where(a => !post.Tags.Any(r => r.Name == a.Name)).ToList();
                }
                post.UnTags = tagslist.Select(_ => (TagModel)_).ToList();
                post.CategoriesList = categorieslist.Select(_ => (CategoryModel)_).ToList();
                return post;
            }

        }
        /// <summary>
        /// получение категории по ее id
        /// </summary>
        /// <param name="idCategory">id категории</param>
        /// <returns>категория с данным id</returns>
        public CategoryModel GetCategoryById(int idCategory)
        {
            using (var db = new DataContext())
            {
                return (CategoryModel)db.Categories.First(_ => _.Id == idCategory);
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
                    posts = posts.Where(_ => _.Title.ToLower().Contains(search.ToLower())).ToList();
                if (tag.Name != null)
                    posts = posts.Where(_ => _.Tags.Any(a => a.Name == tag.Name)).ToList();
                if (category.Name != null)
                    posts = posts.Where(_ => _.Category.Name.Equals(category.Name)).ToList();

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
                    posts = posts.Where(_ => _.Title.ToLower().Contains(search.ToLower())).ToList();
                if (tag.Name != null)
                    posts = posts.Where(_ => _.Tags.Any(a => a.Name == tag.Name)).ToList();
                if (category.Name != null)
                    posts = posts.Where(_ => _.Category.Name.Equals(category.Name)).ToList();
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
        /// удаление неиспользуемых тэгов из бд
        /// </summary>
        void DeleteExcessTag()
        {
            using (var db = new DataContext())
            {
                var tags = db.Tags.ToList();
                foreach (var item in tags)
                {
                    if (item.Posts.Count() == 0)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
