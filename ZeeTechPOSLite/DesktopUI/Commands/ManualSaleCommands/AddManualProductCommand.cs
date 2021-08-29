using DataAccessLibrary.Models;
using DesktopUI.Models;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class AddManualProductCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleViewModel;

        public AddManualProductCommand(ManualSaleViewModel manualSaleViewModel)
        {
            _manualSaleViewModel = manualSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            _manualSaleViewModel.AddToCartManualItem();
        }
    }
}
