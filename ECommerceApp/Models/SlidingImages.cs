using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceApp.Models
{
    public class SlidingImages
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        //[ForeignKey("AdminUser")]
        public int AdminID { get; set; }

        //Navigation Properity

       
    }
}