using DesktopUI.ViewModels;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.EditSaleCommands
{
    public class CancelEditProductCommand : CommandBase
    {
        private readonly EditSaleViewModel _editSaleViewModel;

        public CancelEditProductCommand(EditSaleViewModel editSaleViewModel)
        {
            _editSaleViewModel = editSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            _editSaleViewModel.ClearEditProduct();
        }
    }
}
