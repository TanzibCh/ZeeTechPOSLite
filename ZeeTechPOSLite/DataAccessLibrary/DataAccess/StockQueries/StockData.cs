using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.StockQueries
{
    // This class saves, loads and updates
    // Stock data and StockLog data in the database
    public class StockData
    {
        #region Save Stock
        private const string _connectionStringName = "SQLiteDB";
        private readonly SQLiteDataAccess _db = new SQLiteDataAccess();

        // Add a new stock to the database when the product has no
        // entry in the database. eg. newly created product.
        public void AddStock(int locationId, ProductModel product,
            int quantity, StockLogModel stockLog)
        {
            string sql = @"INSERT INTO Stock
                          (ProductId, LocationId, Quantity)
                          VALUES (@productId, @locationId, @quantity);";

            // Insert stock in the database
            _db.SaveData(sql, new
            {
                productId = product.Id,
                locationId = locationId,
                quantity = quantity
            }, _connectionStringName);

            // Add stock log
            AddStockLog(stockLog);
        }

        // Adjust the stock level of a product
        // that already has a stock entry in the database
        public void AdjustStock(int locationId, int productId, int quantity, StockLogModel stockLog)
        {
            string sql = @"UPDATE Stock
                          SET Quantity = @quantity
                          WHERE ProductId = @productId
                          AND LocatuinId = @locationId";

            // Save the data
            _db.SaveData(sql, new
            {
                productId = productId,
                locationId = locationId,
                quantity = quantity
            }, _connectionStringName);

            // Add stock log
            AddStockLog(stockLog);
        }
        #endregion

        #region Get Stock

        public List<StockModel> GetAllStock(StockLogModel stockLog)
        {
            string sql = @"SELECT ProductId, LocationId, Quantity
                          FROM Stock;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { }, _connectionStringName);

            return stock;
        }

        public List<StockModel> GetStockByLocation(int locationId)
        {
            string sql = @"SELECT ProductId, LocationId, Quantity
                          FROM Stock
                          WHERE LocationId = @locationId;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { locationId }, _connectionStringName);

            return stock;
        }

        public List<StockModel> GetStockByProduct(int productId)
        {
            string sql = @"SELECT ProductId, LocationId, Quantity
                          FROM Stock
                          WHERE LocationId = @productId;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { productId }, _connectionStringName);

            return stock;
        }

        public List<StockModel> GetStockByProductAndLocation(int productId, int locationId)
        {
            string sql = @"SELECT ProductId, LocationId, Quantity
                          FROM Stock
                          WHERE ProductId = @productId
                          AND LocationId = @locationId;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { productId, locationId }, _connectionStringName);

            return stock;
        }
        #endregion

        #region Log Entry

        // When ever stock is altered need to call this method
        // to add a record of the transaction
        private void AddStockLog(StockLogModel stockLog)
        {
            string sql = @"INSERT INTO Stock
                          (LogTypeId, LogDate, Comments)
                          VALUES (@logTypeId, @logDate, @comments);";

            // Insert to stoclog
            _db.SaveData(sql, new
            {
                logTypeId = stockLog.LogTypeId,
                logDate = stockLog.LogDate,
                comments = stockLog.Comments
            }, _connectionStringName);
        }

        public void GetStockLog()
        {

        }

        #endregion
    }
}
