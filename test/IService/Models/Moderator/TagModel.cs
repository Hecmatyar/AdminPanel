using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models
{
    public class TagModel
    {
        /// <summary>
        /// id тэга
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя тэга
        /// </summary>
        [DisplayName("Тэг")]
        public string Name { get; set; }        
    }
}
