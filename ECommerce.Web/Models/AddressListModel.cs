using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models
{
    public class AddressListModel
    {
        public AddressListModel()
        {
            Address = new List<AddressModel>();
        }

        public List<AddressModel> Address { get; set; }
    }
}