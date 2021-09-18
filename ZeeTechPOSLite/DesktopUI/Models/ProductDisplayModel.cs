using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class ProductDisplayModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Barcode { get; set; }
        public decimal AverageCost { get; set; }
        public decimal Price { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
    }
}