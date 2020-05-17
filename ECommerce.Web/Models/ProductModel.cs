using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public string Sku { get; set; }
        public decimal? Price { get; set; }
        public string CategoryName { get; set; }
        public string ManufactureName { get; set; }
        public string Photo { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufactureId { get; set; }
        public bool Active { get; set; }
    }
}