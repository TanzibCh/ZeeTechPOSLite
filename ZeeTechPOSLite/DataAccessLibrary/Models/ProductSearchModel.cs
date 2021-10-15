using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    /// <summary>
    /// Inherits from 
    /// </summary>
    public class ProductSearchModel : ProductModel
    {
        public int Quantity { get; set; } // Local stock Quantity
        public int TotalQuantity { get; set; } // Total quantity in stock
        public int TotalSold { get; set; } // Total quantity sold vallue. Used for Search filtering
    }
}
