using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models.Moderator
{
    public class EditCreateCategoryModel
    {
        /// <summary>
        /// id категории
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// родительская категория
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// название категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// заголовок для адресной строки
        /// </summary>
        public string UrlName { get; set; }

        /// <summary>
        /// список всех категрий
        /// </summary>
        public List<CategoryModel> ListCategories { get; set; }
    }
}
