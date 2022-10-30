using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Business.Abstract
{
    public interface IProductService
    {
        Product GetByIdWithCategories(int id);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetHomePageProducts();
        int GetCountByCategory(string category);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        Product GetProductDetails(string url);
        Product GetById(int id);
        List<Product> GetAll();
        bool Create(Product entity);
        void Update(Product entity);
        bool Update(Product entity, int[] categoryIds);
        void Delete(Product entity);
        
    }
}
