using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using Microsoft.AspNet.Identity;

namespace ECommerceApp.Controllers
{
    public class AdminProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminProfile
        public ActionResult Index()
        {
            if (User.Identity.GetUserId()!=null)
            {
                string id = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(a => a.Id ==id);
                return View(user);
            }
            else
            {
                return View("~/Account/Login.cshtml");
            }
           
        }
    }
}