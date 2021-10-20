using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceApp.Models
{
    public class CartViewModel
    {
       public List<ProductsQuantityViewModel> ProductsQuantities { get; set; }
        public float TotalPrice { get; set; }
        public int TotalQuantities { get; set; }
    }
}