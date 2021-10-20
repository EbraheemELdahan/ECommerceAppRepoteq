using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;

using System.Data.Entity;


namespace ECommerceApp.Controllers
{
    
    public class UsersController : Controller
    {
       ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(s => s.Roles).ToList();


            return View(users);
        }
       public ActionResult Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            var user = db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Id == id);
            return View(user);
        }
        
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

            }

            var user = db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit(ApplicationUser user,HttpPostedFileBase UserImg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                string ImgUser = user.Id + "_" + user.FirstName + user.LastName + UserImg.FileName;
                UserImg.SaveAs(Server.MapPath("~/Images/UsersProfile/") + ImgUser);
                user.UserImg = ImgUser;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(user);
        }
        
        public ActionResult Delete(string id)
        {
            var user = db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Id == id);
            //foreach (var item in db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Id == id).Roles)
            //{
            //    item.UserId = null;
            //}
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("index");
        }
     
        
    }
}