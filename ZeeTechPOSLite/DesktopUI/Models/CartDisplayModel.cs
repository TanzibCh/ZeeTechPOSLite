using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class CartDisplayModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
