using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class CreditModel
    {
        public int Id { get; set; }
        public int CreditNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public int IsCashAccount { get; set; }
        public string CreditNote { get; set; }
        public int CreditAmount { get; set; }
        public string ValidTillDate { get; set; }
        public int IsClaimed { get; set; }
    }
}