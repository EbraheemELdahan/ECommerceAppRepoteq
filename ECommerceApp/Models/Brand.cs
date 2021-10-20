using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceApp.Models
{
    public class Brand
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        [StringLength(50,MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [StringLength(150,MinimumLength =5)]
        public string Description { get; set; }
        public string BrandImg { get; set; }
        //[ForeignKey("AdminUser")]
        public string AdminID { get; set; }

        //navigation properity

        public virtual List<Product> Products { get; set; }
    }
}