using E_Ticaret.Business.Abstract;
using E_Ticaret.Entity;
using E_Ticaret.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace E_Ticaret.Web.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public string urlChanger(string valueToChange)//bu metod türkçe karakterleri ve boşlukları kaldırır
        {
            /*
           if(valueToChange.Contains(" ")|| valueToChange.Contains("ı") || valueToChange.Contains("ü") || valueToChange.Contains("ö")|| valueToChange.Contains("ç") || valueToChange.Contains("ş") || valueToChange.Contains("İ") || valueToChange.Contains("ş") ||)
           {
               for (int i = 0; i < valueToChange.Length; i++)
               {
                   if (valueToChange[i] == " ")
               }
           }*/

            if (new[] { " ", "ç", "Ç", "ğ", "Ğ", "İ", "ı", "Ö", "ö", "Ş", "ş", "ü", "Ü" }.Any(c => valueToChange.Contains(c)))
            {
                valueToChange = valueToChange.Replace(" ", "-");
                valueToChange = valueToChange.Replace("ç", "c");
                valueToChange = valueToChange.Replace("Ç", "C");
                valueToChange = valueToChange.Replace("ğ", "g");
                valueToChange = valueToChange.Replace("Ğ", "G");
                valueToChange = valueToChange.Replace("İ", "I");
                valueToChange = valueToChange.Replace("ı", "i");
                valueToChange = valueToChange.Replace("Ö", "O");
                valueToChange = valueToChange.Replace("ö", "o");
                valueToChange = valueToChange.Replace("Ş", "S");
                valueToChange = valueToChange.Replace("ş", "ş");
                valueToChange = valueToChange.Replace("Ü", "U");
                valueToChange = valueToChange.Replace("ü", "u");
            }
            return valueToChange;
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
            //productModel.Url = urlChanger(productModel.Name);

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

            var entity = _productService.GetByIdWithCategories((int)id);

            if (entity == null)
                return NotFound();

            var productModel = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                SelectedCategories = entity.ProductCategories.Select(x => x.Category).ToList()   
            };

            ViewBag.AllCategories = _categoryService.GetAll();

            return View(productModel);
        }
        [HttpPost]
        public IActionResult Edit(ProductModel productModel)
        { 
            productModel.Url = urlChanger(productModel.Name);

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

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün silindi.",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }

        //--------------------------------------------------------

        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel categoryModel)
        {
            categoryModel.Url = urlChanger(categoryModel.Name);

            var entity = new Category()
            {
                CategoryId = categoryModel.CategoryId,
                Name = categoryModel.Name,
                Url = categoryModel.Url
            };

            _categoryService.Create(entity);

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli kategori eklendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = _categoryService.GetByIdWithProducts((int)id);

            if (entity == null)
                return NotFound();

            var categoryModel = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(x => x.Product).ToList()
            };

            return View(categoryModel);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel categoryModel)
        {
            categoryModel.Url = urlChanger(categoryModel.Name);

            var entity = _categoryService.GetById(categoryModel.CategoryId);

            if (entity == null)
                return NotFound();

            entity.CategoryId = categoryModel.CategoryId;
            entity.Name = categoryModel.Name;
            entity.Url = categoryModel.Url;

            _categoryService.Update(entity);

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli kategori güncellendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
                return NotFound();

            var entity=_categoryService.GetById((int)id);

            if (entity == null)
                return NotFound();

            _categoryService.Delete(entity);

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli kategori silindi.",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteFromCategory(int productId,int CategoryId)
        {
            _categoryService.DeleteFromCategory(productId,CategoryId);
            return Redirect("/admin/EditCategory/" + CategoryId);
        }
    }
}
