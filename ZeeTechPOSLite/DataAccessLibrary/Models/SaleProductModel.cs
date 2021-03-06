using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class SaleProductModel
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Department { get; set; }
        public int SalePrice { get; set; }
        public int ProductCost { get; set; }
        public int QuantitySold { get; set; }
        public int Total { get; set; }
        public int IsActive { get; set; }
    }
}
