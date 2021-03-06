﻿using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    /// <summary>
    /// отображение постов
    /// </summary>
    public class Posts
    {
        /// <summary>
        /// выбранный тэг
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// выбранная категория
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// строка поиска
        /// </summary>
        public string q { get; set; }
        /// <summary>
        /// страница 
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// количество страниц
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// посты в ленте
        /// </summary>
        public List<PostModel> PostsList { get; set; }
        /// <summary>
        /// выбранный пост из ленты
        /// </summary>
        public PostModel CurrentPosts { get; set; }
    }
}