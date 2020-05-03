using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Areas.Admin.Models
{
    public class CategoryListModel
    {
        public CategoryListModel()
        {
            Category = new List<CategoryModel>();
        }

        public List<CategoryModel> Category { get; set; }
    }
}