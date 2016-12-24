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
                sb.Append("<li class='well'>");
                sb.Append(User.Email);
                sb.Append("<button type='button' class='btn btn-danger' data-toggle='modal' data-target='#deleteModal'>Delete User</button>");
                sb.Append("<button type='button' class='btn btn-info' data-toggle='modal' data-target='#editModal'>Reset Password</button>");
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