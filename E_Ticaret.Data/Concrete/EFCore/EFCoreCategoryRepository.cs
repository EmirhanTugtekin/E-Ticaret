using E_Ticaret.Data.Abstract;
using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Data.Concrete.EFCore
{
    public class EFCoreCategoryRepository : EFCoreGenericRepository<Category, EFCoreContext>, ICategoryRepository
    {
        public List<Product> GetPopularCategories()
        {
            throw new NotImplementedException();
        }
    }
}
