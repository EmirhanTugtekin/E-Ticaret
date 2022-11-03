using E_Ticaret.Business.Abstract;
using E_Ticaret.Data.Abstract;
using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Business.Concrete
{
    public class CartManager : ICartService
    {
        ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void addToCart(string UserId, int ProductId, int Quantity)
        {
            var cart = GetCartByUserId(UserId);

            if (cart != null)
            {
                //eklenmek istenen ürün sepette varsa sadece miktarı arttırılmalı(update)
                //eklenmek istenen ürün sepette yoksa o ürün eklenmeli(create)

                var index = cart.CartItems.FindIndex(i => i.ProductId == ProductId);

                if(index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = ProductId,
                        Quantity = Quantity,
                        CartId = cart.CartId
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += Quantity;
                }
                _cartRepository.Update(cart);
            }
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart=GetCartByUserId(userId);
            if (cart != null)
            {
                _cartRepository.DeleteFromCart(cart.CartId, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _cartRepository.Create(new Cart() { UserId = userId });
            
        }
    }
}
