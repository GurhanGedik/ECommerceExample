using ECommerce.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private GurhanDbEntities db = new GurhanDbEntities();
       
        // GET: Admin/Category
        public ActionResult List()
        {
            CategoryListModel model = new CategoryListModel();
            var categorys = db.Categories.Where(x => x.Deleted == false).ToList();
            foreach (var category in categorys)
            {
                CategoryModel cat = new CategoryModel();
                cat.Id = category.Id;
                cat.Name = category.Name;
                cat.Published = category.Published;

                model.Category.Add(cat);
            }
            return View(model);           
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryModel model)
        {
            Category category = new Category();
            category.Name = model.Name;
            category.Published = model.Published;
            category.CreatedOnUtc = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            CategoryModel model = new CategoryModel();
            model.Id = category.Id;
            model.Name = category.Name;
            model.Published = category.Published;
        
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryModel model)
        {
            Category Category = db.Categories.SingleOrDefault(x => x.Id == model.Id);
            Category.Name = model.Name;
            Category.Published = model.Published;
            Category.UpdatedOnUtc = DateTime.Now;
            db.SaveChanges();
           
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            Category deleted = db.Categories.SingleOrDefault(x => x.Id == id);
            deleted.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}