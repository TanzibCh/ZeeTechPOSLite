using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class InventoryData
    {
        private const string _connectionStringName = "SQLiteDB";
        private readonly SQLiteDataAccess _db = new SQLiteDataAccess();

        #region Save Inventory

        public void AddNewInventory(InventoryModel inventory)
        {
            string sql = @"INSERT INTO Inventory
                          (ProductId, LocationId, SupplierId, PurchaseDate, DeliveryReceivedDate,
                          Quantity, PurchasePrice)
                          VALUES (@productId, @locationId, @supplierId, @purchaseDate,
                          @deliveryReceivedDate, @quantity, @purchasePrice);";

            // Insert stock in the database
            _db.SaveData(sql, new
            {
                productId = inventory.Productid,
                locationId = inventory.LocationId,
                supplierId = inventory.SupplierId,
                purchaseDate = inventory.PurchaseDate,
                deliveryReceivedDate = inventory.DeliveryReceivedDate,
                quantity = inventory.Quantity,
                purchasePrice = inventory.PurchasePrice
            }, _connectionStringName);
        }
        #endregion
    }
}
