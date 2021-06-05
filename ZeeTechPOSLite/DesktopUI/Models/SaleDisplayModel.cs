using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class SaleDisplayModel
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public string SaleDate { get; set; }
        public string SaleTime { get; set; }
        public decimal Cash { get; set; }
        public decimal Credit { get; set; }
        public decimal SaleTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Profit { get; set; }
        public bool CashOnly { get; set; }
    }
}
