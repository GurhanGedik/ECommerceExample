using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ProductDetail(int id)
        {
            ProductModel model = new ProductModel();
            var product = db.Products.Where(x => x.Id == id && x.Active == true && x.Deleted == false).FirstOrDefault();

            if (product == null)
            {
                return RedirectToAction("Index","Home");
            }

            model.Id = product.Id;
            model.Name = product.Name;
            model.Price = product.Price;
            model.Sku = product.Sku;
            model.Description = product.Description;
            model.Active = product.Active;
            model.Barcode = product.Barcode;
            model.CategoryName = product.Category.Name;
            model.ManufactureName = product.Manufacturer.Name;
            model.Photo = product.Photo;

            return View(model);
        }
    }
}