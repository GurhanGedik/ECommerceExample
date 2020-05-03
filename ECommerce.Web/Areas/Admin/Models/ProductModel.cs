using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Areas.Admin.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableManufacturers = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public string Sku { get; set; }
        public decimal? Price { get; set; }
        public string CategoryName { get; set; }
        public string ManufactureName { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufactureId { get; set; }
        public bool Active { get; set; }

        public List<SelectListItem> AvailableCategories { get; set; }
        public List<SelectListItem> AvailableManufacturers { get; set; }

    }
}