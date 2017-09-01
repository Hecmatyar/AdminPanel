using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    class Category
    {
        public int Id { get; set; }
        [DisplayName("Категория")]
        public string Name { get; set; }       
        public virtual List<Post> Posts { get; set; }
        public Category()
        {
            Posts = new List<Post>();
        }
    }
}
