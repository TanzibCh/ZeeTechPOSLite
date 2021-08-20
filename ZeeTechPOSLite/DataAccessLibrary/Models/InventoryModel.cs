using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public int Productid { get; set; }
        public int LocationId { get; set; }
        public int SupplierId { get; set; }
        public string PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public int PurchasePrice { get; set; }
    }
}
