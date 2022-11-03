using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        void addToCart(string UserId, int ProductId, int Quantity);
        void DeleteFromCart(string userId, int productId);
    }
}
