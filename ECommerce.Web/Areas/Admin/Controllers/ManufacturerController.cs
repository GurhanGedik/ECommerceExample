using ECommerce.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: Admin/Manufacturer
        private GurhanDbEntities db = new GurhanDbEntities();
        public ActionResult List()
        {
            ManufacturerListModel model = new ManufacturerListModel();
            var manufacturers = db.Manufacturers.Where(x => x.Deleted == false);
            foreach (var manufacturer in manufacturers)
            {
                var ManufacturerModel = new ManufacturerModel();
                ManufacturerModel.Id = manufacturer.Id;
                ManufacturerModel.Name = manufacturer.Name;
                ManufacturerModel.Published = manufacturer.Published;
                model.Manufacturer.Add(ManufacturerModel);
            }
            return View(model);
          
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ManufacturerModel manufacturer)
        {
            Manufacturer model = new Manufacturer();
            model.Name = manufacturer.Name;
            model.Published = manufacturer.Published;
            model.CreatedOnUtc = DateTime.Now;
            model.Deleted = false;
            db.Manufacturers.Add(model);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Edit(int ?id)
        {
            ManufacturerModel model = new ManufacturerModel();
            var manufacturer = db.Manufacturers.Find(id);
            model.Id = manufacturer.Id;
            model.Name = manufacturer.Name;
            model.Published = manufacturer.Published;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManufacturerModel model)
        {
            var c = db.Manufacturers.Find(model.Id);
            c.Id = model.Id;
            c.Name = model.Name;
            c.Published = model.Published;
            c.UpdatedOnUtc = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("List");
        }


        public ActionResult Delete(int? id)
        {
            var entity = db.Manufacturers.Find(id);
            entity.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}