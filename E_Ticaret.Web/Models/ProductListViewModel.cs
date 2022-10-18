using E_Ticaret.Entity;
using System;
using System.Collections.Generic;

namespace E_Ticaret.Web.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems * ItemsPerPage);
        }        //eğer veritabanında 10 ürün varsa 10/3=3.3 den 4 ürün (yuvarlama) sayfa başına gösterilsin istiyoruz
    }
    public class ProductListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> Products { get; set; }
    }
}
