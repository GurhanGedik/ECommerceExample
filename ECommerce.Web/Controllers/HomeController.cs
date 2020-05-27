using ECommerce.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopMenu()
        {
            var categorys = db.Categories.Where(x => x.Deleted == false && x.Published == true).ToList();
            HomePageModel model = new HomePageModel();

            foreach (var category in categorys)
            {
                CategoryModel cat = new CategoryModel();
                cat.Id = category.Id;
                cat.Name = category.Name;
                model.Categories.Add(cat);

            }
            return PartialView("_TopMenu",model);
        }
    }
}