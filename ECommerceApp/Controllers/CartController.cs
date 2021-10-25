using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
//using System.Web.Script.Serialization;

namespace ECommerceApp.Controllers
{

    //public class CartController : Controller
    //{
    //    ApplicationDbContext db = new ApplicationDbContext();
    //    // GET: Cart
    //    public ActionResult Index()
    //    {
    //        ViewBag.order = Session["order"] as CartViewModel;
    //        return View();
    //    }
    //    //Buy
    //    [HttpPost]
    //    public ActionResult checkout(Shipped_Data shipped_Data)
    //    {
    //        int count = 0;
    //        string userID = User.Identity.GetUserId();
    //        var CurrentCustomer = db.Users.FirstOrDefault(a => a.Id == userID);
    //        CartViewModel cart = Session["order"] as CartViewModel; //for changing quantity after
    //        DateTime orderdate = DateTime.Now;
    //        var shippedData = new Shipped_Data()
    //        {
    //            UserID = userID,
    //            Country = shipped_Data.Country,
    //            City = shipped_Data.City,
    //            BuildingNumber = shipped_Data.BuildingNumber,
    //            Street = shipped_Data.Street
    //        };
    //        db.Shipped_Datas.Add(shippedData);
    //        db.SaveChanges();
    //        Order order = new Order()
    //        {
    //            UserID = userID,
    //            OrderDate = orderdate,
    //            OrderNumber = count++,
    //            Status = OrderStatus.Waiting,
    //            ShippedData = userID
    //        };// Tax will be determined later by admin
    //        db.Orders.Add(order);
    //        db.SaveChanges();
    //        foreach (var item in cart.ProductsQuantities)
    //        {
    //            for (int i = 0; i < item.Quantity; i++)
    //            {
    //                OrderDetails orderdetails = new OrderDetails()
    //                {
    //                    ProductID = item.Product.ID,
    //                    UnitPrice = item.Product.Price,
    //                    OrderID = order.ID,
    //                    Discount = (float)((item.Product.Price) * .05)
    //                };
    //                db.OrderDetails.Add(orderdetails);

    //            }
    //        }
    //        db.SaveChanges();
    //        Session.Remove("order");
    //        return RedirectToAction("Orders", "Customer");
    //    }

    //    [HttpGet]
    //    public ActionResult AddtoCart(int id, int QuantityArrow)
    //    {
    //        //if ((!User.Identity.IsAuthenticated || !User.IsInRole("UserCustomer")))
    //        //{
    //        ////    if (Request.IsAjaxRequest())
    //        ////        return Content(Url.Action("Login", "Account"));
    //        ////    else
    //        //        return RedirectToAction("Login", "Account");
    //        //}
    //        CartViewModel order;
    //        var product = db.Products.FirstOrDefault(a => a.ID == id);
    //        if (Session["order"] != null)
    //        {//int x=0;
    //            order = Session["order"] as CartViewModel;
    //            int index = IsAddedToCart(id);
    //            if (index == -1)
    //            {

    //                order.ProductsQuantities.Add(new ProductsQuantityViewModel() { Product = product, Quantity = QuantityArrow });
    //                //  x = order.ProductsQuantities.IndexOf(new ProductsQuantityViewModel() { Product = product, Quantity = QuantityArrow });
    //            }
    //            else
    //            {
    //                order.ProductsQuantities[index].Quantity += QuantityArrow;
    //                //order.ProductsQuantities.Add(new ProductsQuantityViewModel() { Product = product, Quantity = 1 });

    //            }
    //            order.TotalPrice += product.Price * QuantityArrow;
    //            //order.TotalQuantities++;

    //        }
    //        else
    //        {
    //            order = new CartViewModel();
    //            order.TotalQuantities = QuantityArrow;
    //            order.TotalPrice = product.Price * order.TotalQuantities;
    //            order.ProductsQuantities = new List<ProductsQuantityViewModel>();
    //            order.ProductsQuantities.Add(new ProductsQuantityViewModel() { Product = product, Quantity = QuantityArrow });
    //            //for (int i = 0; i < order.ProductsQuantities.Count; i++)
    //            //{
    //            //    order.TotalPrice += order.ProductsQuantities[i].Quantity * product.Price;
    //            //}
    //            //order.TotalPrice = order.ProductsQuantities[0].Quantity *product.Price;
    //        }
    //        order.TotalPrice = (float)(Math.Round(order.TotalPrice, 2));
    //        Session["order"] = order;

    //        return RedirectToAction("Index", "cart");
    //    }

    //    public int IsAddedToCart(int productId)
    //    {
    //        CartViewModel cart = Session["order"] as CartViewModel;
    //        var Products = cart.ProductsQuantities;
    //        for (int i = 0; i < Products.Count; i++)

    //            if (Products[i].Product.ID == productId)
    //                return i;
    //        return -1;
    //    }

    //    public ActionResult RemoveFromCart(int id)
    //    {

    //        CartViewModel cart = Session["order"] as CartViewModel;
    //        ProductsQuantityViewModel product = cart.ProductsQuantities.Where(a => a.Product.ID == id).FirstOrDefault();
    //        cart.TotalQuantities -= product.Quantity;
    //        cart.TotalPrice = cart.TotalPrice - (product.Quantity * product.Product.Price);
    //        if (product.Quantity == 1)
    //        {


    //            cart.ProductsQuantities.Remove(product);
    //        }
    //        product.Quantity--;
    //        Session["order"] = cart;
    //        return RedirectToAction("index");
    //    }
    //}




    public class CartController : Controller
    {
        //HttpCookie orderCookie6 = ("ordersCookie4");


        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            var orderCook = Request.Cookies.Get("MyCookieValue13")["myvalues"];
            CartViewModel cart = JsonConvert.DeserializeObject<CartViewModel>(orderCook)as CartViewModel;
           
            ViewBag.order = cart;
            return View();
        }
        //Buy
        [HttpPost]
        public ActionResult checkout(Shipped_Data shipped_Data)
        {
            
            
            string userID = User.Identity.GetUserId();
            var CurrentCustomer = db.Users.FirstOrDefault(a => a.Id == userID);
            //CartViewModel cart = Session["order"] as CartViewModel; //for changing quantity after
            var orderCook = Request.Cookies.Get("MyCookieValue13")["myvalues"];
            CartViewModel cart = JsonConvert.DeserializeObject<CartViewModel>(orderCook) as CartViewModel;


            DateTime orderdate = DateTime.Now;
            var sh=db.Shipped_Datas.ToList();
            
            for (int i = 0; i < sh.Count; i++)
            {
               
                    if (sh[i].UserID == userID)
                {
                    db.Shipped_Datas.Remove(sh[i]);
                    foreach (var item in db.Orders.ToList())
                    {
                        if (item.ShippedData == sh[i].UserID)
                        item.ShippedData = null;
                    }
                   
                  
                   
                    
                    db.SaveChanges();
                }
            }
            var shippedData = new Shipped_Data()
            {
                UserID = userID,
                Country = shipped_Data.Country,
                City = shipped_Data.City,
                BuildingNumber = shipped_Data.BuildingNumber,
                Street = shipped_Data.Street
            };
            db.Shipped_Datas.Add(shippedData);
           
            db.SaveChanges();
            Order order = new Order()
            {
                UserID = userID,
                OrderDate = orderdate,
                
                Status = OrderStatus.Waiting,
                ShippedData = userID,
                TotalPrice=cart.TotalPrice
            };// Tax will be determined later by admin
            db.Orders.Add(order);
            db.SaveChanges();
            order.OrderNumber = order.ID;
            db.SaveChanges();
            foreach (var item in cart.ProductsQuantities)
            {
                //for (int i = 0; i < item.Quantity-1; i++)
               // {
                    
                        OrderDetails orderdetails = new OrderDetails()
                        {
                            ProductID = item.Product.ID,
                            UnitPrice = item.Product.Price,
                            OrderID = order.ID,
                            Discount = (float)((item.Product.Price) * .05),
                            Quantity = item.Quantity,
                            TotalPrice = item.Product.Price * item.Quantity
                        };                    
                        db.OrderDetails.Add(orderdetails);
                    
                //}
            }
            db.SaveChanges();
            
            return RedirectToAction("Orders", "Customer");
        }

        [HttpGet]
        public ActionResult AddtoCart(int id, int QuantityArrow)

        {
           

            var product = db.Products.FirstOrDefault(a => a.ID == id);
            Product product2 = new Product()
            { ID = product.ID, Name = product.Name, ImageURL = product.ImageURL, Price = product.Price, Description = product.Description };
            CartViewModel order = new CartViewModel();
            if (Request.Cookies["MyCookieValue13"] != null)
            {
                var cc = Request.Cookies.Get("MyCookieValue13")["myvalues"];
                order = JsonConvert.DeserializeObject<CartViewModel>(cc);
               
                // order=  Session["order"]as CartViewModel;
                int index = IsAddedToCart(id);
                if (index == -1)
                {
                    product2.Price = product2.Price * QuantityArrow;
                    order.ProductsQuantities.Add(new ProductsQuantityViewModel() { Product = product2, Quantity = QuantityArrow });
                    order.TotalPrice += product2.Price * QuantityArrow;

                }
                else
                {
                    order.ProductsQuantities[index].Quantity += QuantityArrow;
                    //product2.Price += product2.Price * QuantityArrow;
                    order.TotalPrice += product2.Price* QuantityArrow;
                }
                //order.TotalPrice += product2.Price;
                order.TotalPrice = (float)(Math.Round(order.TotalPrice, 2));
                string myCookieVal = JsonConvert.SerializeObject(order, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                
                HttpCookie OrderCookieVal = Request.Cookies.Get("MyCookieValue13");
                OrderCookieVal["myvalues"] = myCookieVal;
                HttpContext.Response.Cookies.Add(OrderCookieVal);
                // return Json(OrderCookieVal["myvalues"], JsonRequestBehavior.AllowGet);
                return RedirectToAction("index");
            }
            else
            {
                order = new CartViewModel();
                order.TotalQuantities = QuantityArrow;
                order.TotalPrice = product2.Price *QuantityArrow;
                order.ProductsQuantities = new List<ProductsQuantityViewModel>();
                order.ProductsQuantities.Add(new ProductsQuantityViewModel() { Product = product2 as Product, Quantity = QuantityArrow });
                order.TotalPrice = (float)(Math.Round(order.TotalPrice, 2));
                string myCookieVal=JsonConvert.SerializeObject(order, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                
               // var myCookieVal = new DataContractJsonSerializer(typeof(CartViewModel));
                
                HttpCookie OrderCookieVal = new HttpCookie("MyCookieValue13");
                OrderCookieVal["myvalues"] = myCookieVal.ToString();
                HttpContext.Response.Cookies.Add(OrderCookieVal);
                // return Json(OrderCookieVal["myvalues"], JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }

           
        }

        public int IsAddedToCart(int productId)
        {

            var orderCookie6 = Request.Cookies.Get("MyCookieValue13")["myvalues"];
            var cart = JsonConvert.DeserializeObject<CartViewModel>(orderCookie6);

            
            var Products = cart.ProductsQuantities;
            for (int i = 0; i < Products.Count; i++)

                if (Products[i].Product.ID == productId)
                    return i;
            return -1;
        }

        public ActionResult RemoveFromCart(int id)
        {



            //var jsonSerializer = new JavaScriptSerializer();
            var orderCookie6 = Request.Cookies.Get("MyCookieValue13")["myvalues"];

            // CartViewModel cart = Session["order"] as CartViewModel;
            var cart= JsonConvert.DeserializeObject<CartViewModel>(orderCookie6);

            

            ProductsQuantityViewModel product = cart.ProductsQuantities.Where(a => a.Product.ID == id).FirstOrDefault();
            product.Quantity--;
            cart.TotalQuantities --;
            cart.TotalPrice = (product.Quantity * product.Product.Price);
            if (product.Quantity == 1)
            {
                cart.ProductsQuantities.Remove(product);
            }
            //product.Quantity--;
            CartViewModel order = new CartViewModel()
            {
                ProductsQuantities=cart.ProductsQuantities,
                TotalPrice=cart.TotalPrice,
                TotalQuantities=cart.TotalQuantities
            };
            string myCookieVal = JsonConvert.SerializeObject(order, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            HttpCookie OrderCookieVal = Request.Cookies.Get("MyCookieValue13");
            OrderCookieVal["myvalues"] = myCookieVal;
            HttpContext.Response.Cookies.Add(OrderCookieVal);
            //return Json(OrderCookieVal["myvalues"], JsonRequestBehavior.AllowGet);
            return RedirectToAction("index");
        }
    }


}