using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int IsActive { get; set; }
    }
}
