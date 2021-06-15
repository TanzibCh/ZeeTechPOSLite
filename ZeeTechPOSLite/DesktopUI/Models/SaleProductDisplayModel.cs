using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class SaleProductDisplayModel
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCost { get; set; }
        public string SalePrice { get; set; }
        public int QuantitySold { get; set; }
        public string Total { get; set; }
        public string Department { get; set; }
    }
}
