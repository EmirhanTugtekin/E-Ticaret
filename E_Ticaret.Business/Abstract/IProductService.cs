using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetSearchResult(string searchString);
        List<Product> GetHomePageProducts();
        int GetCountByCategory(string category);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        Product GetProductDetails(string url);
        Product GetById(int id);
        List<Product> GetAll();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        
    }
}
