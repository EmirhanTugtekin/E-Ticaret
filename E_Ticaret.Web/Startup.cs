using E_Ticaret.Business.Abstract;
using E_Ticaret.Business.Concrete;
using E_Ticaret.Data.Abstract;
using E_Ticaret.Data.Concrete.EFCore;
using E_Ticaret.Web.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Identity için
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("server=DESKTOP-6TUVH6H; database=E_Ticaret; integrated security=True"));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true;

                //lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromSeconds(60);
                options.Lockout.AllowedForNewUsers = true;//lockout'un aktif olmasý için

                //options.User.AllowedUserNameCharacters="" //bu karakterlere username'de izin ver, mesela þ ö # ? gibi
                options.User.RequireUniqueEmail = true;//herkesin mail adresi farklý olmalý
                //options.SignIn.RequireConfirmedEmail = true;//email onaylý olmalý
                //options.SignIn.RequireConfirmedPhoneNumber = true;//telefon numarasý onaylý olmalý   
            });

            //cookie ayarlarý (AddIdentity'nin altýnda olmalý)
            services.ConfigureApplicationCookie(options =>
            {
                //cookie=tarayýcý senin kullanýcý adýný ve þifreni hatýrlasýn mý, sen daha önce falanca sitede telefon baktýysan ve o siteye ayný bilgisayarda ayný tarayýcýdan tekrar girdiðinde sana telefonlar önermesi vs gibi þeyler cookie'dir. Uygulamaya request atarsan sunucuya içinde senin tarayýcýndaki cookie'lerin olduðu bir request gider.
                options.LoginPath = "/account/login/";//cookie ile session birbirini tanýmazsa yani yetkilendirme gereken bir sayfaya login olmamýþ bir kullanýcý ulaþmaya çalýþýrsa account controller'ýnýn login action'ýna git
                options.LogoutPath = "/account/logout/";//çýkýþ yapýnca cookie tarayýcýdan silinir ve cookie ile session baðlantýsý kesilmiþ olur, bu yüzden seni account/logout'a gönderir
                options.AccessDeniedPath = "/account/accessdenied";//her login olan her sayfayý (mesela yönetici sayfasýný) göremez. Bu yüzden "eriþimin yok" tarzý bir sayfaya yönlendircez
                
                options.SlidingExpiration = true;//standart olarak 20dk o siteye request atmazsan cookie silinr ve tekrar giriþ yapman gerekir. SlidingExpiration'ý true yaparak her siteye request attýðýnda o 20dk sýfýrlanýr. False dersen 20dk ne kadar request atarsan at sýfýrlanmaz
                options.ExpireTimeSpan = TimeSpan.FromDays(5);//5 gün boyunca uygulamaya tekrar login olman gerekmez.

                options.Cookie = new CookieBuilder()
                {
                    HttpOnly = true,//cookie sadece http request'i ile elde edilebilsin. Mesela bir Javascript uygulamasý cookie'yi elde edemesin
                    Name=".eTicaret.Security.Cookie", //cookie'nin ismini özelleþtirme
                    SameSite=SameSiteMode.Strict// A kullanýcýsý giriþ yapýnca client tarafýnda cookie, server tarafýnda session oluþturulur ve bunlar haberleþir. Eðer B kullanýcýsý A kullanýcýsýnýn cookie'sine sahip olsa dahi giriþ yapamasýn istiyorsak bu ayarý yaparýz

                };
            });

            // mvc
            // razor pages
            services.AddControllersWithViews();

            services.AddScoped<IProductRepository, EFCoreProductRepository>();
            services.AddScoped<ICategoryRepository, EFCoreCategoryRepository>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(); // wwwroot

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/modules"
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();//bu useRouting ile useEndpoints arasýnda olmak zorunda

            // localhost:5000
            // localhost:5000/home
            // localhost:5000/home/index
            // localhost:5000/product/details/3
            // localhost:5000/product/list/2
            // localhost:5000/category/list

            app.UseEndpoints(endpoints =>
            {
                //Admin/ProductList yerine admin/products olur
                endpoints.MapControllerRoute(
                    name: "adminEditCategory",
                    pattern: "admin/categories/{id?}",
                    defaults: new { controller = "Admin", action = "EditCategory" }
                );

                //Admin/ProductList yerine admin/products olur
                endpoints.MapControllerRoute(
                    name: "adminproductlist",
                    pattern: "admin/products",
                    defaults: new { controller = "Admin", action = "ProductList" }
                );

                //localhost/search
                endpoints.MapControllerRoute(
                   name: "search",
                   pattern: "search",
                   defaults: new { controller = "shop", action = "search" }
                   );

                endpoints.MapControllerRoute(
                   name: "productdetails",
                   pattern: "{url}",
                   defaults: new { controller = "shop", action = "details" }
                   );//kullanýcý localhost123/samsung-s20 ye request atarsa shop controller'ýnýn details metodunda samsung 20 sayfasýna yönlendirilir

                endpoints.MapControllerRoute(
                    name: "products",
                    pattern:"products/{category?}",
                    defaults:new {controller="shop",action="list"}
                    );//kullanýcý localhost123/products a request atarsa shop controller'ýnýn list metoduna yönlendirilir

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }

}

