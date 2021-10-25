using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Models
{
    public class Shipped_Data
    {
        
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public String BuildingNumber { get; set; }
        [Key]
        [ForeignKey("User")]
        public string UserID { get; set; }

        //Navigation properity

        public ApplicationUser User { get; set; }
        //public virtual Order Order { get; set; }
    }
}