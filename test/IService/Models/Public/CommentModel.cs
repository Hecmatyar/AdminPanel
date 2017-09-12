using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models.Public
{
    public class CommentModel
    {
        /// <summary>
        /// id комментария
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id комментария, на который писали этот комментарий
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// текст комментария
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// дата написания комментария
        /// </summary>
        public System.DateTime Published { get; set; }
        /// <summary>
        /// пользователь, оставивший комментарий
        /// </summary>
        public UserModel Author { get; set; }
        /// <summary>
        /// пост, к которому оставили комментарий
        /// </summary>
        public PostModel Post { get; set; }
    }
}
