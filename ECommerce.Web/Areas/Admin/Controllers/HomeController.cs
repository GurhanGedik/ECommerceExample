using ECommerce.Web.Areas.Admin.Models;
using ECommerce.Web.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        WorkContext workContext = new WorkContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "../Login");
            }
            return View();
        }

        public ActionResult Customer()
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "../Login");
            }

            CustomerModel model = new CustomerModel();
            model.LastName = customer.LastName;
            model.FirstName = customer.FirstName;


            return PartialView("_Customer", model);
        }
    }
}