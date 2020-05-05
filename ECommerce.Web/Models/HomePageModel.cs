using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Models
{
    public class HomePageModel
    {
        public HomePageModel()
        {
            Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
    }
}