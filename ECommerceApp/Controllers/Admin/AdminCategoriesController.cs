using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using PagedList;
using System.IO;
using System.Data.Entity;

namespace ECommerceApp.Controllers.Admin
{
    //[Authorize(Roles ="Admin")]
    public class AdminCategoriesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminCategories
        public ActionResult Index(int? page,string SortOrder)
        {
            var categories = db.Categories.Include(a=>a.ParentCategory).ToList();
            ViewBag.CurrentSort = SortOrder;
            ViewBag.NameSortParam =String.IsNullOrEmpty(SortOrder)? "name_desc":"";
            ViewBag.ProductsCountParam = SortOrder == "countProducts" ? "Prod_desc" : "countProducts";
            switch (SortOrder)
            {
                case "name_desc":
                    categories = db.Categories.OrderBy(a => a.Name).ToList();
                    break;
                case "countProducts":
                    categories = db.Categories.OrderBy(a => a.Products.Count).ToList();
                    break;
                case "Prod_desc":
                    categories = db.Categories.OrderByDescending(a => a.Products.Count).ToList();
                    break;
                default:
                    categories = db.Categories.OrderBy(a => a.ID).ToList();
                    break;



            }

            var pageNumber = page ?? 1;
            var CategoriesPerPage = categories.ToPagedList(pageNumber,3);
            ViewBag.CategoriesPerPage = CategoriesPerPage;
            return View(CategoriesPerPage);
        }
        //Get
        public ActionResult Create()
        {
            ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(a=>a.ParentCategoryID==null).ToList(),"ID","Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category,HttpPostedFileBase CatName)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                string NewImgUrl = category.ID+ "." + CatName.FileName.Split('.')[1];
                CatName.SaveAs(Server.MapPath("~/Images/CategoriesImages/" )+ NewImgUrl);
                category.CatName = NewImgUrl;
                db.SaveChanges();
                return RedirectToAction("index", "AdminCategories");
            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(a=>a.ParentCategoryID==null).ToList(), "ID", "Name");

            return View(category);
        }
        public ActionResult Details(int id)
        {
            return View(db.Categories.Include("Products").FirstOrDefault(a => a.ID == id));
        }
        public ActionResult edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var category = db.Categories.FirstOrDefault(a => a.ID == id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit (Category Newcategory,HttpPostedFileBase NewCatName)
        {
            if (ModelState.IsValid)
            {
                Category OldCategory = db.Categories.FirstOrDefault(a => a.ID == Newcategory.ID);
                OldCategory.Name = Newcategory.Name;
                OldCategory.Description = Newcategory.Description;
                db.SaveChanges();
                FileInfo fi = new FileInfo(Server.MapPath("~/Images/CategoriesImages/" )+ OldCategory.CatName);
                fi.Delete();
                string NewCategoryImgName = OldCategory.ID + "_" + NewCatName.FileName;
                NewCatName.SaveAs(Server.MapPath("~/Images/CategoriesImages/" )+ NewCategoryImgName);
                
                
               

                //db.Entry(category).State = System.Data.Entity.EntityState.Modified
                OldCategory.CatName = NewCategoryImgName;
                db.SaveChanges();
                
                
                
                return RedirectToAction("index");

            }
            return View(Newcategory);
        }
        public ActionResult delete (int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(db.Categories.FirstOrDefault(a => a.ID == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult delete(Category category)
        {
            var deletedCat = db.Categories.FirstOrDefault(a => a.ID == category.ID);
            db.Categories.Remove(deletedCat);
            foreach (var item in db.Products)
            {
                if (item.CategoryID == deletedCat.ID)
                {
                    item.CategoryID=null;
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
    
}