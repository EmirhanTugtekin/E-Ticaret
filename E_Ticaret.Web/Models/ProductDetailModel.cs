﻿using E_Ticaret.Entity;
using System.Collections.Generic;

namespace E_Ticaret.Web.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
