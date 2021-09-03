using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class EditCartItemCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleVM;
        private readonly ProductStore _productStore;
        private readonly INavigationService _navigationService;

        public EditCartItemCommand(ManualSaleViewModel manualSaleVM, 
            ProductStore productStore, INavigationService navigationService)
        {
            _manualSaleVM = manualSaleVM;
            _productStore = productStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            if (_manualSaleVM.Cart == null)
            {
                MessageBox.Show("There is no product added to the cart.");
            }
            else if (_manualSaleVM.SelectedCartItem == null)
            {
                MessageBox.Show("Select a product to deit first.");
            }
            else
            {
                _productStore.SelectedSaleProduct = _manualSaleVM.SelectedCartItem;

                _navigationService.Navigate();
            }
        }
    }
}
