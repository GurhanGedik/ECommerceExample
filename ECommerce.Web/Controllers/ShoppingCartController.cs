using ECommerce.Web.Lib;
using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();
        WorkContext workContext = new WorkContext();

        public ActionResult CartList()
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            var cartList = db.ShoppingCartItems.Where(x => x.CustomerId == customer.Id).ToList();
            decimal? totalprice = 0;
            ShoppingCartListModel model = new ShoppingCartListModel();


            foreach (var item in cartList)
            {
                var product = db.Products.FirstOrDefault(x => x.Id == item.ProductId);

                var ShoppingCart = new ShoppingCartModel();
                ShoppingCart.Id = item.Id;
                ShoppingCart.Quantity = item.Quantity;
                ShoppingCart.ProductName = product.Name;
                ShoppingCart.ProductId = product.Id;
                ShoppingCart.Price = product.Price;
                ShoppingCart.Photo = product.Photo;
                ShoppingCart.CustomerId = item.CustomerId;
                totalprice += product.Price * item.Quantity;

                model.ShoppingCarts.Add(ShoppingCart);
            }
            model.TotalPrice = totalprice;
            model.TotalQuantity = cartList.Count;

            return PartialView("_ShoppingCart", model);

        }

        [HttpPost]
        public ActionResult AddProductToCart(int quantity, int productId)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                TempData["result"] = "Giriş Yapınız!";
                return RedirectToAction("ProductDetail", "Product", new { id = productId });
            }


            var ShoppingCarts = db.ShoppingCartItems.FirstOrDefault(x => x.CustomerId == customer.Id && x.ProductId == productId);

            if (ShoppingCarts == null)
            {
                ShoppingCartItem sci = new ShoppingCartItem();
                sci.CustomerId = customer.Id;
                sci.ProductId = productId;
                sci.Quantity = quantity;
                sci.CreatedOnUtc = DateTime.Now;

                db.ShoppingCartItems.Add(sci);
                if (db.SaveChanges() > 0)
                    TempData["result"] = "Sepete Eklendi.";
                else
                    TempData["result"] = "Ekleme işleminde sorun oluştu";
            }
            else
            {
                ShoppingCarts.Quantity += quantity;
                if (db.SaveChanges() > 0)
                    TempData["result"] = "Sepete Güncellendi.";
                else
                    TempData["result"] = "Sepet güncelleme işleminde sorun oluştu";
            }


            return RedirectToAction("ProductDetail", "Product", new { id = productId });
        }

        public ActionResult DeleteCart(int Id)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            var cart = db.ShoppingCartItems.SingleOrDefault(x => x.Id == Id && x.CustomerId == customer.Id);

            if (cart != null)
            {
                db.ShoppingCartItems.Remove(cart);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShoppingCart()
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            var cartList = db.ShoppingCartItems.Where(x => x.CustomerId == customer.Id).ToList();
            decimal? totalprice = 0;
            ShoppingCartListModel model = new ShoppingCartListModel();


            foreach (var item in cartList)
            {
                var product = db.Products.FirstOrDefault(x => x.Id == item.ProductId);

                var ShoppingCart = new ShoppingCartModel();
                ShoppingCart.Id = item.Id;
                ShoppingCart.Quantity = item.Quantity;
                ShoppingCart.ProductName = product.Name;
                ShoppingCart.ProductId = product.Id;
                ShoppingCart.Price = product.Price;
                ShoppingCart.Photo = product.Photo;
                ShoppingCart.CustomerId = item.CustomerId;
                totalprice += product.Price * item.Quantity;

                model.ShoppingCarts.Add(ShoppingCart);
            }
            model.TotalPrice = totalprice;
            model.TotalQuantity = cartList.Count;

            return View(model);
        }

        public ActionResult ProductMinus(int Id)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            var cart = db.ShoppingCartItems.SingleOrDefault(x => x.ProductId == Id && x.CustomerId == customer.Id);

            if (cart != null)
            {
                if (cart.Quantity <= 1)
                {
                    ShoppingCartItem deleted = cart;
                    db.ShoppingCartItems.Remove(deleted);
                    db.SaveChanges();
                    return RedirectToAction("ShoppingCart", "ShoppingCart");
                }
                cart.Quantity -= 1;
                db.SaveChanges();
            }

            return RedirectToAction("ShoppingCart", "ShoppingCart");
        }

        public ActionResult ProductPlus(int Id)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            var cart = db.ShoppingCartItems.SingleOrDefault(x => x.ProductId == Id && x.CustomerId == customer.Id);

            if (cart == null && customer.Id != 0)
            {
                ShoppingCartItem sci = new ShoppingCartItem();
                sci.CustomerId = customer.Id;
                sci.ProductId = Id;
                sci.Quantity = 1;
                sci.CreatedOnUtc = DateTime.Now;

                db.ShoppingCartItems.Add(sci);
                db.SaveChanges();


                return RedirectToAction("Index", "Home");
            }

            if (cart.CustomerId == customer.Id)
            {
                cart.Quantity += 1;
                db.SaveChanges();
            }

            return RedirectToAction("ShoppingCart", "ShoppingCart");
        }

        public ActionResult AddressCheck()
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }

            var cartList = db.ShoppingCartItems.Where(x => x.CustomerId == customer.Id).ToList();
            AddressCheckModel model = new AddressCheckModel();

            #region ShoppingCart

            decimal? totalprice = 0;
            foreach (var item in cartList)
            {
                var product = db.Products.FirstOrDefault(x => x.Id == item.ProductId);

                var ShoppingCart = new ShoppingCartModel();
                ShoppingCart.Id = item.Id;
                ShoppingCart.Quantity = item.Quantity;
                ShoppingCart.ProductName = product.Name;
                ShoppingCart.ProductId = product.Id;
                ShoppingCart.Price = product.Price;
                ShoppingCart.Photo = product.Photo;
                ShoppingCart.CustomerId = item.CustomerId;
                totalprice += product.Price * item.Quantity;

                model.ShoppingCarts.Add(ShoppingCart);
            }
            model.TotalPrice = totalprice;
            model.TotalQuantity = cartList.Count;

            #endregion

            #region Address
            var cam = db.CustomerAddressMappings.Where(x => x.CustomerId == customer.Id).ToList();
            foreach (var addressId in cam)
            {
                var address = db.Addresses.FirstOrDefault(x => x.Id == addressId.AddressId);

                model.AvailableAddress.Add(new SelectListItem { Text = address.FirsName, Value = address.Id.ToString() });
                
            }
            #endregion

            #region Cargo
            model.AvailableCargo.Add(new SelectListItem { Text = "Aras Kargo", Value = "Aras Kargo" });
            model.AvailableCargo.Add(new SelectListItem { Text = "Yurtiçi Kargo", Value = "Yurtiçi Kargo" });
            model.AvailableCargo.Add(new SelectListItem { Text = "MNG Kargo", Value = "MNG Kargo" });
            model.AvailableCargo.Add(new SelectListItem { Text = "UPS Kargo", Value = "UPS Kargo" });
            #endregion



            return View(model);
        }
    }
}