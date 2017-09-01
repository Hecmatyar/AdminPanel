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
        private PostContext db = new PostContext();
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
        public int GetPageCount(string search, TagModel tag, CategoryModel category, int pageSize)
        {
            return(int)Math.Ceiling(
                db.Posts.Where(_ => string.IsNullOrEmpty(search) ? true : _.Title.Contains(search) ||
                _.Tags.Any(a=>a.Name == tag.Name) || _.Category.Name.Equals(category.Name))
                .Count() / (double)pageSize);
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
        /// получение списка постов
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество постов на странице</param>
        /// <param name="pageIndex">номер страниццы</param>
        /// <param name="tag">требуемый тэг</param>
        /// <param name="category">требемая категория</param>
        /// <returns>список постов, которые содержат указанный тэг или категорию
        /// поиск осуществляется по авторам и именам постов</returns>
        public List<PostModel> GetPostList(string search, int pageSize, int pageIndex, TagModel tag, CategoryModel category)
        {
            return db.Posts
                .Where(_ => string.IsNullOrEmpty(search) ? true : _.Title.Contains(search) ||
                _.Tags.Any(a => a.Name == tag.Name) || _.Category.Name.Equals(category.Name))
                .ToList().Select(_ => (PostModel)_).ToList();
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
    }
}
