using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class RemoveSearchedProductQuantityCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleVM;

        public RemoveSearchedProductQuantityCommand(ManualSaleViewModel manualSaleVM)
        {
            _manualSaleVM = manualSaleVM;
        }

        public override void Execute(object parameter)
        {
            RemoveQuantity();
        }

        private void RemoveQuantity()
        {
            if (_manualSaleVM.SelectedSearchedProduct != null)
            {
                if (_manualSaleVM.SearchedProductQuantity < 2)
                {
                    _manualSaleVM.SearchedProductQuantity = 1;
                }
                else if (_manualSaleVM.SearchedProductQuantity > 1)
                {
                    _manualSaleVM.SearchedProductQuantity -= 1;
                }
            }
            else
            {
                _manualSaleVM.SearchedProductQuantity = 0;
            }
        }
    }
}
