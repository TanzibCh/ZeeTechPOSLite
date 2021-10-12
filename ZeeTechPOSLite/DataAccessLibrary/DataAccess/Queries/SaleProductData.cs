using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.SalesQueries
{
    public class SaleProductData
    {
        private readonly Internal.SQLiteDataAccess.SQLiteDataAccess _db = new Internal.SQLiteDataAccess.SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        public List<SaleProductModel> GetSaleProductBySaleId(int saleId)
        {
            string sql = @"SELECT Id, ProductId, ProductName, ProductDescription, SalePrice, ProductCost, QuantitySold, Total, Department
                           FROM SaleProduct
                           WHERE SaleId = @saleId;";

            List<SaleProductModel> products = _db.LoadData<SaleProductModel, dynamic>(sql, new { saleId }, _connectionStringName);

            return products;
        }
    }
}
