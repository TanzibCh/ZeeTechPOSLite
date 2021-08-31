using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class RemoveFromCartCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleViewModel;

        public RemoveFromCartCommand(ManualSaleViewModel manualSaleViewModel)
        {
            _manualSaleViewModel = manualSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            _manualSaleViewModel.RemoveItemFromCart();
        }
    }
}
