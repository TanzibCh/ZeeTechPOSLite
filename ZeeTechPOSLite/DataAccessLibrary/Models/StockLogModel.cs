using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class StockLogModel
    {
        public int Id { get; set; }
        public string LogDate { get; set; } // DateTime
        public int ProductId { get; set; }
        public string Comments { get; set; }
    }
}
