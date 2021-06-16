using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.ExpenseQueries
{
    public class ExpenseData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        public void SaveExpense(ExpenseModel expense)
        {
            string sql = @"INSERT INTO Expense
                           (ExpenseDate, ExpenseDetails, Card, Cash, Total)
                           VALUES (@expenseDate, @expenseDetails, @card, @cash, @total)";

            _db.SaveData(sql, new
            {
                expenseDate = Convert.ToString(expense.ExpenseDate),
                expenseDetails = expense.ExpenseDetails,
                card = expense.Card,
                cash = expense.Cash,
                total = expense.Total
            }, _connectionStringName);
        }
    }
}
