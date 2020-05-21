using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            string userFirstName = Request.Cookies["User"]["FirstName"];
            string userLastName = Request.Cookies["User"]["LastName"];

            if (userFirstName == null && userLastName == null)
            {
                return RedirectToAction("SingIn", "../Login");
            }
            return View();
        }
    }
}