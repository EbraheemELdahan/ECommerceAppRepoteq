using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceApp.Models
{
    public class ProductsPaginationViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PoductPerPage { get; set; }
        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Products.Count() / (double)PoductPerPage));
        }
        public IEnumerable<Product> PaginatedProducts()
        {
            int start = (CurrentPage - 1) * PoductPerPage;
            return Products.OrderBy(a => a.ID).Skip(start).Take(PoductPerPage);

        }
    }
}