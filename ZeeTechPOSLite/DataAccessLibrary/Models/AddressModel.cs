using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SupplierId { get; set; }
        public int LocationId { get; set; }
        public string Address { get; set; }
    }
}
