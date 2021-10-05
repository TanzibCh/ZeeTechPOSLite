using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class StockModel
    {
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } // Inner join to Product
        public string LocationName { get; set; } // Inner joun to Location
    }
}
