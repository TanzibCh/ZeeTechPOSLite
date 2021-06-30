using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DesktopUI.Models;

namespace DesktopUI.Commands.BankingCommands
{
    public class EditSaleCommand : ICommand
    {
        public BankingViewModel BankingVM { get; set; }

        public EditSaleCommand(BankingViewModel bankingVM)
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
            SaleDisplayModel sale = BankingVM.SelectedSale;
            EditSaleViewModel editSaleVM = new EditSaleViewModel(sale.Id);

            Window win = new Window
        }
    }
}
