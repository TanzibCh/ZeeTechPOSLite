using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class RepairModel
    {
        public int Id { get; set; }
        public int RepairNumber { get; set; }
        public string BookingDate { get; set; }
        public string DiagnosticReport { get; set; }
        public int DiagnosticCharged { get; set; } // bool
        public int DiagnosticAmount { get; set; }
        public int QuotationAmount { get; set; }
        public int BalanceToPay { get; set; }
        public int DiagnosticDone { get; set; } // bool
        public int ReportGiven { get; set; } // bool
        public int IsApproved { get; set; } // bool
        public int IsRepaired { get; set; } // bool
        public int IsRepairable { get; set; } // bool
        public int IsActive { get; set; } // bool
    }
}
