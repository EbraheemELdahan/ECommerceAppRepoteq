using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ECommerceApp.Models
{
    public class Favourites
    {
        [Key]
        [Column(Order =0)]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        //Navigation Properity

       
        public virtual Product Product { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}