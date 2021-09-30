using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string ExpenseDate { get; set; } // DateTime
        public string ExpenseDetails { get; set; }
        public int ExpenseTotal { get; set; } // Decimal
        public int Card { get; set; } // Decimal
        public int Cash { get; set; } // Decimal
        public int IsActive { get; set; } // bool
    }
}
