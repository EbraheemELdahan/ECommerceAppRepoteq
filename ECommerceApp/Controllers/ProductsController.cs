using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;

namespace ECommerceApp.Controllers
{
   
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserProduct
        public ActionResult Index(int page=1)
        {
            

            ViewBag.Categories = db.Categories.Include(a=>a.SubCategories).Where(a=>a.ParentCategoryID==null).ToList();
            ViewBag.Brands = db.Brands.ToList();
        //    db.Categories.FirstOrDefault(a => a.ID == 1).SubCategories;
           
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
        public ActionResult detailsByName(string name)
        {
            var product = db.Products.FirstOrDefault(a => a.Name ==name);
            var ProductsInSameCategory = db.Categories.Include(a => a.Products).FirstOrDefault(a => a.ID == product.CategoryID).Products;
            ViewBag.ProductsInSameCategory = ProductsInSameCategory;
            return View(product);
        }
        public ActionResult Details(int? id)
        {
            var product = db.Products.FirstOrDefault(a => a.ID == id);
            var ProductsInSameCategory= db.Categories.Where(a=>a.ParentCategoryID!=null).Include(a=>a.SubCategories).Include(a => a.Products).FirstOrDefault(a => a.ID == product.CategoryID).Products;
            ViewBag.ProductsInSameCategory = ProductsInSameCategory;
            return View(product);
        }
        public ActionResult WishList(int id)
        {
            var product = db.Products.FirstOrDefault(a => a.ID == id);
            string user = User.Identity.GetUserId();
            var wishlist = new WishList() { productID = id, userid = user };
            db.WishLists.Add(wishlist);
            db.SaveChanges();
            return RedirectToAction("index","adminprofile");
        }
        
       public ActionResult ProductsInCategory(int id ,int page=1)
        {
            ViewBag.Category = db.Categories.SingleOrDefault(a => a.ID == id);
           List< Product> products = db.Products.Where(a => a.CategoryID == id).ToList();
            ViewBag.Categories = db.Categories.Include(a => a.SubCategories).Where(a => a.ParentCategoryID == null).ToList();
            ViewBag.Brands = db.Brands.ToList();
            //    db.Categories.FirstOrDefault(a => a.ID == 1).SubCategories;

            var product = new ProductsPaginationViewModel()
            {
                Products = products.OrderBy(a => a.ID),
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

            return View(product); ;
        }
        public ActionResult ProductsInBrand(int id,int page=1)
        {
            ViewBag.Brand = db.Brands.SingleOrDefault(a => a.ID == id);
            List<Product> products = db.Products.Where(a => a.BrandID == id).ToList();


            ViewBag.Categories = db.Categories.Include(a => a.SubCategories).Where(a => a.ParentCategoryID == null).ToList();
            ViewBag.Brands = db.Brands.ToList();
            //    db.Categories.FirstOrDefault(a => a.ID == 1).SubCategories;

            var product = new ProductsPaginationViewModel()
            {
                Products = products.OrderBy(a => a.ID),
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

            return View(product); ;
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