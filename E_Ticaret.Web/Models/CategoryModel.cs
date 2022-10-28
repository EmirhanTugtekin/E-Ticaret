using E_Ticaret.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Web.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Kategori için 5-100 arasında değer giriniz.")]
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Product> Products { get; set; }
    }
}
