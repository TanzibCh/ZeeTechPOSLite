using DataAccessLibrary.DataAccess.Queries;
using DataAccessLibrary.Models;
using DesktopUI.Helpers;
using DesktopUI.Services;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUI.Commands.CreateProductCommands
{
    internal class CreateCommand : CommandBase
    {
        private readonly CreateProductViewModel _createProductViewModel;
        private readonly ProductData _productData = new ProductData();
        private readonly CurrencyHelper _currencyHelper = new CurrencyHelper();
        private readonly INavigationService _navigationService;

        public CreateCommand(CreateProductViewModel createProductViewModel, INavigationService navigationService)
        {
            _createProductViewModel = createProductViewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            if (String.IsNullOrWhiteSpace(_createProductViewModel.ProductName))
            {
                MessageBox.Show("Enter a Product name.");
            }
            else if (String.IsNullOrWhiteSpace(_createProductViewModel.ProductDescription))
            {
                MessageBox.Show("Enter a Product description.");
            }
            else if (_createProductViewModel.SelectedDepartment == null)
            {
                MessageBox.Show("Select a Department.");
            }
            else
            {
                _productData.AddNewProduct(CreateProductForDb());

                _navigationService.Navigate();
            }
        }
        
        private ProductModel CreateProductForDb()
        {
            ProductModel product = new ProductModel
            {
                ProductName = _createProductViewModel.ProductName,
                ProductDescription = _createProductViewModel.ProductDescription,
                Barcode = _createProductViewModel.Barcode,
                AverageCost = _currencyHelper.ConvertDecimalToInt(_createProductViewModel.AverageCost),
                Price = _currencyHelper.ConvertDecimalToInt(_createProductViewModel.Price),
                Department = _createProductViewModel.SelectedDepartment.ToString(),
                IsActive = 1
            };

            return product;
        }
    }
}
