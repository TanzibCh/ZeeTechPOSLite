using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class ReturnedProductModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CreditId { get; set; }
        public int RefundId { get; set; }
        public string ReturnDate { get; set; } // DateTime
        public int ReturnQuantity { get; set; }
        public int RefundAmount { get; set; } // Decimal
    }
}
