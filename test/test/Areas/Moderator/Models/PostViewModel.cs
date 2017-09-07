using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Areas.Moderator.Models
{
    /// <summary>
    /// отображение постов
    /// </summary>
    public class PostViewModel
    {
        /// <summary>
        /// выбранный тэг
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// выбранная категория
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// строка поиска
        /// </summary>
        public string SearchField { get; set; }
        /// <summary>
        /// страница 
        /// </summary>
        public int? PageNumber { get; set; }
        /// <summary>
        /// количество страниц
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// посты в ленте
        /// </summary>
        public List<PostModel> PostsList { get; set; }
        /// <summary>
        /// список категорий
        /// </summary>
        //public List<CategoryModel> CategoriesList { get; set; }
        /// <summary>
        /// список постов
        /// </summary>
        //public List<TagModel> TagsList { get; set; }
        /// <summary>
        /// выбранный пост из ленты
        /// </summary>
        public PostModel CurrentPosts { get; set; }
        
    }
}