using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string PaymentType { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
