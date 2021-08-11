using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.BankingCommands
{
    public class UpdateProductCommand : CommandBase
    {
        private readonly EditSaleProductViewModel _editSaleProductViewModel;
        private readonly ProductStore _productStore;
        private readonly INavigationService _navigationService;

        public UpdateProductCommand(EditSaleProductViewModel editSaleProductViewModel, ProductStore productStore,
            INavigationService navigationService)
        {
            _editSaleProductViewModel = editSaleProductViewModel;
            _productStore = productStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            var editedProduct = new SaleProductDisplayModel()
            {
                ProductName = _editSaleProductViewModel.ProductName,
                ProductDescription = _editSaleProductViewModel.ProductDescription
            };

            _productStore.EditedSaleProduct = editedProduct;

            _navigationService.Navigate();
        }
    }
}
