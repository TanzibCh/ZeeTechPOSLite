using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class RepairLogModel
    {
        public int Id { get; set; }
        public int RepairId { get; set; }
        public string LogDate { get; set; } // DateTime
        public string LogTime { get; set; } // DateTime
        public string Comments { get; set; }
        public int IsBooked { get; set; } // bool
        public int IsRebooked { get; set; } // bool
        public int IsDiagnosed { get; set; } // bool
        public int IsReported { get; set; } // bool
        public int IsApproved { get; set; } // bool
        public int IsDeclined { get; set; } // bool
        public int IsCollected { get; set; } // bool
    }
}
