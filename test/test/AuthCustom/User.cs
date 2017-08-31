using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.AuthCustom
{    
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public bool UserConfirmedEmail { get; set; }
        public string UserToken { get; set; }
        public UserRoles userroles { get; set; }
        
        //public virtual ICollection<UserRoles> userRoles { get; set; }
        //public Post()
        //{
        //    userRoles = new List<UserRoles>();
        //}
        internal bool IsInRole(UserRoles accessTole)
        {
            //return userRoles.Contains(accessTole) ? true : false;
            return userroles == accessTole ? true : false;
        }
    }
}