using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.RefundCommands
{
    public class CompleteCommand : CommandBase
    {
        private readonly CurrencyHelper _currencyHelper = new CurrencyHelper();
        private readonly INavigationService _navigationService;
        private readonly ReturnStore _returnStore;
        private readonly RefundViewModel _refundViewModel;

        public CompleteCommand(INavigationService navigationService,
            ReturnStore returnStore, RefundViewModel refundViewModel)
        {
            _navigationService = navigationService;
            _returnStore = returnStore;
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _returnStore.SaleId = _refundViewModel.SaleId;
            _returnStore.Amount = _refundViewModel.RefundTotal;
            foreach (SaleProductDisplayModel product in _refundViewModel.ReturnProducts)
            {
                _returnStore.ReturnProducts.Add(new SaleProductDisplayModel
                {
                    Id = product.Id,
                    SaleId = product.SaleId,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductCost = product.ProductCost,
                    SalePrice = product.SalePrice,
                    QuantitySold = product.QuantitySold,
                    Total = product.Total,
                    Department = product.Department
                });
            }

            _navigationService.Navigate();
        }
    }
}