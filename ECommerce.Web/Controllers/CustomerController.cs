using ECommerce.Web.Lib;
using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class CustomerController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();
        WorkContext workContext = new WorkContext();

        public ActionResult Index()
        {
            return View();
        }

        #region Address
        public ActionResult AddressList()
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            AddressListModel model = new AddressListModel();
            var cam = db.CustomerAddressMappings.Where(x => x.CustomerId == customer.Id).ToList();
            foreach (var addressId in cam)
            {
                var address = db.Addresses.FirstOrDefault(x => x.Id == addressId.AddressId);
                AddressModel addressModel = new AddressModel();
                addressModel.Id = address.Id;
                addressModel.FirsName = address.FirsName;
                addressModel.LastName = address.LastName;
                addressModel.ProvinceId = address.ProvinceId;
                addressModel.CountiesId = address.CountiesId;
                addressModel.Address = address.Address1;
                addressModel.Email = address.Email;
                addressModel.Phone = address.Phone;
                addressModel.CreatedOnUtc = address.CreatedOnUtc;
                model.Address.Add(addressModel);
            }

            return View(model);
        }
        public ActionResult AddAddress()
        {
            var model = new AddressModel();

            var provinces = db.Provinces.ToList();
            var counties = db.Counties.ToList();

            model.AvailableProvinces = provinces.Select(x => new SelectListItem
            {
                Text = x.City,
                Value = x.id.ToString()
            }).ToList();

            model.AvailableCounties = counties.Where(x => x.City == 1).Select(x => new SelectListItem
            {
                Text = x.District,
                Value = x.id.ToString()
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAddress(AddressModel model)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            Address address = new Address();
            address.FirsName = model.FirsName;
            address.LastName = model.LastName;
            address.ProvinceId = model.ProvinceId;
            address.CountiesId = model.CountiesId;
            address.Email = model.Email;
            address.Phone = model.Phone;
            address.Address1 = model.Address;
            address.CreatedOnUtc = DateTime.Now;
            db.Addresses.Add(address);

            if (db.SaveChanges() > 0)
            {
                var addressId = db.Addresses.Max(x => x.Id);
                CustomerAddressMapping cam = new CustomerAddressMapping();
                cam.AddressId = addressId;
                cam.CustomerId = customer.Id;
                db.CustomerAddressMappings.Add(cam);
                db.SaveChanges();

            }

            return RedirectToAction("AddressList", "Customer");
        }
        public JsonResult CountiesId(int id)
        {
            var counties = db.Counties.Where(x => x.City == id).OrderBy(x => x.District).Select(
                x => new
                {
                    Text = x.District,
                    Value = x.id.ToString()
                }).ToList();

            return Json(counties, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddressEdit(int id)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            AddressModel model = new AddressModel();
            var address = db.Addresses.SingleOrDefault(x => x.Id == id);
            var provinces = db.Provinces.ToList();
            var counties = db.Counties.ToList();

            model.AvailableProvinces = provinces.Select(x => new SelectListItem
            {
                Text = x.City,
                Value = x.id.ToString()
            }).ToList();

            model.AvailableCounties = counties.Where(x => x.City == address.ProvinceId).Select(x => new SelectListItem
            {
                Text = x.District,
                Value = x.id.ToString()
            }).ToList();

            model.Id = address.Id;
            model.FirsName = address.FirsName;
            model.LastName = address.LastName;
            model.ProvinceId = address.ProvinceId;
            model.CountiesId = address.CountiesId;
            model.Email = address.Email;
            model.Phone = address.Phone;
            model.Address = address.Address1;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddressEdit(AddressModel model)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }
            Address address = db.Addresses.SingleOrDefault(x => x.Id == model.Id);
            address.FirsName = model.FirsName;
            address.LastName = model.LastName;
            address.ProvinceId = model.ProvinceId;
            address.CountiesId = model.CountiesId;
            address.Email = model.Email;
            address.Phone = model.Phone;
            address.Address1 = model.Address;
            db.SaveChanges();
            return RedirectToAction("AddressList", "Customer");
        }

        public ActionResult AddressDelete(int id)
        {
            Address deleted = db.Addresses.SingleOrDefault(x => x.Id == id);
            CustomerAddressMapping cam = db.CustomerAddressMappings.SingleOrDefault(x => x.AddressId == id);
            db.CustomerAddressMappings.Remove(cam);
            db.Addresses.Remove(deleted);
            db.SaveChanges();
            return RedirectToAction("AddressList", "Customer");
        }

        #endregion

    }
}