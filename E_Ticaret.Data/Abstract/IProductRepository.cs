using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Data.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        Product GetByIdWithCategories(int id);
        List<Product> GetHomePageProducts();
        int GetCountByCategory(string category);
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        List<Product> GetSearchResult(string searchString);
    }
}
