using ECommerce.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private GurhanDbEntities db = new GurhanDbEntities();

        // GET: Admin/Login
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
                Session.Add("FirstName", User.FirstName);
                Session.Add("LastName", User.LastName);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Bilgiler Hatalı";
            //test
            return View();
        }

        public ActionResult SingOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}