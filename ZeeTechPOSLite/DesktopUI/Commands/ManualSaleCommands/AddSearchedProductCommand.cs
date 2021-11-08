using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class AddSearchedProductCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleVM;
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();

        public AddSearchedProductCommand(ManualSaleViewModel manualSaleViewModel)
        {
            _manualSaleVM = manualSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            // Create a product to store in the cartItem
            SaleProductDisplayModel product = new SaleProductDisplayModel
            {
                Id = _manualSaleVM.SelectedSearchedProduct.Id,
                ProductName = _manualSaleVM.SelectedSearchedProduct.ProductName,
                ProductDescription = _manualSaleVM.SelectedSearchedProduct.ProductDescription,
                ProductCost = _cHelper.ConvertIntToCurrencyString(_manualSaleVM.SelectedSearchedProduct.AverageCost),
                SalePrice = _cHelper.ConvertDecimalToString(_manualSaleVM.SearchedProductPrice),
                Department = _manualSaleVM.SelectedSearchedProduct.Department
            };

            decimal total = _manualSaleVM.SearchedProductQuantity * _manualSaleVM.SearchedProductPrice;

            // Create a cart item to store and display in the cart
            CartItemDisplayModel item = new CartItemDisplayModel
            {
                Product = product,
                Quantity = _manualSaleVM.SearchedProductQuantity,
                Price = _cHelper.ConvertDecimalToString(_manualSaleVM.SearchedProductPrice),
                Total = _cHelper.ConvertDecimalToString(total)
            };

            _manualSaleVM.Cart.Add(item);
            _manualSaleVM.CalculatePayments();
        }
    }
}
