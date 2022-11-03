using E_Ticaret.Business.Abstract;
using E_Ticaret.Entity;
using E_Ticaret.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using E_Ticaret.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using E_Ticaret.Web.Identity;
using System.Collections.Generic;

namespace E_Ticaret.Web.Controllers
{
    [Authorize(Roles ="Admin")]  
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IProductService productService, ICategoryService categoryService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
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
                valueToChange = valueToChange.Replace("ş", "s");
                valueToChange = valueToChange.Replace("Ü", "U");
                valueToChange = valueToChange.Replace("ü", "u");
            }
            return valueToChange;
        }

        #region User Methods
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = allRoles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }

            return Redirect("/admin/UserList/");
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model,string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result=await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                        return Redirect("/admin/userlist/");
                    }
                }
                
            }
            return View(model); 
        }

        #endregion

        #region Role Methods

        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<AppUser>();
            var nonMembers = new List<AppUser>();

            foreach (var user in _userManager.Users.ToList())
            {
                #region basit yöntem
                //if (await _userManager.IsInRoleAsync(user, role.Name))
                //{
                //    members.Add(user);
                //}
                //else
                //{
                //    nonMembers.Add(user);
                //}
                #endregion

                #region daha profesyonel yöntem
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
                #endregion
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(model);
        }
        /*list, değer ve referans tiplerden referans tiptir. Burada eğer true gelirse, item members list'ine değilse nonMembers list'ine atılır*/
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return Redirect("/Admin/EditRole/" + model.RoleId);
        }
        #endregion

        #region Product Methods
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
            if (ModelState.IsValid)
            {
                if(productModel.Name!=null)
                    productModel.Url = urlChanger(productModel.Name);

                var entity = new Product()
                {
                    Name = productModel.Name,
                    Url = productModel.Url,
                    Price = productModel.Price,
                    Description = productModel.Description,
                    ImageUrl = productModel.ImageUrl
                };

                #region AlertMessage işlemlerinin ilk ve ikinci hali 

                //TempData["message"] = $"{entity.Name} isimli ürün başarıyla eklendi";

                //TempData["message"] = new AlertMessage()
                //{
                //    Message = $"{entity.Name} isimli ürün başarıyla eklendi",
                //    AlertType = "success"
                //};

                //var msg = new AlertMessage()
                //{
                //    Message = $"{entity.Name} isimli ürün eklendi.",
                //    AlertType = "success"
                //};

                //TempData["message"] = JsonConvert.SerializeObject(msg);

                #endregion

                if (_productService.Create(entity))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "kayıt eklendi",
                        Message = "kayıt eklendi",
                        AlertType = "success"
                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "hata",
                    Message = "hata",
                    AlertType = "danger"
                });

                return View(productModel);
            }
            return View(productModel);
        }
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
                IsHome = entity.IsHome,
                IsApproved = entity.IsApproved,
                SelectedCategories = entity.ProductCategories.Select(x => x.Category).ToList()   
            };

            ViewBag.Categories = _categoryService.GetAll();

            return View(productModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel productModel, int[] categoryIds, IFormFile file)
        {
            
            if (ModelState.IsValid)
            {
                productModel.Url = urlChanger(productModel.Name);

                var entity = _productService.GetById(productModel.ProductId);           

                if (entity == null)
                    return NotFound();

                //entity.ProductId = productModel.ProductId;
                entity.Name = productModel.Name;
                entity.Url = productModel.Url;
                entity.Price = productModel.Price;
                entity.ImageUrl = productModel.ImageUrl;
                entity.Description = productModel.Description;
                entity.IsHome = productModel.IsHome;
                entity.IsApproved = productModel.IsApproved;

                if (file != null)
                {
                    var extention = Path.GetExtension(file.FileName);//uzantısını aldım (.jpg .png vs.)
                    var randomName = string.Format($"{Guid.NewGuid()}{extention}");//Guid.NewGuid() ile rastgele bir isim üretip extension ile uzantısını yazdım
                    entity.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _productService.Update(entity,categoryIds);

                #region AlertMessage işlemleri eski hali
                ////TempData["message"] = $"{entity.Name} isimli ürün başarıyla güncellendi";
                ///*TempData["message"] = new AlertMessage()
                //{
                //    Message = $"{entity.Name} isimli ürün başarıyla güncellendi",
                //    AlertType = "success"
                //};*/
                //var msg = new AlertMessage()
                //{
                //    Message = $"{entity.Name} isimli ürün güncellendi.",
                //    AlertType = "success"
                //};

                //TempData["message"] = JsonConvert.SerializeObject(msg);
                #endregion

                if (_productService.Update(entity, categoryIds))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "kayıt güncellendi",
                        Message = "kayıt güncellendi",
                        AlertType = "success"
                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "hata",
                    Message = "hata",
                    AlertType = "danger"
                });

                return RedirectToAction("ProductList");
            }
            
            ViewBag.Categories = _categoryService.GetAll();
            return View(productModel);
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

        #endregion

        #region Category Methods
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
            if (ModelState.IsValid)
            {
                if(categoryModel.Name!=null)
                    categoryModel.Url = urlChanger(categoryModel.Name);

                var entity = new Category()
                {
                    CategoryId = categoryModel.CategoryId,
                    Name = categoryModel.Name,
                    Url = categoryModel.Url
                };

                _categoryService.Create(entity);

                #region AlertMessage işlemleri eski hali
                //var msg = new AlertMessage()
                //{
                //    Message = $"{entity.Name} isimli kategori eklendi.",
                //    AlertType = "success"
                //};

                //TempData["message"] = JsonConvert.SerializeObject(msg);
                #endregion

                TempData.Put("message", new AlertMessage()
                {
                    Title = "kayıt eklendi.",
                    Message = $"{entity.Name} isimli category eklendi.",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }
            return View(categoryModel);
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
            if (ModelState.IsValid)
            {
                categoryModel.Url = urlChanger(categoryModel.Name);

                var entity = _categoryService.GetById(categoryModel.CategoryId);

                if (entity == null)
                    return NotFound();

                entity.CategoryId = categoryModel.CategoryId;
                entity.Name = categoryModel.Name;
                entity.Url = categoryModel.Url;

                _categoryService.Update(entity);

                #region AlertMessage işlemleri eski hali
                //var msg = new AlertMessage()
                //{
                //    Message = $"{entity.Name} isimli kategori güncellendi.",
                //    AlertType = "success"
                //};

                //TempData["message"] = JsonConvert.SerializeObject(msg);
                #endregion

                TempData.Put("message", new AlertMessage()
                {
                    Title = "kayıt eklendi.",
                    Message = $"{entity.Name} isimli category eklendi.",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }
            return View(categoryModel);
            
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
        #endregion
    }
}
