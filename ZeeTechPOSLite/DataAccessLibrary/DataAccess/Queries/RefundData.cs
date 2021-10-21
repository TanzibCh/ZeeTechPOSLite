using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class RefundData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Refund

        public void SaveRefund(RefundModel refund)
        {
            // Query to save Address data
            string sql = @"INSERT INTO Refund
                          (SaleId, Comments, Amount, Card, Cash)
                          VALUES (@saleId, @comments, @amount, @card, @cash);";


            // Insert Address data into the database
            _db.SaveData(sql, new
            {
                saleId = refund.SaleId,
                comments = refund.Comments,
                amount = refund.Amount,
                card = refund.Card,
                cash = refund.Cash
            }, _connectionStringName);
        }

        public void UpdateRefund(RefundModel refund)
        {
            string sql = @"UPDATE Product
                           SET SalesId = @salesId,
                           Comments = @comments,
                           Amount = @amount
                           Card = @card,
                           Cash = @cash
                           WHERE Id = @id;";

            _db.SaveData(sql, new
            {
                id = refund.Id,
                salesId = refund.SaleId,
                comments = refund.Comments,
                amount = refund.Amount,
                card = refund.Card,
                cash = refund.Cash
            }, _connectionStringName);
        }
        #endregion

        #region Get Refund

        public RefundModel GetLatestRefund()
        {
            string sql = @"SELECT Id, SaleId, Comments, Amount, Card, cash
                          FROM Refund
                          ORDER BY Id DESC
                          LIMIT 1;";

            return _db.LoadData<RefundModel, dynamic>(sql, new { }, _connectionStringName).FirstOrDefault();
        }

        public List<RefundModel> GetAllRefund()
        {
            string sql = @"SELECT Id, SaleId, Comments, Amount, Card, cash
                          FROM Refund";

            return _db.LoadData<RefundModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public List<RefundModel> GetAllActiveRefund()
        {
            string sql = @"SELECT Id, SaleId, Comments, Amount, Card, cash
                          FROM Refund
                          WHERE IsActive = 1";

            return _db.LoadData<RefundModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public RefundModel GetRefundById(int id)
        {
            string sql = @"SELECT Id, SaleId, Comments, Amount, Card, cash
                          FROM Refund
                          WHERE Id = @id";

            return _db.LoadData<RefundModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }
        #endregion
    }
}
