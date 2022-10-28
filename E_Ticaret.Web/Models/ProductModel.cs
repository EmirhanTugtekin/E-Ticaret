using E_Ticaret.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Ticaret.Web.Models
{
    public class ProductModel //formlar için oluşturduğumuz model'lerle entity'ler için oluşturduğumuz sınıfları birbirinden ayırmak daha doğru. Çünkü belki entity'deki bazı property'leri kullanmayacağız
    {
        public int ProductId { get; set; }

        [Display(Name = "Name", Prompt = "Enter product name")]
        [Required(ErrorMessage = "Name zorunlu bir alan.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Ürün ismi 5-10 karakter aralığında olmalıdır.")]
        public string Name { get; set; }

        public string Url { get; set; }

        [Required(ErrorMessage = "Price zorunlu bir alan.")]
        [Range(1, 100000, ErrorMessage = "Price için 1-100000 arasında değer girmelisiniz.")]
        public double? Price { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "ImageUrl zorunlu bir alan.")]
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
