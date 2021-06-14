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
        public string Cash { get; set; }
        public string Card { get; set; }
        public string Credit { get; set; }
        public string SaleTotal { get; set; }
        public string Tax { get; set; }
        public string TotalCost { get; set; }
        public string Profit { get; set; }
        public bool CashOnly { get; set; }
    }
}
