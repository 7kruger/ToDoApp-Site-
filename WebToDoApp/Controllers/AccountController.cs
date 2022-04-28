using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebToDoApp.Models;
using System.Web.Security;
using WebToDoApp.Models.Entities;
using WebToDoApp.Models.Data;
using System.Threading.Tasks;

namespace WebToDoApp.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.FirstOrDefault(u => u.Name == model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователя не существует");
                    return View(model);
                }
                if (user.Password != model.Password)
                {
                    ModelState.AddModelError("", "Неверный пароль");
                    return View(model);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = db.Users.FirstOrDefault(u => u.Name == model.UserName);
            if (user != null)
            {
                ModelState.AddModelError("", "Пользователь с таким ником уже существует");
                return View(model);
            }
            else
            {
                user = new User { Name = model.UserName, Password = model.Password };
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}