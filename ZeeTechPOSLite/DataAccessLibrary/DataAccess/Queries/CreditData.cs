using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLibrary.DataAccess.CreditQueries
{
    public class CreditData
    {
        private const string _connectionStringName = "SQLiteDB";
        private SQLiteDataAccess _db = new SQLiteDataAccess();

        #region Save Credit

        // Crete a new sale and save it in the database
        public void SaveNewCredit(CreditModel credit)
        {
            string sql = @"INSERT INTO Credit
                          (SaleId,  Comments, Amount, ValidTill)
                          VALUES (@saleId, @commants, @amount, @validTill);";

            // Insert Credit data into the database
            _db.SaveData(sql, new
            {
                saleId = credit.SaleId,
                comments = credit.Comments,
                amount = credit.Amount,
                validTill = credit.ValidTill
            }, _connectionStringName);
        }

        public void UpdateCredit(CreditModel credit)
        {
            string sql = @"UPDATE Credit
                          SET SaleId = @saleId,
                              Comments = @comments,
                              Amount = @amount,
                              ValidIll = @validTill
                          WHERE Id = @id";

            // Save the data
            _db.SaveData(sql, new 
            { 
                id = credit.Id,
                saleId = credit.SaleId,
                comments = credit.Comments,
                amount = credit.Amount,
                validTill = credit.ValidTill

            }, _connectionStringName);
        }

        public void ChangeIsClaimed(int id, int isClaimed)
        {
            string sql = @"UPDATE Credit
                          SET IsClaimed = @isClaimed
                          WHERE Id = @id";

            // Save the data
            _db.SaveData(sql, new { id, isClaimed }, _connectionStringName);
        }
        #endregion Save Credit

        #region Get Credit

        public List<CreditModel> GetAllCredits()
        {
            string sql = @"SELECT Id, SaleId, Commants, ValidTill, Amount, IsClaimed
                          FROM Credit;";

            return _db.LoadData<CreditModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public CreditModel GetCreditById(int id)
        {
            string sql = @"SELECT Id, SaleId, Commants, ValidTill, Amount, IsClaimed
                          FROM Credit
                          WHERE Id = @id;";

            return _db.LoadData<CreditModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }

        /// <summary>
        /// Method to get all Credits that are either claimed or unclaimed
        /// pass in the choice in the parameter
        /// </summary>
        /// <param name="claimedStatus">Pass in 1 for true or 0 for fase</param>
        /// <returns></returns>
        public List<CreditModel> GetCreditsByClaimedStatus(int claimedStatus)
        {
            string sql = @"SELECT Id, SaleId, Commants, ValidTill, Amount, IsClaimed
                          FROM Credit
                          WHERE IsClaimed = @claimedStatus;";

            return _db.LoadData<CreditModel, dynamic>(sql, new { claimedStatus }, _connectionStringName);
        }
        #endregion
    }
}