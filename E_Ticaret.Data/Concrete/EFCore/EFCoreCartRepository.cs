using E_Ticaret.Data.Abstract;
using E_Ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Ticaret.Data.Concrete.EFCore
{
    public class EFCoreCartRepository : EFCoreGenericRepository<Cart, EFCoreContext>, ICartRepository
    {
        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context= new EFCoreContext())
            {
                var command = "delete from CartItems where cartId=@p0 and productId=@p1";
                context.Database.ExecuteSqlRaw(command, cartId, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using(var context = new EFCoreContext())
            {
                return context.Carts
                                    .Include(i => i.CartItems)
                                    .ThenInclude(i => i.Product)
                                    .FirstOrDefault(i => i.UserId == userId);
            }
        }
        public override void Update(Cart entity)
        {
            using(var context=new EFCoreContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }


    }
}
