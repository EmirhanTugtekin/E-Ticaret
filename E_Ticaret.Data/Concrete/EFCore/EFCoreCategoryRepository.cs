using E_Ticaret.Data.Abstract;
using E_Ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Ticaret.Data.Concrete.EFCore
{
    public class EFCoreCategoryRepository : EFCoreGenericRepository<Category, EFCoreContext>, ICategoryRepository
    {
        public void DeleteFromCategory(int productId, int categoryId)
        {
            using(var context=new EFCoreContext())
            {
                string command = "delete from productcategory where productId=@p0 and categoryId=@p1";
                context.Database.ExecuteSqlRaw(command,productId,categoryId);
                
            }
        }

        public Category GetByIdWithProducts(int id)
        {
            using (var context = new EFCoreContext())
            {
                return context.Categories
                                         .Where(a => a.CategoryId == id)
                                         .Include(a => a.ProductCategories)
                                         .ThenInclude(a => a.Product)
                                         .FirstOrDefault();
            }

            
        }
    }
}
