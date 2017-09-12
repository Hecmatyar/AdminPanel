using Data.Models.Admin;
using Data.Models.Moderator;
using IService.Models;
using IService.Models.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Pubplic
{
    public class Comment
    {
        /// <summary>
        /// id комментария
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id комментария, на который писали этот комментарий
        /// </summary>
        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }
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
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        /// <summary>
        /// пост, к которому оставили комментарий
        /// </summary>
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        /// <summary>
        /// приведение к классу CommentModel
        /// </summary>
        /// <param name="v">Comment, который надо привести к CommentModel</param>
        public static explicit operator CommentModel(Comment v)
        {
            return new CommentModel
            {
                Id = v.Id,
                Body = v.Body,
                Published = v.Published,
                Author = (UserModel)v.Author,
                ParentId = v.ParentId
                //Post = (PostModel)v.Post
            };
        }
    }
}
