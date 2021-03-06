using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public string SaleDate { get; set; } = DateTime.UtcNow.Date.ToString();
        public string SaleTime { get; set; } = DateTime.UtcNow.TimeOfDay.ToString();
        public int Card { get; set; }
        public int Cash { get; set; }
        public int Credit { get; set; }
        public int SaleTotal { get; set; }
        public int Tax { get; set; }
        public int TotalCost { get; set; }
        public int Profit { get; set; }
        public int CashOnly { get; set; }
        public int IsActive { get; set; }
    }
}
