using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.EditSaleCommands
{
    public class EditProductCommand : CommandBase
    {
        private readonly EditSaleViewModel _editSaleViewModel;

        public EditProductCommand(EditSaleViewModel editSaleViewModel)
        {
            _editSaleViewModel = editSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            _editSaleViewModel.SetupProductToEdit();
        }
    }
}
