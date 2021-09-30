using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class RefundModel
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string Comments { get; set; }
        public int Amount { get; set; }
        public int Card { get; set; }
        public int Cash { get; set; }
    }
}
