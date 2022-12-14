using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Data.Abstract
{
    public interface ICartRepository:IRepository<Cart>
    {
        void DeleteFromCart(int cartId, int productId);
        Cart GetCartByUserId(string userId);
    }
}
