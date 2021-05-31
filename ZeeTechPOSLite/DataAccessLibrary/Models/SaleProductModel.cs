using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class SaleProductModel
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int SalePrice { get; set; }
        public int ProductCost { get; set; }
        public string Department { get; set; }
    }
}
