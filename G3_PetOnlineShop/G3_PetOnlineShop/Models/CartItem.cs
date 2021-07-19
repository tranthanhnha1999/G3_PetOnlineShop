using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G3_PetOnlineShop.Models;
namespace G3_PetOnlineShop.Models
{
    public class CartItem
    {
        public Product product { get; set;  }
        public int quantity { get; set; }
    }
}