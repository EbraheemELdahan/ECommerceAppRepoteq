using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Models
{
    public class WishList
    {
        [Key]
        [Column(Order =0)]
        [ForeignKey("ApplicationUser")]
        public String userid { get; set; }
        [Key]
        [Column(Order =1)]
        [ForeignKey("Product")]
        public int productID { get; set; }
        public Product Product { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}