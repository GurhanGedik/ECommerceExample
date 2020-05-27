using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();

        // GET: Category
        public ActionResult CategoryDetail(int id)
        {
            CategoryModel model = new CategoryModel();
            var products = db.Products.Where(x => x.CategoryId == id && x.Active == true && x.Deleted == false).ToList();

            foreach (var product in products)
            {
                var productModel = new ProductModel();
                productModel.Id = product.Id;
                productModel.Name = product.Name;
                productModel.Price = product.Price;
                productModel.Sku = product.Sku;
                productModel.Description = product.Description;
                productModel.Active = product.Active;
                productModel.Barcode = product.Barcode;
                productModel.CategoryName = product.Category.Name;
                productModel.ManufactureName = product.Manufacturer.Name;
                productModel.Photo = product.Photo;

                model.Products.Add(productModel);

            }

            return View(model);
        }
    }
}