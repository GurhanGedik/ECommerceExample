using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Areas.Admin.Models
{
    public class CustomerListModel
    {
        public CustomerListModel()
        {
            Customers = new List<CustomerModel>();
        }
        public List<CustomerModel> Customers { get; set; }
    }
}