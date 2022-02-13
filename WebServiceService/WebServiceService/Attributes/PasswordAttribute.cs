using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceService.PasswordAttributes
{
    public class PasswordAttribute : Attribute
    {
        public string Password { get; set; }

        public PasswordAttribute(string password)
        {
            this.Password = password;
        }
    }
}