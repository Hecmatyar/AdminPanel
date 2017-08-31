using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.AuthCustom
{
    public interface IAuthenticationService
    {
        void Login(User user, bool rememberMe);
        void Logoff();
        string GeneratePassword(string pass, string salt);
        User CurrentUser { get; }
    }
}