using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopUI.Commands.EditSaleCommands
{
    public class VoidSaleCommand : CommandBase
    {
        private readonly EditSaleViewModel _editSaleViewModel;

        public VoidSaleCommand(EditSaleViewModel editSaleViewModel)
        {
            _editSaleViewModel = editSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to Void this Sale?", "Void Sale", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _editSaleViewModel.VoidSale();
            }
            else
            {
                return;
            }
        }
    }
}
