using E_Ticaret.Web.Identity;
using E_Ticaret.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Ticaret.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;//cookie olaylarını yönetir

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl=null)
        {
            return View(new LoginModel()
            {
                ReturnUrl= ReturnUrl// ReturnUrl şu işe yarıyor: Bir kişi giriş yapmadan admin-product'a erişmeye çalıştı diyelim. Bu onu Login'e atar ve Login olabilirse onu anasayfaya değil de girmeye çalışıp giremediği sayfaya atmaya yarıyor
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile daha önce bir hesap oluşturulmamış");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user,model.Password,true,false);//3.cookie 4.lockout açma kapama
            if (result.Succeeded)
            {
                //return RedirectToAction("Index", "Home");
                return Redirect(model.ReturnUrl ?? "~/");//url null ise Home/Index'e yönlendir
            }
            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            var result=await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                //generate token
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Bilinmeyen bir hata oldu, lütfen tekrar deneyiniz");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
