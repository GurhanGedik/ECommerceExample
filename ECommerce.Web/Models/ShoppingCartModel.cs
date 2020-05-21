using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        #region Product
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string Photo { get; set; }
        #endregion
    }
}