using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L2Test.Helpers;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;

namespace L2Test.Models
{
    public class UserMgmt
    {
        public string UserList()
        {
            var context = new IdentityDbContext();
            var List = context.Users.ToList();
            string UserString = "";
            foreach (var User in List)
            {
                StringBuilder sb = new StringBuilder(UserString);
                sb.Append("<li>");
                sb.Append(User.Email);
                sb.Append("</li>");
                UserString = sb.ToString();
            }
            return UserString;
        }

        public static void Delete(string key)
        {
            //Delete account
        }

        public static void PaswordUpdate(string key)
        {
            //Change Password
        }
    }
}