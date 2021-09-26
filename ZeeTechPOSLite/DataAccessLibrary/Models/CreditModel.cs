using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class CreditModel
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string CreditNote { get; set; }
        public string ValidTill { get; set; }
        public int Amount { get; set; }
        public int IsClaimed { get; set; }
    }
}