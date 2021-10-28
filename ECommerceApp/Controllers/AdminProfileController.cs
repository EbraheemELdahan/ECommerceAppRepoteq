using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ECommerceApp.Controllers
{
    public class AdminProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //private readonly UserManager<IdentityUser> _userManager;
        //public AdminProfileController(UserManager<IdentityUser> userManager)
        //{
        //    _userManager = userManager;
        //}
        public ActionResult Index()
        {

            // get userid from context
            string id = User.Identity.GetUserId();
            var admin = db.Users.SingleOrDefault(a => a.Id == id);
            var wishlists = db.WishLists.Include(a=>a.Product).Where(a => a.userid == id);
            var productsInWishLists = new List<Product>();
            foreach (var item in wishlists)
            {
                productsInWishLists.Add(item.Product);
            }
            ViewBag.wishlists = productsInWishLists;
            return View(admin);


        }
    }
}