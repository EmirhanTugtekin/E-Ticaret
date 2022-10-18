using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Data.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        List<Product> GetPopularCategories();
    }
}
