using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class SupplierModel
    {
        public int Id { get; set; }
        public string SupplirName { get; set; }
        public int IsActive { get; set; } // bool
    }
}
