using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models
{
    public class CategoryModel
    {
        /// <summary>
        /// id категории
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// родительская категория
        /// </summary>
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        /// <summary>
        /// название категории
        /// </summary>
        [DisplayName("Категория")]
        public string Name { get; set; }

        /// <summary>
        /// заголовок для адресной строки
        /// </summary>
        public string UrlName { get; set; }        
    }
}
