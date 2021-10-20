using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;

namespace ECommerceApp.Controllers
{
   
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserProduct
        public ActionResult Index(int page=1)
        {

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            var product = new ProductsPaginationViewModel()
            {
                Products = db.Products.OrderBy(a => a.ID),
                PoductPerPage = 3,
                CurrentPage = page
            };
            if (page < 0)
            {
                page = 1;
                product.CurrentPage = 1;
               
            }
            if (page > product.PageCount())
            {
                page = product.PageCount();
                product.CurrentPage = product.PageCount();
            }
            
            return View(product);
        }
        public ActionResult Details(int? id)
        {
            var product = db.Products.FirstOrDefault(a => a.ID == id);
            var ProductsInSameCategory= db.Categories.Include(a => a.Products).FirstOrDefault(a => a.ID == product.CategoryID).Products;
            ViewBag.ProductsInSameCategory = ProductsInSameCategory;
            return View(product);
        }
        
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}