using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        [StringLength(50,MinimumLength =3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }
        [ForeignKey("ParentCategory")]
        public int? ParentCategoryID { get; set; }
        //[ForeignKey("UserAdmin")]
        //public string AdminID { get; set; }

        public string CatName { get; set; }

        //NAVIGATION PROPERITY


        public virtual Category ParentCategory { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}