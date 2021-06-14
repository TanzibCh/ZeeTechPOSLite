using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    // sp.SaleId, s.InvoiceNo, s.SaleDate, sp.ProductId, sp.ProductName, sp.ProductDescription, sp.SalePrice, sp.ProductCost, sp.QuantitySold, sp.Department
    public class SaleProductDBModel
    {
        public int SaleId { get; set; }
        public int InvoiceNo { get; set; }
        public string SaleDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int SalePrice { get; set; }
        public int ProductCost { get; set; }
        public int QuantitySold { get; set; }
        public string Department { get; set; }
    }
}
