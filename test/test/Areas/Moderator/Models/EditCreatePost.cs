using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Areas.Moderator.Models
{
    public class EditCreatePost
    {
        /// <summary>
        /// выбранные тэги
        /// </summary>
        public int[] SelectedTag { get; set; }
        /// <summary>
        /// выбранная категория
        /// </summary>
        public int SelectedCategory { get; set; }
        /// <summary>
        /// выбранный пост из ленты
        /// </summary>
        public PostModel CurrentPosts { get; set; }
        /// <summary>
        /// список постов
        /// </summary>
        public List<TagModel> TagsList { get; set; }
        /// <summary>
        /// список категорий
        /// </summary>
        public List<CategoryModel> CategoriesList { get; set; }
    }
}