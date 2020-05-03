using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Areas.Admin.Models
{
    public class ManufacturerListModel
    {
        public ManufacturerListModel()
        {
            Manufacturer = new List<ManufacturerModel>();
        }
        public List<ManufacturerModel> Manufacturer { get; set; }
        
    }
}