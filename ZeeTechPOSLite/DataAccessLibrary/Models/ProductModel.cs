using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Barcode { get; set; }
        public int AverageCost { get; set; }
        public int Price { get; set; }
        public string Department { get; set; }
        public int IsActive { get; set; } // bool
    }
}