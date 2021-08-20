using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DesktopUI.Commands.BankingCommands
{
    public class EditExpenseCommand : CommandBase
    {
        private readonly BankingViewModel _bankingViewModel;

        public EditExpenseCommand(BankingViewModel bankingViewModel)
        {
            _bankingViewModel = bankingViewModel;
        }

        public override void Execute(object parameter)
        {
            _bankingViewModel.EditSelectedExpense();
        }
    }
}
