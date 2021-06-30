using DesktopUI.Models;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.BankingCommands
{
    public class EditExpenseCommand : ICommand
    {
        public BankingViewModel BankingVM { get; set; }

        public EditExpenseCommand(BankingViewModel bankingVM)
        {
            BankingVM = bankingVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            ExpenseDisplayModel expense = parameter as ExpenseDisplayModel;

            if (expense == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            BankingVM.EditSelectedExpense();
        }
    }
}
