using DesktopUI.Services;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.EditProductCommands
{
    public class UpdateEditedProductCommand : CommandBase
    {
        private readonly EditSaleProductViewModel _editSaleProductVM;
        private readonly INavigationService _navigationService;

        public UpdateEditedProductCommand(EditSaleProductViewModel editSaleProductVM, 
            INavigationService navigationService)
        {
            _editSaleProductVM = editSaleProductVM;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _editSaleProductVM.UpdateChanges();
            _navigationService.Navigate();
        }
    }
}
