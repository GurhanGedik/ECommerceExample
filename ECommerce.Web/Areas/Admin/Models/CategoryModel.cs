﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Areas.Admin.Models
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