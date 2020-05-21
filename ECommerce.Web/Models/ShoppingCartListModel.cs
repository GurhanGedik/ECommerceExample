using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models
{
    public class ShoppingCartListModel
    {
        public ShoppingCartListModel()
        {
            ShoppingCarts = new List<ShoppingCartModel>();
        }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalQuantity { get; set; }
        public List<ShoppingCartModel> ShoppingCarts { get; set; }
    }
}