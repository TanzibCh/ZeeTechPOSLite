using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class CartItemDisplayModel : ICartItemDisplayModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
    }
}
