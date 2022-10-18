using E_Ticaret.Business.Abstract;
using E_Ticaret.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Web.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult ProductList()
        {
            
            return View(new ProductListViewModel
            {
                Products=_productService.GetAll()
            });
        }
    }
}
