using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TodoList_v1.lib.Entity;
using TodoList_v1.Models;

namespace TodoList_v1.Controllers
{
    public class AuthController : Controller
    {
        private dbTodoListEntities dbContext;

        public AuthController()
        {
            dbContext = new dbTodoListEntities();
        }

        public ViewResult Login()
        {
            return View();
        }

        public ViewResult Register()
        {
            return View();
        }

        public RedirectResult Exit()
        {
            if (Session["id"] != null)
                Session.Clear();

            return Redirect("/Auth/Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public RedirectResult Register (RegisterViewModel model)
        {
            tbUsers user = new tbUsers();
            user.cLogin = model.Login;
            user.cPassword = ComputeMD5(model.Password);
            dbContext.tbUsers.Add(user);
            dbContext.SaveChanges();

            return Redirect("/Auth/Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public RedirectResult Login (LoginViewModel model)
        {
            string passwd = ComputeMD5(model.Password);          
            var dbres = dbContext.tbUsers.FirstOrDefault(l => (l.cLogin == model.Login) && (l.cPassword == passwd));
            if (dbres != null)
            {
                Session["id"] = dbres.id;
                Session["name"] = dbres.cLogin;
            }

            return Redirect("/Lists/Index");
        }   

        private string ComputeMD5(string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(pass));
            string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
            return result;
        }
    }
}