using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Areas.Admin.Models
{
    public class ProductListModel
    {
        public ProductListModel()
        {
            Products = new List<ProductModel>();
        }
        public List<ProductModel> Products { get; set; }
    }
}