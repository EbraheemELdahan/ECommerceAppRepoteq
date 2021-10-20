using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceApp.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        
        [StringLength(150)]
        public string Notes { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public float? Tax { get; set; }
        public float TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        [ForeignKey("Shipped_Data")]
        public string ShippedData { get; set; }

        //Navigation PROPERITY
       
        public virtual Shipped_Data Shipped_Data { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}