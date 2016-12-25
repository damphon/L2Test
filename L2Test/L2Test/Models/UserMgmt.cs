using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L2Test.Helpers;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

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
                string jsfixD = '"' + "deleteUser('" + User.UserName + "' , '" + User.Id + "')" + '"'; //Added this because otherwise stringbuilder cannot format the variable in a way that Javascript can accept.
                string jsfixR = '"' + "editUser('" + User.UserName + "' , '" + User.Id + "')" + '"';
                StringBuilder sb = new StringBuilder(UserString);
                sb.Append("<li>");
                sb.Append(User.Email);
                sb.AppendFormat("<button type='button' class='btn btn-danger' onclick={0}>Delete User</button>", jsfixD);
                sb.AppendFormat("<button type='button' class='btn btn-info' onclick={0}>Reset Password</button>", jsfixR);
                sb.Append("</li>");
                UserString = sb.ToString();
            }
            return UserString;
        }

        public static void Delete(string key)
        {
            using (var db = new IdentityDbContext())
            {
                var user = db.Users.Find(key);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public static void PaswordUpdate(string key, string newPassword)
        {
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            userManager.RemovePassword(key);
            userManager.AddPassword(key, newPassword);
        }
    }
}