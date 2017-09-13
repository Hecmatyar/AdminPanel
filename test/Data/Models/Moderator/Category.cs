using IService.Models;
using IService.Models.Moderator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Moderator
{
    /// <summary>
    /// категория в блоге
    /// </summary>
    public class Category
    {
        /// <summary>
        /// id категории
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// родительская категория
        /// </summary>
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        /// <summary>
        /// название категории
        /// </summary>
        [DisplayName("Категория")]
        public string Name { get; set; }
        /// <summary>
        /// имя для адресной строки
        /// </summary>
        public string UrlName { get; set; }
        /// <summary>
        /// список постов с данной ктаегорией
        /// </summary>
        public virtual List<Post> Posts { get; set; }
        public Category()
        {
            Posts = new List<Post>();
        }
        /// <summary>
        /// приведение к классу UserModel
        /// </summary>
        /// <param name="v">User, который надо привести к UserModel</param>
        public static explicit operator CategoryModel(Category v)
        {
            var category = new CategoryModel();

            category.Id = v.Id;
            category.ParentId = v.ParentId;
            if (v.Parent != null)
                category.ParentName = v.Parent.Name;
            category.Name = v.Name;
            category.UrlName = v.UrlName;

            return category;
        }
        /// <summary>
        /// приведение к классу UserModel
        /// </summary>
        /// <param name="v">User, который надо привести к UserModel</param>
        public static explicit operator EditCreateCategoryModel(Category v)
        {
            var category = new EditCreateCategoryModel();

            category.Id = v.Id;
            category.ParentId = v.ParentId;           
            category.Name = v.Name;
            category.UrlName = v.UrlName;

            return category;
        }
    }
}
