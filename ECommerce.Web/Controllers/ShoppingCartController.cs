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

        public ActionResult CartList()
        {
            var cartList = db.ShoppingCartItems.ToList();
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
            string user = String.Empty;
            if (Request.Cookies["User"] != null)
            {
                user = Request.Cookies["User"].Value;
            }
            else
            {
                TempData["result"] = "Giriş Yapınız!";

                return RedirectToAction("ProductDetail", "Product", new { id = productId });
            }

            string userId = Request.Cookies["User"]["UserId"];
            int CustomerId = Convert.ToInt32(userId);

            var ShoppingCarts = db.ShoppingCartItems.FirstOrDefault(x => x.CustomerId == CustomerId && x.ProductId == productId);

            if (ShoppingCarts == null)
            {
                ShoppingCartItem sci = new ShoppingCartItem();
                sci.CustomerId = CustomerId;
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
            //kullanıcı kontrolü yapılacak
            ShoppingCartItem deleted = db.ShoppingCartItems.SingleOrDefault(x => x.Id == Id);
            db.ShoppingCartItems.Remove(deleted);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}