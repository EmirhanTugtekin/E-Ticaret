using E_Ticaret.Entity;
using System.Collections.Generic;

namespace E_Ticaret.Web.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Product> Products { get; set; }
    }
}
