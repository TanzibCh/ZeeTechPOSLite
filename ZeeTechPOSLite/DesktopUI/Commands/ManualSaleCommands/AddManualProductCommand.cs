using DataAccessLibrary.Models;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class AddManualProductCommand : CommandBase
    {
        private readonly ManualSaleViewModel _manualSaleViewModel;
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();

        public AddManualProductCommand(ManualSaleViewModel manualSaleViewModel)
        {
            _manualSaleViewModel = manualSaleViewModel;
        }

        public override void Execute(object parameter)
        {
            AddManualItemToCart();
        }

        /// <summary>
        /// Takes the created product and adds it to the cart
        /// also adds the quantity entered and calculates the 
        /// total amount for that product
        /// </summary>
        public void AddManualItemToCart()
        {
            if (string.IsNullOrWhiteSpace(_manualSaleViewModel.ManualProductName))
            {
                MessageBox.Show("You need to enter Product Name");
            }
            else if (_manualSaleViewModel.SelectedDepartment == null)
            {
                MessageBox.Show("You need to select a Department");
            }
            else if (_manualSaleViewModel.ManualQuantity < 1)
            {
                MessageBox.Show("You need to enter a Quantity");
            }
            else
            {
                SaleProductDisplayModel product = _manualSaleViewModel.CreateManualProduct();

                decimal total = _manualSaleViewModel.ManualQuantity *
                    _cHelper.ConvertStringToDecimal(_manualSaleViewModel.ManualPrice);

                CartItemDisplayModel item = new CartItemDisplayModel
                {
                    Product = product,
                    Quantity = _manualSaleViewModel.ManualQuantity,
                    Price = _manualSaleViewModel.ManualPrice,
                    Total = _cHelper.ConvertDecimalToString(total)
                };

                _manualSaleViewModel.Cart.Add(item);

                if (_manualSaleViewModel.CartIsEmpty == true)
                {
                    _manualSaleViewModel.CartIsEmpty = false;
                }

                _manualSaleViewModel.CalculatePayments();

                _manualSaleViewModel.ClearManualFields();
            }
        }
    }
}
