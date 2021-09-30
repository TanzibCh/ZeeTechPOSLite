using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.StockQueries
{
    public class StockData
    {
        private const string _connectionStringName = "SQLiteDB";
        private SQLiteDataAccess _db = new SQLiteDataAccess();

        // Add a new stock
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

        public void AdjustStock(int locationId, int productId, int quantity, StockLogModel stockLog)
        {
            string sql = @"UPDATE Stock
                          SET Quantity = @quantity
                          WHERE ProductId = @productId
                          AND LocatuinId = @locationId";

            // Adjust the stock level of a product
            // that already has a stock entry in the database
            _db.SaveData(sql, new
            {
                productId = productId,
                locationId = locationId,
                quantity = quantity
            }, _connectionStringName);

            // Add stock log
            AddStockLog(stockLog);
        }

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
    }
}
