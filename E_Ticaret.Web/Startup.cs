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
            //Identity i�in
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("server=DESKTOP-6TUVH6H; database=E_Ticaret; integrated security=True"));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true;

                //lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromSeconds(60);
                options.Lockout.AllowedForNewUsers = true;//lockout'un aktif olmas� i�in

                //options.User.AllowedUserNameCharacters="" //bu karakterlere username'de izin ver, mesela � � # ? gibi
                options.User.RequireUniqueEmail = true;//herkesin mail adresi farkl� olmal�
                //options.SignIn.RequireConfirmedEmail = true;//email onayl� olmal�
                //options.SignIn.RequireConfirmedPhoneNumber = true;//telefon numaras� onayl� olmal�   
            });

            //cookie ayarlar� (AddIdentity'nin alt�nda olmal�)
            services.ConfigureApplicationCookie(options =>
            {
                //cookie=taray�c� senin kullan�c� ad�n� ve �ifreni hat�rlas�n m�, sen daha �nce falanca sitede telefon bakt�ysan ve o siteye ayn� bilgisayarda ayn� taray�c�dan tekrar girdi�inde sana telefonlar �nermesi vs gibi �eyler cookie'dir. Uygulamaya request atarsan sunucuya i�inde senin taray�c�ndaki cookie'lerin oldu�u bir request gider.
                options.LoginPath = "/account/login/";//cookie ile session birbirini tan�mazsa yani yetkilendirme gereken bir sayfaya login olmam�� bir kullan�c� ula�maya �al���rsa account controller'�n�n login action'�na git
                options.LogoutPath = "/account/logout/";//��k�� yap�nca cookie taray�c�dan silinir ve cookie ile session ba�lant�s� kesilmi� olur, bu y�zden seni account/logout'a g�nderir
                options.AccessDeniedPath = "/account/accessdenied";//her login olan her sayfay� (mesela y�netici sayfas�n�) g�remez. Bu y�zden "eri�imin yok" tarz� bir sayfaya y�nlendircez
                
                options.SlidingExpiration = true;//standart olarak 20dk o siteye request atmazsan cookie silinr ve tekrar giri� yapman gerekir. SlidingExpiration'� true yaparak her siteye request att���nda o 20dk s�f�rlan�r. False dersen 20dk ne kadar request atarsan at s�f�rlanmaz
                options.ExpireTimeSpan = TimeSpan.FromDays(5);//5 g�n boyunca uygulamaya tekrar login olman gerekmez.

                options.Cookie = new CookieBuilder()
                {
                    HttpOnly = true,//cookie sadece http request'i ile elde edilebilsin. Mesela bir Javascript uygulamas� cookie'yi elde edemesin
                    Name=".eTicaret.Security.Cookie", //cookie'nin ismini �zelle�tirme
                    SameSite=SameSiteMode.Strict// A kullan�c�s� giri� yap�nca client taraf�nda cookie, server taraf�nda session olu�turulur ve bunlar haberle�ir. E�er B kullan�c�s� A kullan�c�s�n�n cookie'sine sahip olsa dahi giri� yapamas�n istiyorsak bu ayar� yapar�z

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
            app.UseAuthorization();//bu useRouting ile useEndpoints aras�nda olmak zorunda

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
                   );//kullan�c� localhost123/samsung-s20 ye request atarsa shop controller'�n�n details metodunda samsung 20 sayfas�na y�nlendirilir

                endpoints.MapControllerRoute(
                    name: "products",
                    pattern:"products/{category?}",
                    defaults:new {controller="shop",action="list"}
                    );//kullan�c� localhost123/products a request atarsa shop controller'�n�n list metoduna y�nlendirilir

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }

}

