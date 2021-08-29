using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class CartItemDisplayModel
    {
        public SaleProductDisplayModel Product { get; set; }
        public int Quantity { get; set; }
        public string TotalCost { get; set; }
        public string Total { get; set; }
        public string Price { get; set; }
    }
}
