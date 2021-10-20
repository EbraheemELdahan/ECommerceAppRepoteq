using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            return View(admin);

        }
    }
}