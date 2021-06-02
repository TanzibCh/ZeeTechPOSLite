using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class ExpenseDisplayModel
    {
        public int Id { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
