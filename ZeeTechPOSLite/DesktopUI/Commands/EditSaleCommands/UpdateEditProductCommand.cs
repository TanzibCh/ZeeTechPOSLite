using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.ViewModels;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.EditSaleCommands
{
    public class UpdateEditProductCommand : CommandBase
    {
        private readonly EditSaleViewModel _editSaleViewModel;
        private readonly CurrencyHelper _currencyHelper = new CurrencyHelper();

        public UpdateEditProductCommand(EditSaleViewModel editSaleViewModel)
        {
            _editSaleViewModel = editSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_editSaleViewModel.EditQuantity != 0)
            {
                _editSaleViewModel.UpdateSoldProduct();
                _editSaleViewModel.ClearEditProduct();
                _editSaleViewModel.CalculateSaleCurrencies();
            }
            else
            {
                MessageBox.Show("Select a product to Edit first");
            }
            
        }
    }
}
