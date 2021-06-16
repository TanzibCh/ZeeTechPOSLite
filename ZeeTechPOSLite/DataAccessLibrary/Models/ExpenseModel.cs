using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseDetails { get; set; }
        public int Card { get; set; }
        public int Cash { get; set; }
        public int Total { get; set; }
    }
}
