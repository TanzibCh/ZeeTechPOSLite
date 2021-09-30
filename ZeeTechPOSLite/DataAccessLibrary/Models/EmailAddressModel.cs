using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class EmailAddressModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SupplierId { get; set; }
        public int LocationId { get; set; }
        public string EmailAdderss { get; set; }
    }
}
