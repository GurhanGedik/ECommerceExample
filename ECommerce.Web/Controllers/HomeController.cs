using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();
        // GET: Home
        public ActionResult Index()
        {
            HomePageModel model = new HomePageModel();
            model.Categories = db.Categories.Where(x => x.Deleted == false).ToList();

           


            return View(model);
        }
    }
}