using E_Ticaret.Business.Abstract;
using E_Ticaret.Entity;
using E_Ticaret.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        //httpget yazmaya gerek yok aslında. Ztn varsayılan olarak httpget
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductModel productModel)
        {
            var entity = new Product()
            {
                Name = productModel.Name,
                Url = productModel.Url,
                Price = productModel.Price, 
                Description = productModel.Description,
                ImageUrl=productModel.ImageUrl
            };

            _productService.Create(entity);

            //TempData["message"] = $"{entity.Name} isimli ürün başarıyla eklendi";
            /*TempData["message"] = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün başarıyla eklendi",
                AlertType = "success"
            };*/

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün eklendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }
        //httpget yazmaya gerek yok aslında. Ztn varsayılan olarak httpget
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = _productService.GetById((int)id);

            if (entity == null)
                return NotFound();

            var productModel = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                ImageUrl=entity.ImageUrl,
                Description = entity.Description
            };

            return View(productModel);
        }
        [HttpPost]
        public IActionResult Edit(ProductModel productModel)
        {
            var entity = _productService.GetById(productModel.ProductId);

            if (entity == null)
                return NotFound();

            entity.ProductId = productModel.ProductId;
            entity.Name = productModel.Name;
            entity.Url = productModel.Url;
            entity.Price = productModel.Price;
            entity.ImageUrl = productModel.ImageUrl;
            entity.Description = productModel.Description;

            _productService.Update(entity);

            //TempData["message"] = $"{entity.Name} isimli ürün başarıyla güncellendi";
            /*TempData["message"] = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün başarıyla güncellendi",
                AlertType = "success"
            };*/
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün güncellendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = _productService.GetById((int)id);

            if (entity == null)
                return NotFound();

            _productService.Delete(entity);

            //TempData["message"] = $"{entity.Name} isimli ürün başarıyla silindi";
            /*TempData["message"] = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün başarıyla silindi",
                AlertType = "danger"
            };*/
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün silindi.",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }
    }
}
