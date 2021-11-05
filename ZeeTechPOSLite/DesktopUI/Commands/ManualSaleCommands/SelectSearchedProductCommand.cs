using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class SelectSearchedProductCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleViewModel;

        public SelectSearchedProductCommand(ManualSaleViewModel manualSaleViewModel)
        {
            _manualSaleViewModel = manualSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            _manualSaleViewModel.SearchedProductQuantity = 1;
            _manualSaleViewModel.SearchedProductPrice = _manualSaleViewModel.SelectedSearchedProduct.Price;
        }
    }
}
