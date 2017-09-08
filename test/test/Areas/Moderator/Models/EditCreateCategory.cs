using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Areas.Moderator.Models
{
    /// <summary>
    /// добавление, редактирование категорий
    /// </summary>
    public class EditCreateCategory
    {
        /// <summary>
        /// id категории
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// название категории
        /// </summary>
        public string Name { get; set; }
    }
}