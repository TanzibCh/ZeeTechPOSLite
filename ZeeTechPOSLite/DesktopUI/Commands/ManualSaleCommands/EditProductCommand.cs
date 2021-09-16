using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class EditProductCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleViewModel;
        private readonly ProductStore _productStore;
        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
