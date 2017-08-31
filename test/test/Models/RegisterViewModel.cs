using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public bool RememberMe { get; set; }
        [DisplayName("Дата рождения")]
        public DateTime UserBirth { get; set; }
        [DisplayName("Аватар")]
        public byte[] UserPhoto { get; set; }
    }
}