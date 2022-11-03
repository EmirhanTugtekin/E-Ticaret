using System;
using System.Collections.Generic;
using System.Text;

namespace E_Ticaret.Entity
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
