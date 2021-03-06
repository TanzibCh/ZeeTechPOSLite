using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.BankingCommands
{
    public class ClearExpenseCommand : ICommand
    {
        public BankingViewModel BankingVM { get; set; }
        public ClearExpenseCommand(BankingViewModel bankingVM)
        {
            BankingVM = bankingVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            BankingVM.ClearExpenseFIelds();
        }
    }
}
