using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="This Field Is Required")]
        [StringLength(50,MinimumLength =3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        [StringLength(500)]
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public float Price { get; set; }
        public int? Rate { get; set; }
        public bool IsPopular { get; set; }
        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        [ForeignKey("Brand")]
        public int? BrandID { get; set; }
        //[ForeignKey("UserAdmin")]
        //public string AdminID { get; set; }

        //navigation properity
        public virtual Category Category { get; set; }
        
        public virtual List<OrderDetails> OrderDetails { get; set; }
        public virtual List<Favourites> Favourites { get; set; }
        public virtual Brand Brand { get; set; }

    }
}