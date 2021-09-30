using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class StockLogModel
    {
        public int Id { get; set; }
        public int LogTypeId { get; set; }
        public string LogDate { get; set; } // DateTime
        public string Comments { get; set; }
    }
}
