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
                           (ExpenseDate, ExpenseDetails, Card, Cash, ExpenseTotal)
                           VALUES (@expenseDate, @expenseDetails, @card, @cash, @total)";

            _db.SaveData(sql, new
            {
                expenseDate = Convert.ToString(expense.ExpenseDate),
                expenseDetails = expense.ExpenseDetails,
                card = expense.Card,
                cash = expense.Cash,
                total = expense.ExpenseTotal
            }, _connectionStringName);
        }

        public List<ExpenseModel> LoadAllExpenseByDate(string selectedDate)
        {
            string sql = @"SELECT Id, ExpenseDate, ExpenseDetails, Card, Cash, ExpenseTotal
                          FROM Expense
                          WHERE ExpenseDate = @selectedDate;";

            List<ExpenseModel> expenses = _db.LoadData<ExpenseModel, dynamic>(sql, new { selectedDate }, _connectionStringName);

            return expenses;
        }
    }
}
