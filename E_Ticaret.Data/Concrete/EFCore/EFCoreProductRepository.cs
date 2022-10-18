using E_Ticaret.Data.Abstract;
using E_Ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace E_Ticaret.Data.Concrete.EFCore
{
    public class EFCoreProductRepository : EFCoreGenericRepository<Product, EFCoreContext>, IProductRepository
    {
        public List<Product> GetPopularProducts()
        {
            using(var context = new EFCoreContext())
            {
                return context.Products.ToList();
            }
        }

        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            using (var context = new EFCoreContext())
            {
                var products = context.Products.Where(x=>x.IsApproved).AsQueryable();//aşağıda daha sorgu yapacağımız için AsQueryable() ekliyoruz

                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                                .Include(x => x.ProductCategories)
                                .ThenInclude(x => x.Product)
                                .Where(x => x.ProductCategories.Any(a => a.Category.Url == name));   
                }
                //return products.Skip(5).Take(5).ToList();//sorgu burada çalışır //skip ilk 5 ürünü öteler take ise 5 ürünü alır yani 6 7 8 9 10u almış oluruz

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();//kullanıcı page'i göndermezse varsayılan olarak ShopControllerda 1 gelir. böylece ilk 0 ürünü öteler yani öteleme olmaz. Page 2 gönderilirse 2.sayfada olan 2 ürün gösterilir
            }
        }


        public Product GetProductDetails(string url)
        {
            using (var context = new EFCoreContext())
            {
                return context.Products
                    .Where(x => x.Url == url)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)//many to many
                    .FirstOrDefault();
            }
        }

        public List<Product> GetSearchResult(string searchString)
        {
            using (var context = new EFCoreContext())
            {
                var products = context
                    .Products
                    .Where(x => x.IsApproved && (x.Name.ToLower().Contains(searchString.ToLower())) || x.Description.ToLower().Contains(searchString.ToLower()))
                    .AsQueryable();//aşağıda daha sorgu yapacağımız için AsQueryable() ekliyoruz

                return products.ToList();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new EFCoreContext())
            {
                var products = context.Products.Where(x => x.IsApproved).AsQueryable();//onaylı olan ürünleri say //aşağıda daha sorgu yapacağımız için AsQueryable() ekliyoruz

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(x => x.ProductCategories)
                                .ThenInclude(x => x.Product)
                                .Where(x => x.ProductCategories.Any(a => a.Category.Url == category));
                }


                return products.Count();
            }
        }

        public List<Product> GetHomePageProducts()
        {
            using (var context = new EFCoreContext())
            {
                return context.Products.Where(x => x.IsApproved && x.IsHome).ToList();
            }
        }
    }
}
