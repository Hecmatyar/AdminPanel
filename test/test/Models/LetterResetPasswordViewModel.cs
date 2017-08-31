using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class LetterResetPasswordViewModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserActivationLink { get; set; }
    }
}