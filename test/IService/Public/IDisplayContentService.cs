using IService.Models;
using IService.Models.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Public
{
    public interface IDisplayContentService
    {
        /// <summary>
        /// получение поста по его id
        /// </summary>
        /// <param name="idPost">id поста</param>
        /// <returns>пост с данными id</returns>
        PostModel GetPostById(int idPost);
        /// <summary>
        /// получение поста по его urltitle
        /// </summary>
        /// <param name="idPost">urltitle поста</param>
        /// <returns>пост с данными id</returns>
        PostModel GetPostByUrl(string UrlTitle);
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
        /// получение комментария по его id
        /// </summary>
        /// <param name="idComment">id требуемого комментария</param>
        /// <returns></returns>
        CommentModel GetCommentById(int idComment);

        /// <summary>
        /// получение списка комментариев к посту
        /// </summary>
        /// <param name="idPost">id просматриваемого поста</param>
        /// <returns>список комментариев</returns>
        List<CommentModel> GetCommentFromPost(int idPost);
        
        /// <summary>
        /// добавление нового комментария к посту
        /// </summary>
        /// <param name="authorId">id автора поста</param>
        /// <param name="parentId">id родительского поста</param>
        /// <param name="body">текст поста</param>
        /// <param name="postId">пост, под которым был оставлен комментрарий</param>
        void AddCommentToPost(int authorId, int? parentId, string body, int postId);
    }
}
