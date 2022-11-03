using E_Ticaret.Business.Abstract;
using E_Ticaret.Web.Identity;
using E_Ticaret.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Ticaret.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<AppUser> _userManager;//Eğer ICartService'de GetCartByUserId metodunda parametre olarak userId'ye göre değilde userName'e göre alsaydım buna gerek kalmazdı çünkü identity'de name için hazır metot var: var cart=_cartService.GetCartByUserId(User.Identity.Name); diyebilirdim. UserId'ye göre işlem yapacağım için giriş yapan kullanıcının id'sine ihtiyaç var ve onun için de UserManager'a ihtiyaç var
        public CartController(ICartService cartService, UserManager<AppUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartModelId = cart.CartId,
                CartItems = cart.CartItems.Select(a => new CartItemModel()
                {
                    CartItemId = a.CartItemId,
                    ProductId = a.ProductId,
                    Name = a.Product.Name,
                    Price = (double)a.Product.Price,
                    ImageUrl = a.Product.ImageUrl,
                    Quantity = a.Quantity
                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.addToCart(userId, productId, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, productId);
            return RedirectToAction("Index");

        }
    }
}
