using E_Ticaret.Business.Abstract;
using E_Ticaret.Data.Abstract;
using E_Ticaret.Data.Concrete.EFCore;
using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Ticaret.Business.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public bool Create(Product entity)
        {
            _productRepository.Create(entity);
            return true;
            
        }

        public void Delete(Product entity)
        {
            // iş kuralları
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetProductsByCategory(string name,int page,int pageSize)
        {
            return _productRepository.GetProductsByCategory(name,page,pageSize);
        }

        public Product GetProductDetails(string url)
        {
            return _productRepository.GetProductDetails(url);
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productRepository.GetHomePageProducts();
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _productRepository.GetSearchResult(searchString);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productRepository.GetByIdWithCategories(id);
        }

        public bool Update(Product entity, int[] categoryIds)
        {
            _productRepository.Update(entity, categoryIds);
            return true;
        }
    }
}
