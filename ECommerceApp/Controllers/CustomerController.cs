using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using System.Data.Entity;


namespace ECommerceApp.Controllers
{
    [Authorize(Roles = "UserCustomer")]
    public class CustomerController : Controller
    {
      
        
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Orders()
        {
            
            return View(db.Orders.ToList());
        }
        public ActionResult OrderDetails(int id)
        {
            var order = db.Orders.Include(a=>a.OrderDetails).Include(a=>a.Shipped_Data).FirstOrDefault(a => a.ID == id);
            return View(order);
        }
        public new ActionResult Profile(string id)
        {
            var user = db.Users.First(a => a.Id == id);
            return View(user);
        }
       

    }
}