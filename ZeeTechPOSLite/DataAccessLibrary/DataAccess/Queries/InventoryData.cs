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
        private readonly Internal.SQLiteDataAccess.SQLiteDataAccess _db = new Internal.SQLiteDataAccess.SQLiteDataAccess();

        #region Save Inventory

        public void AddNewInventory(InventoryModel inventory)
        {
            string sql = @"INSERT INTO Inventory
                          (ProductId, LocationId, SupplierId, PurchaseDate, EntryDate,
                          Quantity, PurchasePrice)
                          VALUES (@productId, @locationId, @supplierId, @purchaseDate,
                          @entryDate, @quantity, @purchasePrice);";

            // Insert stock in the database
            _db.SaveData(sql, new
            {
                productId = inventory.ProductId,
                locationId = inventory.LocationId,
                supplierId = inventory.SupplierId,
                purchaseDate = inventory.PurchaseDate,
                entryDate = inventory.EntryDate,
                quantity = inventory.Quantity,
                purchasePrice = inventory.PurchasePrice
            }, _connectionStringName);
        }
        #endregion

        #region Get Inventory

        public List<InventoryModel> GetAllInventory()
        {
            string sql = @"SELECT Id, ProductId, LocationId, SupplierId, PurchaseDate,
                          DeliveryReceivedDate, Quantity, PurchasePrice
                          FROM Inventory;";

            return _db.LoadData<InventoryModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public List<InventoryModel> GetInventoryByPurchaseDate(string purchaseDate)
        {
            string sql = @"SELECT Id, ProductId, LocationId, SupplierId, PurchaseDate,
                          DeliveryReceivedDate, Quantity, PurchasePrice
                          FROM Inventory
                          WHERE PurchaseDate = @purchaseDate;";

            return _db.LoadData<InventoryModel, dynamic>(sql, new { purchaseDate }, _connectionStringName);
        }

        public List<InventoryModel> GetInventoryByEntryDate(string entryDate)
        {
            string sql = @"SELECT Id, ProductId, LocationId, SupplierId, PurchaseDate,
                          EntryDate, Quantity, PurchasePrice
                          FROM Inventory
                          WHERE EntryDate = @entryDate;";

            return _db.LoadData<InventoryModel, dynamic>(sql, new { entryDate }, _connectionStringName);
        }

        public List<InventoryModel> GetInventoryByProduct(int productId)
        {
            string sql = @"SELECT Id, ProductId, LocationId, SupplierId, PurchaseDate,
                          DeliveryReceivedDate, Quantity, PurchasePrice
                          FROM Inventory
                          WHERE ProductId = @productId;";

            return _db.LoadData<InventoryModel, dynamic>(sql, new { productId }, _connectionStringName);
        }

        public List<InventoryModel> GetInventoryBySupplier(int supplierId)
        {
            string sql = @"SELECT Id, ProductId, LocationId, SupplierId, PurchaseDate,
                          DeliveryReceivedDate, Quantity, PurchasePrice
                          FROM Inventory
                          WHERE SupplierId = @supplierId;";

            return _db.LoadData<InventoryModel, dynamic>(sql, new { supplierId }, _connectionStringName);
        }

        public List<InventoryModel> GetInventoryByLocation(int locationId)
        {
            string sql = @"SELECT Id, ProductId, LocationId, SupplierId, PurchaseDate,
                          DeliveryReceivedDate, Quantity, PurchasePrice
                          FROM Inventory
                          WHERE LocationId = @locationId;";

            return _db.LoadData<InventoryModel, dynamic>(sql, new { locationId }, _connectionStringName);
        }
        #endregion
    }
}
