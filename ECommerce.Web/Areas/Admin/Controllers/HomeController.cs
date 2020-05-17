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
            if (Session["FirstName"] == null && Session["LastName"] == null)
            {
                return RedirectToAction("SingIn", "../Login");
            }
            return View();
        }
    }
}