﻿using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        GurhanDbEntities db = new GurhanDbEntities();
        // GET: Home
        public ActionResult Index()
        {
            var categorys = db.Categories.Where(x => x.Deleted == false).ToList();
            HomePageModel model = new HomePageModel();

            foreach (var category in categorys)
            {
                CategoryModel cat = new CategoryModel();
                cat.Id = category.Id;
                cat.Name = category.Name;
                model.Categories.Add(cat);
                
            }
            return View(model);
        }
    }
}