using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopUI.Commands.EditSaleCommands
{
    public class SaveChangesCommand : CommandBase
    {
        private readonly EditSaleViewModel _editSaleViewModel;

        public SaveChangesCommand(EditSaleViewModel editSaleViewModel)
        {
            _editSaleViewModel = editSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to save the changes?", "Save Changes", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _editSaleViewModel.SaveChanges();
            }
            else
            {
                return;
            }
        }
    }
}
