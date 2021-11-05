using DataAccessLibrary.DataAccess.Queries;
using DataAccessLibrary.Models;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class SearchBarcodeCommand : CommandBase
    {
        private readonly LocationStore _locationStore;
        private readonly ManualSaleViewModel _manualSaleViewModel;

        public SearchBarcodeCommand(ManualSaleViewModel manualSaleViewModel,
            LocationStore locationStore)
        {
            _manualSaleViewModel = manualSaleViewModel;
            _locationStore = locationStore;
        }

        private readonly ProductData _productData = new ProductData();

        public override void Execute(object parameter)
        {
            var products = _productData.SearchProductByBarcode(_locationStore.Id,
                _manualSaleViewModel.SearchBarcode);

            ObservableCollection<ProductSearchModel> searchedProducts = 
                new ObservableCollection<ProductSearchModel>();

            foreach (var product in products)
            {
                searchedProducts.Add(new ProductSearchModel
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Department = product.Department,
                    Barcode = product.Barcode,
                    AverageCost = product.AverageCost,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    TotalQuantity = product.TotalQuantity,
                    TotalSold = product.TotalSold,
                    IsActive = product.IsActive
                });
            }

            _manualSaleViewModel.SearchedProducts = searchedProducts;
        }
    }
}
