using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class PayCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleVM;

        public PayCommand(ManualSaleViewModel manualSaleVM)
        {
            _manualSaleVM = manualSaleVM;
        }

        public override void Execute(object parameter)
        {
            _manualSaleVM.CompleteSale();
        }
    }
}
