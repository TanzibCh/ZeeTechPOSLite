using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.BankingCommands
{
    public class DateBackCommand : CommandBase
    {
        private readonly BankingViewModel _bankingViewModel;

        public DateBackCommand(BankingViewModel bankingViewModel)
        {
            _bankingViewModel = bankingViewModel;
        }

        public override void Execute(object parameter)
        {
            DateTime currentDate = _bankingViewModel.SelectedDate;
            DateTime newdate = currentDate.AddDays(-1);
            _bankingViewModel.SelectedDate = newdate;
            _bankingViewModel.SaleProducts = null;
        }
    }
}
