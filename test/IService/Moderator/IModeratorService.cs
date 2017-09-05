using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Moderator
{
    /// <summary>
    /// интерфейс модератора,
    /// работа с тэгами, категориями, постами
    /// </summary>
    public interface IModeratorService
    {
        /// <summary>
        /// получение списка постов
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество постов на странице</param>
        /// <param name="pageIndex">номер страниццы</param>
        /// <param name="tag">требуемый тэг</param>
        /// <param name="category">требемая категория</param>
        /// <returns>список постов, которые содержат указанный тэг или категорию,
        /// поиск осуществляется по авторам и именам постов</returns>
        List<PostModel> GetPostList(string search, int pageSize, int pageIndex, TagModel tag, CategoryModel category);
        /// <summary>
        /// количество страницс постами, которые будут выведены
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="tag">требуемый тэг</param>
        /// <param name="category">требуемая категория</param>
        /// <param name="pageSize">количество постов на страницу</param>
        /// <returns>количество страниц</returns>
        int GetPageCountPost(string search, TagModel tag, CategoryModel category, int pageSize);
        /// <summary>
        /// получение списка тэгов удовлетворяющих поиску и параметрам страницы
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество постов на странице</param>
        /// <param name="pageIndex">номер страниццы</param>
        /// <returns>список тэгов</returns>
        List<TagModel> GetTagList(string search, int pageSize, int pageIndex);
        /// <summary>
        /// количетсво страниц с тэгами
        /// </summary>
        /// <param name="search">строка посика</param>
        /// <param name="pageSize">количество элементов на странице</param>
        /// <returns>количество страниц</returns>
        int GetPageCountTag(string search, int pageSize);
        /// <summary>
        /// получение списка категорий удовлетворяющих поиску и параметрам страницы
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество постов на странице</param>
        /// <param name="pageIndex">номер страниццы</param>
        /// <returns>список категорий</returns>
        List<CategoryModel> GetCategoryList(string search, int pageSize, int pageIndex);
        /// <summary>
        /// количество страниц с категориями
        /// </summary>
        /// <param name="search">строка поиска</param>
        /// <param name="pageSize">количество элемнтов на стрнице</param>
        /// <returns>количество страниц</returns>
        int GetPageCountCategory(string search, int pageSize);
        /// <summary>
        /// редактирование поста
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <param name="post">модель с новыми данными</param>
        void EditPost(int idPost, PostModel post);
        /// <summary>
        /// редактирование тэга
        /// </summary>
        /// <param name="idTag">id тэга</param>
        /// <param name="tag">модель с новыми данными</param>
        void EditTag(int idTag, TagModel tag);
        /// <summary>
        /// редактирование категории
        /// </summary>
        /// <param name="idCategory">id категории</param>
        /// <param name="category">модель с новыми данными</param>
        void EditCategory(int idCategory, CategoryModel category);
        /// <summary>
        /// получение поста по его id
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <returns>пост с данными id</returns>
        PostModel GetPostById(int idPost);
        /// <summary>
        /// получение тэга по его id
        /// </summary>
        /// <param name="idTag">id тэга</param>
        /// <returns>тэг с данным id</returns>
        TagModel GetTagById(int idTag);
        /// <summary>
        /// получение категории по ее id
        /// </summary>
        /// <param name="idCategory">id категории</param>
        /// <returns>категория с данным id</returns>
        CategoryModel GetCategoryById(int idCategory);
        /// <summary>
        /// удаление поста
        /// </summary>
        /// <param name="id">id поста</param>
        void DeletePost(int id);
        /// <summary>
        /// удаление категории
        /// </summary>
        /// <param name="id">id категории</param>
        void DeleteCategory(int id);
        /// <summary>
        /// удаление тэга
        /// </summary>
        /// <param name="id">id тэга</param>
        void DeleteTag(int id);
    }
}
