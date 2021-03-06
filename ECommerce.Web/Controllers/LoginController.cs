﻿using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerce.Web.Controllers
{
    public class LoginController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();
        // GET: Login
        public ActionResult Index()
        {
            return RedirectToAction("SingIn");
        }

        public ActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingIn(LoginModel model)
        {
            var User = db.Customers.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Pasword && x.Active == true);

            if (User != null)
            {
                FormsAuthentication.SetAuthCookie(User.Email, true);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Bilgiler Hatalı";
            return View();
        }

        public ActionResult SingOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}