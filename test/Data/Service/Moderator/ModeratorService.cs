using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService.Models;
using IService.Moderator;
using System.Data.Entity;

namespace Data.Service.Moderator
{
    /// <summary>
    /// реализация интерфейса модератора
    /// </summary>
    public class ModeratorService : IModeratorService
    {
        private DataContext db = new DataContext();
        /// <summary>
        /// удаление категории
        /// </summary>
        /// <param name="id">id категории</param>
        public void DeleteCategory(int id)
        {
            var category = db.Categories.First(_ => _.Id == id);
            db.Entry(category).State = EntityState.Deleted;
            db.SaveChanges();
        }
        /// <summary>
        /// удаление поста
        /// </summary>
        /// <param name="id">id поста</param>
        public void DeletePost(int id)
        {
            var post = db.Posts.First(_ => _.Id == id);
            db.Entry(post).State = EntityState.Deleted;
            db.SaveChanges();
        }
        /// <summary>
        /// удаление тэга
        /// </summary>
        /// <param name="id">id тэга</param>
        public void DeleteTag(int id)
        {
            var tag = db.Tags.First(_ => _.Id == id);
            db.Entry(tag).State = EntityState.Deleted;
            db.SaveChanges();
        }

        /// <summary>
        /// редактирование категории
        /// </summary>
        /// <param name="idCategory">id категории</param>
        /// <param name="category">модель с новыми данными</param>
        public void EditCategory(int idCategory, CategoryModel category)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// редактирование поста
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <param name="post">модель с новыми данными</param>
        public void EditPost(int idPost, PostModel post)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// редактирование тэга
        /// </summary>
        /// <param name="idTag">id тэга</param>
        /// <param name="tag">модель с новыми данными</param>
        public void EditTag(int idTag, TagModel tag)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// получение тэга по его id
        /// </summary>
        /// <param name="idTag">id тэга</param>
        /// <returns>тэг с данным id</returns>
        public TagModel GetTagById(int idTag)
        {
            return (TagModel)db.Tags.First(_ => _.Id == idTag);
        }
        /// <summary>
        /// получение поста по его id
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <returns>пост с данными id</returns>
        public PostModel GetPostById(int idPost)
        {
            return (PostModel)db.Posts.First(_ => _.Id == idPost);
        }
        /// <summary>
        /// получение категории по ее id
        /// </summary>
        /// <param name="idCategory">id категории</param>
        /// <returns>категория с данным id</returns>
        public CategoryModel GetCategoryById(int idCategory)
        {
            return (CategoryModel)db.Categories.First(_ => _.Id == idCategory);
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
            var posts = db.Posts.ToList();
            if (!string.IsNullOrEmpty(search))
                posts = posts.Where(_ => _.Title.Contains(search)).ToList();
            if (tag.Name != null)
                posts = posts.Where(_ => _.Tags.Any(a => a.Name == tag.Name)).ToList();
            if (category.Name != null)
                posts = posts.Where(_ => _.Category.Name.Equals(category.Name)).ToList();

            return (int)Math.Ceiling(posts.Count() / (double)pageSize);
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
            var posts = db.Posts.ToList();
            if (!string.IsNullOrEmpty(search))
                posts = posts.Where(_ => _.Title.Contains(search)).ToList();
            if (tag.Name != null)
                posts = posts.Where(_ => _.Tags.Any(a => a.Name == tag.Name)).ToList();
            if (category.Name != null)
                posts = posts.Where(_ => _.Category.Name.Equals(category.Name)).ToList();
            return posts.Select(_ => (PostModel)_)
                 .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
            return db.Tags.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Select(_ => (TagModel)_).ToList()
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// количетсво страниц с тэгами
        /// </summary>
        /// <param name="search">строка посика</param>
        /// <param name="pageSize">количество элементов на странице</param>
        /// <returns>количество страниц</returns>
        public int GetPageCountTag(string search, int pageSize)
        {
            return (int)Math.Ceiling(db.Tags.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Count() / (double)pageSize);
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
            return db.Categories.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Select(_ => (CategoryModel)_).ToList()
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// количество страниц с категориями
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество элемнтов на стрнице</param>
        /// <returns>количество страниц</returns>
        public int GetPageCountCategory(string search, int pageSize)
        {
            return (int)Math.Ceiling(db.Categories.Where(_ => string.IsNullOrEmpty(search) ? true : _.Name.Contains(search))
                .ToList().Count() / (double)pageSize);
        }
    }
}
