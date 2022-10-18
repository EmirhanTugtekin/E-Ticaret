using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);
        List<Category> GetAll();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
