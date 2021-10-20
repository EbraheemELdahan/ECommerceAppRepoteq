using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{

    public class CategoriesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
    }
}