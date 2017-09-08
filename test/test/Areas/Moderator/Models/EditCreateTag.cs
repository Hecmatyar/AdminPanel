using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Areas.Moderator.Models
{
    /// <summary>
    /// модель редактивраония тэга
    /// </summary>
    public class EditCreateTag
    {
        /// <summary>
        /// id тэга
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// название тэга
        /// </summary>
        public string Name { get; set; }
    }
}