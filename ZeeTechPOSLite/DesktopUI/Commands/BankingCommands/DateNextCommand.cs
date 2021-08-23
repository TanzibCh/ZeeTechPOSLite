using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DesktopUI.Commands.BankingCommands
{
    public class DateNextCommand : CommandBase
    {
        private readonly BankingViewModel _bankingViewModel;

        public DateNextCommand(BankingViewModel bankingViewModel)
        {
            _bankingViewModel = bankingViewModel;
        }

        public override void Execute(object parameter)
        {
            DateTime currentDate = _bankingViewModel.SelectedDate;

            if (currentDate != DateTime.Today)
            {
                var newDate = currentDate.AddDays(+1);
                _bankingViewModel.SelectedDate = newDate;
                _bankingViewModel.SaleProducts = null;
            }
            else
            {
                MessageBox.Show("There is no Banking done for tomorrow.");
            }
        }
    }
}
