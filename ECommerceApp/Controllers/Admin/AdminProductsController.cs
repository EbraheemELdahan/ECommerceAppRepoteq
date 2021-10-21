using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using PagedList;
using System.Data.Entity;
using System.Net;
using PagedList.Mvc;
using System.IO;

namespace ECommerceApp.Controllers.Admin
{
    //[Authorize(Roles ="Admin")]
    public class AdminProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminProducts
        public ActionResult Index(int? page, string SortOrder)
        {


            var products = db.Products.Include(p => p.Brand).Include(p => p.Category);

            ViewBag.CurrentSort = SortOrder;
            ViewBag.IDSort = String.IsNullOrEmpty(SortOrder) ? "IDSort" : "";
            ViewBag.RateSort = SortOrder == "RateSort" ? "RateSort" : "";
            ViewBag.PriceSort = SortOrder == "PriceSort" ? "PriceSort" : "";

            switch (SortOrder)
            {
                case "IDSort":
                    products = products.OrderBy(a => a.ID);
                    break;
                case "RateSort":
                    products = products.OrderBy(a => a.Rate);
                    break;
                case "PriceSort":
                    products = products.OrderBy(a => a.Price);
                    break;
                default:
                    products = products.OrderBy(a => a.ID);
                    break;
            }


            var pageNumber = page ?? 1;
            var ProductsPerPage = products.ToPagedList(pageNumber, 3);
            ViewBag.ProductsPerPage = ProductsPerPage;

            return View(ProductsPerPage.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            var product = db.Products.FirstOrDefault(a => a.ID == id);
            return View(product);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "ID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories.Where(a=>a.ParentCategoryID!=null).ToList(), "ID", "Name");

            ViewBag.BrandID = new SelectList(db.Brands.ToList(), "ID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                string NewImageURL = product.ID + "." + ImageURL.FileName.Split('.')[1];
                ImageURL.SaveAs(Server.MapPath("~/Images/ProductsImages/") + NewImageURL);
                product.ImageURL = NewImageURL;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            //ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "ID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories.Where(a=>a.ParentCategoryID!=null).ToList(), "ID", "Name");

            ViewBag.BrandID = new SelectList(db.Brands.ToList(), "ID", "Name");
            return View(product);

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            var product = db.Products.SingleOrDefault(a => a.ID == id);
            //ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "ID", "Name");
            
            ViewBag.CategoryID = new SelectList(db.Categories.Where(a => a.ParentCategoryID != null).ToList(), "ID", "Name");

            ViewBag.BrandID = new SelectList(db.Brands.ToList(), "ID", "Name");
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                FileInfo fi = new FileInfo(Server.MapPath("~/Images/ProductsImages/" + product.ImageURL));
                fi.Delete();
                string NewImageURL = product.ID + "_" + ImageURL.FileName;
                ImageURL.SaveAs(Server.MapPath("~/Images/ProductsImages/" + NewImageURL));
                product.ImageURL = NewImageURL;
                db.SaveChanges();
                return RedirectToAction("index");

            }
            ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "ID", "Name");
            ViewBag.BrandID = new SelectList(db.Brands.ToList(), "ID", "Name");
            return View(product);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Product product = db.Products.Include(a => a.Category).Include(a => a.Brand).SingleOrDefault(a => a.ID == id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            var Deletedproduct = db.Products.Single(a => a.ID == product.ID);
            db.Products.Remove(Deletedproduct);
            db.SaveChanges();
            return RedirectToAction("index");
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