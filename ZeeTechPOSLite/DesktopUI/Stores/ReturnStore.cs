using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Stores
{
    public class ReturnStore
    {
        public int SaleId { get; set; }
        public decimal Amount { get; set; }
        public List<SaleProductDisplayModel> ReturnProducts { get; set; }
    }
}