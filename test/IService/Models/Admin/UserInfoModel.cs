using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Models.Admin
{
    public class UserInfoModel
    {        
        /// <summary>
        /// файл с новой фотографией пользователя
        /// </summary>
        //public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// id пользователя
        /// </summary>
        public int UserId { get; set; }       
        /// <summary>
        /// имя пользователя
        /// </summary>       
        public string UserName { get; set; }        
        /// <summary>
        /// почтовый адрес пользователя
        /// </summary>      
        public string UserEmail { get; set; }       
        /// <summary>
        /// аватар пользователя
        /// </summary>      
        public byte[] UserPhoto { get; set; }
        /// <summary>
        /// новый аватар пользователя
        /// </summary>        
        public byte[] NewUserPhoto { get; set; }       
    }
}
