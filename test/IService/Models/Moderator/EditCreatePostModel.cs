using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models.Moderator
{
    public class EditCreatePostModel
    {
        /// <summary>
        /// id поста
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// заголовок поста
        /// </summary>
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        /// <summary>
        /// заголовок для адресной строки
        /// </summary>
        public string UrlTitle { get; set; }
        /// <summary>
        /// краткое описание поста
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// полный текст поста
        /// </summary>
        public string Description { get; set; }        
        /// <summary>
        /// категория, к которой относится пост
        /// </summary>
        //public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        /// <summary>
        /// список тэгов поста
        /// </summary>
        public List<TagModel> Tags { get; set; }

        /// <summary>
        /// выбранные тэги
        /// </summary>
        public int[] SelectedTag { get; set; }
        /// <summary>
        /// выбранная категория
        /// </summary>
        public int SelectedCategory { get; set; }
        /// <summary>
        /// список тэгов, которые 
        /// </summary>
        public List<TagModel> UnTags { get; set; }
        /// <summary>
        /// список категорий
        /// </summary>
        public List<CategoryModel> CategoriesList { get; set; }


        /// <summary>
        /// выбранные тэги
        /// </summary>
        public string STag { get; set; }
    }
}
