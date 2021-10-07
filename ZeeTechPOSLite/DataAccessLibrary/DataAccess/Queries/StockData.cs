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
        private const string _connectionStringName = "SQLiteDB";
        private readonly SQLiteDataAccess _db = new SQLiteDataAccess();

        #region Save Stock

        /// <summary>
        /// Add a new stock to the database when the product has no
        /// entry in the database. eg. newly created product.
        /// </summary>
        /// <param name="locationId">Needs an int parameter for LocationId</param>
        /// <param name="product">Needs an int parameter for ProductId</param>
        /// <param name="quantity">Needs an int parameter for Quantity</param>
        /// <param name="stockLog">Needs a StockLog Model</param>
        public void AddStock(int locationId, int productId,
            int quantity, string comments)
        {
            string sql = @"INSERT INTO Stock
                          (ProductId, LocationId, Quantity)
                          VALUES (@productId, @locationId, @quantity);";

            // Insert stock in the database
            _db.SaveData(sql, new
            {
                productId = productId,
                locationId = locationId,
                quantity = quantity
            }, _connectionStringName);

            // Add stock log
            AddStockLog(productId, DateTime.UtcNow.Date.ToString(), comments);
        }

        // Adjust the stock level of a product
        // that already has a stock entry in the database
        public void AdjustStock(int locationId, int productId, int quantity, string comments)
        {
            string sql = @"UPDATE Stock
                          SET Quantity = @quantity
                          WHERE ProductId = @productId
                          AND LocationId = @locationId";

            // Save the data
            _db.SaveData(sql, new
            {
                productId = productId,
                locationId = locationId,
                quantity = quantity
            }, _connectionStringName);

            // Add stock log
            AddStockLog(productId, DateTime.UtcNow.Date.ToString(), comments);
        }
        #endregion

        #region Get Stock

        public List<StockModel> GetAllStock(StockLogModel stockLog)
        {
            string sql = @"SELECT s.ProductId, s.LocationId, s.Quantity, p.ProductName, l.LocationName
                          FROM Stock s
                          INNER JOIN Product p on s.ProductId = p.Id
                          INNER JOIN Location l on s.LocationId = l.Id;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { }, _connectionStringName);

            return stock;
        }

        //SELECT ProductId, LocationId, Quantity
        //                  FROM Stock
        //                  WHERE LocationId = @locationId;
        public List<StockModel> GetStockByLocation(int locationId)
        {
            string sql = @"SELECT s.ProductId, s.LocationId, s.Quantity, p.ProductName, l.LocationName
                          FROM Stock s
                          INNER JOIN Product p on s.ProductId = p.Id
                          INNER JOIN Location l on s.LocationId = l.Id
                          WHERE s.LocationId = @locationId;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { locationId }, _connectionStringName);

            return stock;
        }

        public List<StockModel> GetStockByProduct(int productId)
        {
            string sql = @"SELECT s.ProductId, s.LocationId, s.Quantity, p.ProductName, l.LocationName
                          FROM Stock s
                          INNER JOIN Product p on s.ProductId = p.Id
                          INNER JOIN Location l on s.LocationId = l.Id
                          WHERE s.ProductId = @productId;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { productId }, _connectionStringName);

            return stock;
        }

        public List<StockModel> GetStockByProductAndLocation(int productId, int locationId)
        {
            string sql = @"SELECT s.ProductId, s.LocationId, s.Quantity, p.ProductName, l.LocationName
                          FROM Stock s
                          INNER JOIN Product p on s.ProductId = p.Id
                          INNER JOIN Location l on s.LocationId = l.Id
                          WHERE s.ProductId = @productId
                          AND s.LocationId = @locationId;";

            List<StockModel> stock = _db.LoadData<StockModel, dynamic>(sql, new { productId, locationId }, _connectionStringName);

            return stock;
        }
        #endregion

        #region Log Entry

        // When ever stock is altered need to call this method
        // to add a record of the transaction
        private void AddStockLog(int productId, string logDate, string comments)
        {
            string sql = @"INSERT INTO StockLog
                          (ProductId, LogDate, Comments)
                          VALUES (@productId, @logDate, @comments);";

            // Insert to stoclog
            _db.SaveData(sql, new { productId, logDate, comments }, _connectionStringName);
        }

        public void GetStockLog()
        {

        }

        #endregion
    }
}
