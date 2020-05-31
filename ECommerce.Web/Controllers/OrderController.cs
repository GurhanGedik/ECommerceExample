using ECommerce.Web.Lib;
using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class OrderController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();
        WorkContext workContext = new WorkContext();

        public ActionResult Create(AddressCheckModel model)
        {
            var customer = workContext.GetAuthenticatedCustomer();
            if (customer == null)
            {
                return RedirectToAction("SingIn", "Login");
            }


            #region ShoppingCart
            var cartList = db.ShoppingCartItems.Where(x => x.CustomerId == customer.Id).ToList();

            decimal? totalprice = 0;
            foreach (var item in cartList)
            {
                var product = db.Products.FirstOrDefault(x => x.Id == item.ProductId);

                var ShoppingCart = new ShoppingCartModel();
                ShoppingCart.Id = item.Id;
                ShoppingCart.Quantity = item.Quantity;
                ShoppingCart.ProductId = product.Id;
                ShoppingCart.Price = product.Price;
                ShoppingCart.CustomerId = item.CustomerId;
                totalprice += product.Price * item.Quantity;

                model.ShoppingCarts.Add(ShoppingCart);
            }
            #endregion

            #region OrderCerate
            Order order = new Order();
            order.CustomerId = customer.Id;
            order.AddressId = model.AddressId;
            order.ShippingMethod = model.Cargo;
            order.OrderStatusId = (int)OrderStatusEnum.Preparing;
            order.ShippingStatusId = (int)ShippingStatusEnum.NotYetShipped;
            order.OrderTotal = totalprice;
            order.Deleted = false;
            order.CreatedOnUtc = DateTime.Now;
            db.Orders.Add(order);
            if (db.SaveChanges() > 0)
            {
                foreach (var product in model.ShoppingCarts)
                {
                    OrderItem oi = new OrderItem();
                    oi.OrderId = order.Id;
                    oi.ProductId = product.ProductId;
                    oi.Quantity = product.Quantity;
                    oi.Price = product.Price;
                    db.OrderItems.Add(oi);
                    if (db.SaveChanges() > 0)
                    {
                        var cart = db.ShoppingCartItems.SingleOrDefault(x => x.Id == product.Id && x.CustomerId == customer.Id);

                        if (cart != null)
                        {
                            db.ShoppingCartItems.Remove(cart);
                            db.SaveChanges();
                        }
                    }
                }


            }
            #endregion

            return RedirectToAction("Index", "Home");
        }
    }
}