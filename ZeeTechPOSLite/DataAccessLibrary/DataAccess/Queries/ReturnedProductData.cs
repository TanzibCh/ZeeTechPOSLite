using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.ProductQueries
{
    public class ReturnedProductData
    {
        private const string _connectionStringName = "SQLiteDB";
        private Internal.SQLiteDataAccess.SQLiteDataAccess _db = new Internal.SQLiteDataAccess.SQLiteDataAccess();

        #region Save ReturnedProduct

        public void SaveReturnedProduct(ReturnedProductModel Product)
        {
            string sql = @"INSERT INTO ReturnedProduct
                          (ProductId, CreditId, RefundId, ReturnDate, RretyrnQuantity, RefundAmount)
                          VALUES (@productId, @creditId, @refundId, @returnDate, @retunQuantity, @refundAmount);";

            // Insert Returned Products in database
            _db.SaveData(sql, new
            {
                productId = Product.ProductId,
                creditId = Product.CreditId,
                refundId = Product.RefundId,
                returnDate = Product.ReturnDate,
                retunQuantity = Product.ReturnQuantity,
                refundAmount = Product.RefundAmount
            }, _connectionStringName);
        }

        public void UpdateReturnedProduct(ReturnedProductModel Product)
        {
            string sql = @"UPDATE ReturnedProduct
                           SET ProductId = @productId,
                           CreditId = @creditId,
                           RefundId = @refundId,
                           ReturnDate = @returnDate,
                           RreturnQuantity = @returnQuantity,
                           RefundAmount = @refundAmount
                           WHERE Id = @id;";

            _db.SaveData(sql, new
            {
                id = Product.Id,
                productId = Product.ProductId,
                creditId = Product.CreditId,
                refundId = Product.RefundId,
                returnDate = Product.ReturnDate,
                returnQuantity = Product.ReturnQuantity,
                refundAmount = Product.RefundAmount
            }, _connectionStringName);
        }
        #endregion

        #region Get ReturnedProducts

        public List<ReturnedProductModel> GetAllReturnedProducts()
        {
            string sql = @"SELECT Id, ProductId, CeditId, RefundId, ReturnDate, ReturnQuantity, RefundAmount
                          FROM ReturnedProduct";

            return _db.LoadData<ReturnedProductModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public List<ReturnedProductModel> GetAllCreditReturnedProducts()
        {
            string sql = @"SELECT Id, ProductId, CeditId, RefundId, ReturnDate, ReturnQuantity, RefundAmount
                          FROM ReturnedProduct
                          WHERE CreditId != 0
                          AND RefundId = 0;";

            return _db.LoadData<ReturnedProductModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public List<ReturnedProductModel> GetAllRefundReturnedProducts()
        {
            string sql = @"SELECT Id, ProductId, CeditId, RefundId, ReturnDate, ReturnQuantity, RefundAmount
                          FROM ReturnedProduct
                          WHERE RefundId != 0
                          AND CreditId = 0;";

            return _db.LoadData<ReturnedProductModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public ReturnedProductModel GetReturnedProductsId(int id)
        {
            string sql = @"SELECT Id, ProductId, CeditId, RefundId, ReturnDate, ReturnQuantity, RefundAmount
                          FROM ReturnedProduct
                          WHERE Id = @id;";

            return _db.LoadData<ReturnedProductModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }

        public ReturnedProductModel GetReturnedProductsCreditId(int creditId)
        {
            string sql = @"SELECT Id, ProductId, CeditId, RefundId, ReturnDate, ReturnQuantity, RefundAmount
                          FROM ReturnedProduct
                          WHERE CreditId = @creditId;";

            return _db.LoadData<ReturnedProductModel, dynamic>(sql, new { creditId }, _connectionStringName).FirstOrDefault();
        }

        public ReturnedProductModel GetReturnedProductsRefundId(int refundId)
        {
            string sql = @"SELECT Id, ProductId, CeditId, RefundId, ReturnDate, ReturnQuantity, RefundAmount
                          FROM ReturnedProduct
                          WHERE RefundId = @refundId;";

            return _db.LoadData<ReturnedProductModel, dynamic>(sql, new { refundId }, _connectionStringName).FirstOrDefault();
        }
        #endregion

    }
}
