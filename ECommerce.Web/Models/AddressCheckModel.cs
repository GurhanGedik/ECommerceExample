using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Models
{
    public class AddressCheckModel
    {
        public AddressCheckModel()
        {
            ShoppingCarts = new List<ShoppingCartModel>();
            AvailableCargo = new List<SelectListItem>();
            AvailableAddress = new List<SelectListItem>();
        }

        public decimal? TotalPrice { get; set; }
        public decimal? TotalQuantity { get; set; }
        public string Cargo { get; set; }
        public int AddressId { get; set; }
        public List<ShoppingCartModel> ShoppingCarts { get; set; }
        public List<SelectListItem> AvailableCargo { get; set; }
        public List<SelectListItem> AvailableAddress { get; set; }
    }
}