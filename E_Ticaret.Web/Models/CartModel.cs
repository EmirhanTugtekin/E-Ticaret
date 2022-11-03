using E_Ticaret.Entity;
using System.Collections.Generic;
using System.Linq;

namespace E_Ticaret.Web.Models
{
    public class CartModel
    {
        public int CartModelId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
        public double TotalPrice()
        {
            return CartItems.Sum(i => i.Price * i.Quantity);
        }

        /* Cart entity
        public int CartId { get; set; }
        **public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
        */
    }
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

        /* CartItem entity
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
        */

    }
}
