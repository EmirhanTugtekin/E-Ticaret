using E_Ticaret.Business.Abstract;
using E_Ticaret.Entity;
using E_Ticaret.Web.Models;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Ticaret.Web.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            this._productService = productService;
        }

        //localhost/products/telefon?page=1
        public IActionResult List(string category,int page=1)//int? page yaparsak hata alırız, int page=1 demek hiçbir page değeri girilmezse (localhost/products/ gibi) o zaman page'e varsayılan olarak 1 değeri atanır
        {
            const int pageSize = 2;//her sayfada 2 ürün gösterilir
            var productViewModel = new ProductListViewModel()
            {
                PageInfo=new PageInfo()
                {
                    TotalItems=_productService.GetCountByCategory(category),
                    CurrentPage=page,
                    ItemsPerPage=pageSize,
                    CurrentCategory=category
                },
                Products = _productService.GetProductsByCategory(category,page,pageSize)
            };

            return View(productViewModel);
        }
        public IActionResult Details(string url)
        {
            if (url == null)
            {
                return NotFound();
            }

            Product product = _productService.GetProductDetails(url);

            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailModel
            {
                Product = product,
                Categories = product.ProductCategories.Select(c => c.Category).ToList()
            });
        }
        public IActionResult Search(string q)
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetSearchResult(q)
            };
            return View(productViewModel);
        }
    }
}
