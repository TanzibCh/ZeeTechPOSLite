using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.ProductQueries
{
    public class ReturnedProductData
    {
        private const string _connectionStringName = "SQLiteDB";
        private Internal.SQLiteDataAccess.SQLiteDataAccess _db = new Internal.SQLiteDataAccess.SQLiteDataAccess();

        public void SaveReturnedProduct(ReturnedProductModel returnedProductModel)
        {
            string sql = @"INSERT INTO ReturnedProduct
                          (ProductId, CreditId, RefundId, ReturnDate, RretyrnQuantity, RefundAmount)
                          VALUES (@productId, @creditId, @refundId, @returnDate, @retunQuantity, @refundAmount);";

            // Insert Returned Products in database
            _db.SaveData(sql, new
            {
                productId = returnedProductModel.ProductId,
                creditId = returnedProductModel.CreditId,
                refundId = returnedProductModel.RefundId,
                returnDate = returnedProductModel.ReturnDate,
                retunQuantity = returnedProductModel.ReturnQuantity,
                refundAmount = returnedProductModel.RefundAmount
            }, _connectionStringName);
        }
    }
}
