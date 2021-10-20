using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceApp.Models
{
    public class OrderDetails
    {
        [Key]
        public int ID { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        
        public float? Discount { get; set; }
        public float TotalPrice { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        //Navigation Properity

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}