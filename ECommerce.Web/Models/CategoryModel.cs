using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            Products = new List<ProductModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }


        public List<ProductModel> Products { get; set; }
    }
}