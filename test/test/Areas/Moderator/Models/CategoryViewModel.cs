using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Areas.Moderator.Models
{
    /// <summary>
    /// модель категорий
    /// </summary>
    public class CategoryViewModel
    {       
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
        /// тэги в ленте
        /// </summary>
        public List<CategoryModel> Categories { get; set; }
    }
}