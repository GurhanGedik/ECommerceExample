using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Models
{
    public class AddressModel
    {
        public AddressModel()
        {
            AvailableProvinces = new List<SelectListItem>();
            AvailableCounties = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone  { get; set; }
        public string Address { get; set; }
        public Nullable<DateTime> CreatedOnUtc { get; set; }
        public int? ProvinceId { get; set; }
        public int? CountiesId { get; set; }

        public List<SelectListItem> AvailableProvinces { get; set; }
        public List<SelectListItem> AvailableCounties { get; set; }
    }
}