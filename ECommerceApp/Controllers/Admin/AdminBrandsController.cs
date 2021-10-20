using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using System.IO;

namespace ECommerceApp.Controllers.Admin
{
    //[Authorize(Roles ="Admin")]
    public class AdminBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminBrands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: AdminBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: AdminBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,BrandImg,AdminID")] Brand brand,HttpPostedFileBase BrandImg)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                string brandImgURL = brand.ID + "_" + BrandImg.FileName;
                BrandImg.SaveAs(Server.MapPath("~/Images/BrandsImages/") + brandImgURL);
                brand.BrandImg = brandImgURL;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: AdminBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: AdminBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,BrandImg,AdminID")] Brand brand,HttpPostedFileBase BrandImg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;

                db.SaveChanges();
                FileInfo fi = new FileInfo(brand.BrandImg);
                fi.Delete();
                string brandImgURL = brand.ID + "_" + BrandImg.FileName;
                BrandImg.SaveAs(Server.MapPath("~/Images/BrandsImages/") + brandImgURL);
                brand.BrandImg = brandImgURL;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: AdminBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: AdminBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            
           var ProductsInBrand= db.Products.Where(a => a.BrandID==id).ToList();
            foreach (var item in ProductsInBrand)
            {
                item.BrandID = null;
            }
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
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
