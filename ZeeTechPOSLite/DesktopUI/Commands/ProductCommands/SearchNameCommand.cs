using DataAccessLibrary.DataAccess.Queries;
using DataAccessLibrary.Models;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Commands.ProductCommands
{
    public class SearchNameCommand : CommandBase
    {
        private readonly LocationStore _locationStore;
        private readonly ProductViewModel _productViewModel;
        private readonly ProductData _productData = new ProductData();

        public SearchNameCommand(ProductViewModel productViewModel, LocationStore locationStore)
        {
            _productViewModel = productViewModel;
            _locationStore = locationStore;
        }

        public override void Execute(object parameter)
        {
            if (!String.IsNullOrWhiteSpace(_productViewModel.SearchName))
            {
                SearchByName();
            }
            else
            {
                _productViewModel.GetAllProducts();
            }
        }

        private void SearchByName()
        {
            var products = _productData.SearchProductByName(_locationStore.Id, _productViewModel.SearchName);

            ObservableCollection<ProductSearchModel> searchedProducts = new ObservableCollection<ProductSearchModel>();

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

            _productViewModel.SearchedProducts = searchedProducts;
        }
    }
}
