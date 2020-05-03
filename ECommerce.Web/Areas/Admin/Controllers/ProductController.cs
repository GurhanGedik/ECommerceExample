using ECommerce.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        private GurhanDbEntities db = new GurhanDbEntities();
        public ActionResult List()
        {
            var products = db.Products.Where(x => x.Deleted == false).ToList();
            var categorys = db.Categories.ToList();
            var manufactures = db.Manufacturers.ToList();

            ProductListModel model = new ProductListModel();

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
                foreach (var category in categorys.Where(x => x.Id == product.CategoryId))
                {
                    productModel.CategoryName = category.Name;
                }
                foreach (var manufacture in manufactures.Where(x => x.Id == product.ManufacturerId))
                {
                    productModel.ManufactureName = manufacture.Name;
                }

                model.Products.Add(productModel);
            }

            return View(model);
        }

        public ActionResult Add()
        {
            List<SelectListItem> catList =
                (from i in db.Categories.Where(x => x.Deleted == false).ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString()
                 }).ToList();
            ViewBag.CategoryList = catList;


            List<SelectListItem> manufactureList =
                (from i in db.Manufacturers.Where(x => x.Deleted == false).ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString()
                 }).ToList();
            ViewBag.ManufacturerList = manufactureList;



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductModel model)
        {
            Product p = new Product();
            p.Name = model.Name;
            p.Price = model.Price;
            p.Sku = model.Sku;
            p.Active = model.Active;
            p.Barcode = model.Barcode;
            p.CategoryId = model.CategoryId;
            p.ManufacturerId = model.ManufactureId;
            p.CreatedOnUtc = DateTime.Now;
            p.Description = model.Description;

            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }


        public ActionResult Edit(int id)
        {
            ProductModel model = new ProductModel();
            var product = db.Products.SingleOrDefault(x => x.Id == id);

            List<SelectListItem> catList =
                (from i in db.Categories.Where(x => x.Deleted == false).ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString(),
                     Selected = i.Id == product.CategoryId ? true : false
                 }).ToList();
            ViewBag.CategoryList = catList;


            List<SelectListItem> manufactureList =
                (from i in db.Manufacturers.Where(x => x.Deleted == false).ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString(),
                     Selected = i.Id == product.ManufacturerId ? true : false
                 }).ToList();
            ViewBag.ManufacturerList = manufactureList;



            model.Id = product.Id;
            model.ManufactureId = product.ManufacturerId;
            model.Name = product.Name;
            model.Price = product.Price;
            model.Sku = product.Sku;
            model.Active = product.Active;
            model.Barcode = product.Barcode;
            model.CategoryId = product.CategoryId;
            model.Description = product.Description;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel model)
        {
            Product product = db.Products.SingleOrDefault(x => x.Id == model.Id);
            product.ManufacturerId = model.ManufactureId;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Sku = model.Sku;
            product.Active = model.Active;
            product.Barcode = model.Barcode;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;

            db.SaveChanges();


            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            Product product = db.Products.SingleOrDefault(x => x.Id == id);
            product.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("List");
        }



    }
}