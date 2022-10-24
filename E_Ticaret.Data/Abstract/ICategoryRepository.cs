using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Data.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void DeleteFromCategory(int productId, int categoryId);
        Category GetByIdWithProducts(int id);
    }
}
