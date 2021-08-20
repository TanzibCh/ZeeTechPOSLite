using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.BankingCommands
{
    public class AddExpenseCommand : CommandBase
    {
        private readonly BankingViewModel _bankingViewModel;

        public AddExpenseCommand(BankingViewModel bankingViewModel)
        {
            _bankingViewModel = bankingViewModel;
        }

        public override void Execute(object parameter)
        {
            string expenseStatus = _bankingViewModel.ExpenseLable;

            if (expenseStatus == "New Expense")
            {
                _bankingViewModel.AddNewExpense();
            }
            else
            {
                _bankingViewModel.UpdateExpense();
            }
        }
    }
}
