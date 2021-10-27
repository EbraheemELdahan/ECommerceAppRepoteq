using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult SearchHeader()
        {
            

            //ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "ID", "Name");
            return PartialView("SearchHolder",db.Categories.ToList());
         }
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.Include(a => a.SubCategories).ToList();
            var products = db.Products.ToList();
            return View(products);
        }
        [HttpPost]
        public ActionResult Index(string _prefix)
        {
            var productsNames = db.Products.Where(s => s.Name.Contains( _prefix)).Select(a=>new {a.Name });
            return Json(productsNames, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}