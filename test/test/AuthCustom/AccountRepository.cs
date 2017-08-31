using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Controllers;
using IService;
using System.Web.Mvc;

namespace test.AuthCustom
{
    public class AccountRepository
    {
        protected IDataBaseRereqiest _DataBaseRereqiest;
        public AccountRepository()
        {           
            _DataBaseRereqiest = DependencyResolver.Current.GetService<IDataBaseRereqiest>();
        }

        public User GetUserByID(int id)
        { 
            /* 
             * ????? 
             */
            return new User() { Id = id,
                UserName = _DataBaseRereqiest.GetUserById(id).UserName,
                userroles = (UserRoles)_DataBaseRereqiest.GetUserById(id).userroles,
                UserEmail = _DataBaseRereqiest.GetUserById(id).UserEmail,
                UserToken = _DataBaseRereqiest.GetUserById(id).UserToken                
            };
        }
    }
}