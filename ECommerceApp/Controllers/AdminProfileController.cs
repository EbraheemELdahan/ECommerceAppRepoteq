using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    public class AdminProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminProfile
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                id = "3063038f-1f9d-4d02-9001-6bc43f87b8ab";
            }
            if(id== "3063038f-1f9d-4d02-9001-6bc43f87b8ab")
            {
                var admin=db.Users.SingleOrDefault(a => a.Id == id);
                return View(admin);
            }
            return View("~/Views/Account/Login.cshtml");
        }
    }
}