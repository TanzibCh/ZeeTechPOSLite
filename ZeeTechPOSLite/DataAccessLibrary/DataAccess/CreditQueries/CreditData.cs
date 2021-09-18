using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
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
                          (InvoiceNumber, CreditNumber, IsCashAccount, CreditNote, CreditAmount, ValidTillDate, IsClaimed)
                          VALUES (@invoiceNumber, @creditNumber, @isCashAccount, @creditNote, @creditAmount, @validTillDate, @isClaimed); ";

            // Insert Credit data into the database
            _db.SaveData(sql, new
            {
                invoiceNumber = credit.InvoiceNumber,
                creditNumber = GetNewCreditNo(),
                isCashAccount = credit.IsCashAccount,
                creditNote = credit.CreditNote,
                creditAmount = credit.CreditAmount,
                validTillDate = credit.ValidTillDate,
                isClaimed = credit.IsClaimed
            }, _connectionStringName);
        }

        private int GetNewCreditNo()
        {
            string sql = @"SELECT CreditNumber
                           FROM Credit
                           ORDER BY CreditNumber DESC
                           LIMIT 1;";

            CreditModel credit = _db.LoadData<CreditModel, dynamic>(
                sql, new { }, _connectionStringName).FirstOrDefault();

            int output;
            if (credit == null)
            {
                output = 1;
            }
            else
            {
                output = credit.CreditNumber + 1;
            }
            return output;
        }

        #endregion Save Credit
    }
}