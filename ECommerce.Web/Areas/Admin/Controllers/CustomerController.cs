using ECommerce.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private GurhanDbEntities db = new GurhanDbEntities();

        // GET: Admin/Category
        public ActionResult List()
        {
            CustomerListModel model = new CustomerListModel();
            var customers = db.Customers.Where(x=>x.Deleted == false).ToList();
            foreach (var customer in customers)
            {
                var customerModel = new CustomerModel();
                customerModel.Id = customer.Id;
                customerModel.FirstName = customer.FirstName;
                customerModel.LastName = customer.LastName;
                customerModel.Password = customer.Password;
                customerModel.Email = customer.Email;
                customerModel.Active = customer.Active;


                model.Customers.Add(customerModel);
            }
            

             return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CustomerModel model)
        {
            Customer c = new Customer();
            c.FirstName = model.FirstName;
            c.LastName = model.LastName;
            c.Email = model.Email;
            c.Active = model.Active;
            c.CreatedOnUtc = DateTime.Now;
            c.Password = model.Password;

            db.Customers.Add(c);
            db.SaveChanges();
            return RedirectToAction("List");
        }


        public ActionResult Edit(int id)
        {
            CustomerModel model = new CustomerModel();
            var customer = db.Customers.SingleOrDefault(x => x.Id == id);

            model.Id = customer.Id;
            model.FirstName = customer.FirstName;
            model.LastName = customer.LastName;
            model.Email = customer.Email;
            model.Password = customer.Password;
            model.Active = customer.Active;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel model)
        {
            Customer customer = db.Customers.SingleOrDefault(x => x.Id == model.Id);
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Password = model.Password;
            customer.Email = model.Email;
            customer.Active = model.Active;
            db.SaveChanges();


            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            Customer deleted = db.Customers.SingleOrDefault(x => x.Id == id);
            deleted.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}