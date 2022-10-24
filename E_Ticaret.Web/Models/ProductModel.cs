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
        public string Name { get; set; }
        public string Url { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
