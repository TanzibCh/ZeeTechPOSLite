using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class AddSearchedProductQuantityCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleVM;

        public AddSearchedProductQuantityCommand(ManualSaleViewModel manualSaleVM)
        {
            _manualSaleVM = manualSaleVM;
        }

        public override void Execute(object parameter)
        {
            AddQuantity();
        }

        private void AddQuantity()
        {
            int localQuantity = _manualSaleVM.SelectedSearchedProduct.Quantity;

            if (_manualSaleVM.SearchedProductQuantity == localQuantity)
            {
                if (MessageBox.Show("Do not have enough stock in current location. Do you still Add more?",
                    "Quantity Limit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _manualSaleVM.SearchedProductQuantity += 1;
                }
            }
            else
            {
                _manualSaleVM.SearchedProductQuantity += 1;
            }
        }
    }
}
