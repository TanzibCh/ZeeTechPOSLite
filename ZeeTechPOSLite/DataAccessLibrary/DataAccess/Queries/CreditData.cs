﻿using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System.Linq;

namespace DataAccessLibrary.DataAccess.CreditQueries
{
    public class CreditData
    {
        private const string _connectionStringName = "SQLiteDB";
        private SQLiteDataAccess _db = new SQLiteDataAccess();

        #region Save Credit

        // Crete a new sale and save it in the database
        public void SaveCredit(CreditModel credit)
        {
            string sql = @"INSERT INTO Credit
                          (SaleId,  Comments, Amount, ValidTill, IsClaimed)
                          VALUES (@saleId, @commants, @amount, @validTill, @isClaimed);";

            // Insert Credit data into the database
            _db.SaveData(sql, new
            {
                saleId = credit.SaleId,
                comments = credit.Comments,
                amount = credit.Amount,
                validTill = credit.ValidTill,
                isClaimed = credit.IsClaimed
            }, _connectionStringName);
        }

        #endregion Save Credit
    }
}