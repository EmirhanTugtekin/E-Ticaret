using E_Ticaret.Business.Abstract;
using E_Ticaret.Business.Concrete;
using E_Ticaret.Data.Abstract;
using E_Ticaret.Data.Concrete.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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

            app.UseRouting();

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

