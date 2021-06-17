using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Models
{
    public class ExpenseDisplayModel
    {
        public int Id { get; set; }
        public string ExpenseDate { get; set; }
        public string ExpenseDetails { get; set; }
        public string Card { get; set; }
        public string Cash { get; set; }
        public string ExpenseTotal { get; set; }
    }
}
