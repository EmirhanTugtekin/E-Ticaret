using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
