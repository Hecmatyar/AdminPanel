﻿using Data.Models.Admin;
using Data.Models.Pubplic;
using IService.Models;
using IService.Models.Moderator;
using IService.Models.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Moderator
{
    /// <summary>
    /// модель постов в блоге
    /// </summary>
    public class Post
    {
        /// <summary>
        /// id поста
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// заголовок поста
        /// </summary>
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        /// <summary>
        /// заголовок для адресной строки
        /// </summary>
        public string UrlTitle { get; set; }
        /// <summary>
        /// краткое описание поста
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// полный текст поста
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// дата публикации поста
        /// </summary>
        [DisplayName("Опубликовано")]
        public DateTime Published { get; set; }
        /// <summary>
        /// автор публикации поста
        /// </summary>
        [DisplayName("Автор")]
        public int UserId { get; set; }
        public virtual User Author { get; set; }
        /// <summary>
        /// категория, к которой относится пост
        /// </summary>
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        /// <summary>
        /// список тэгов поста
        /// </summary>
        public virtual List<Tag> Tags { get; set; }
        /// <summary>
        /// комментарии к посту
        /// </summary>
        //public virtual List<Comment> Comments { get; set; }
        public Post()
        {
            Tags = new List<Tag>();
            //Comments = new List<Comment>();
        }

        /// <summary>
        /// приведение к классу UserModel
        /// </summary>
        /// <param name="v">User, который надо привести к UserModel</param>
        public static explicit operator PostModel(Post v)
        {
            var tags = v.Tags.Select(a => (TagModel)a).ToList();
            //var comments = v.Comments.Select(a => (CommentModel)a).ToList();
            return new PostModel
            {
                Id = v.Id,
                Title = v.Title,
                UrlTitle = v.UrlTitle,
                ShortDescription = v.ShortDescription,
                Description = v.Description,
                Published = v.Published,
                Author = (UserModel)v.Author,
                Category = (CategoryModel)v.Category,
                Tags = tags
                //Comments = comments
            };
        }
        /// <summary>
        /// приведение к классу UserModel
        /// </summary>
        /// <param name="v">User, который надо привести к UserModel</param>
        public static explicit operator EditCreatePostModel(Post v)
        {
            var tags = v.Tags.Select(a => (TagModel)a).ToList();
            return new EditCreatePostModel
            {
                Id = v.Id,
                Title = v.Title,
                UrlTitle = v.UrlTitle,
                ShortDescription = v.ShortDescription,
                Description = v.Description,
                Category = (CategoryModel)v.Category,
                Tags = tags
            };
        }
    }
}
