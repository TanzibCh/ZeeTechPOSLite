using DataAccessLibrary.Models;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class PayCommand : CommandBase
    {
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();
        private readonly ManualSaleViewModel _manualSaleVM;
        private readonly INavigationService _navigationService;
        private readonly SaleStore _saleStore;

        public PayCommand(ManualSaleViewModel manualSaleVM,
            INavigationService navigationService, SaleStore saleStore)
        {
            _manualSaleVM = manualSaleVM;
            _navigationService = navigationService;
            _saleStore = saleStore;

            _manualSaleVM.PropertyChanged += OnVeiwModelPropertyChanged;
        }

        public override void Execute(object parameter)
        {
            OpenPaymentView();
        }

        public override bool CanExecute(object parameter)
        {
            return _manualSaleVM.CartIsEmpty == false  && base.CanExecute(parameter);
        }

        private void OpenPaymentView()
        {
            if (_manualSaleVM.Cart != null)
            {
                SetValuesInSaleStore();

                _navigationService.Navigate();
            }
            else
            {
                return;
            }
        }

        // Takes values from NewSaleVM and stores them in SaleStore
        private void SetValuesInSaleStore()
        {
            _saleStore.SubTotal = _cHelper.ConvertStringToDecimal(_manualSaleVM.Subtotal);
            _saleStore.Tax = _cHelper.ConvertStringToDecimal(_manualSaleVM.Tax);
            _saleStore.Total = _cHelper.ConvertStringToDecimal(_manualSaleVM.CartTotal);
            _saleStore.TotalCost = _manualSaleVM.CalculateTotalCartCost();
            _saleStore.TotalPtofit = _manualSaleVM.CalculateCartProfit();
            _saleStore.SaleProductsForDB = CreateSaleProductsForDB();
        }

        // Creates a list of SaleProduct object to sae in the SaleStore so that it
        // can be available when saving the sale in the database
        private List<SaleProductModel> CreateSaleProductsForDB()
        {
            List<SaleProductModel> saleProducts = new List<SaleProductModel>();

            foreach (CartItemDisplayModel item in _manualSaleVM.Cart)
            {
                saleProducts.Add(new SaleProductModel
                {
                    ProductId = item.Product.Id,
                    ProductName = item.Product.ProductName,
                    ProductDescription = item.Product.ProductDescription,
                    ProductCost = _cHelper.ConvertStringToInt(item.Product.ProductCost),
                    SalePrice = _cHelper.ConvertStringToInt(item.Price),
                    QuantitySold = item.Quantity,
                    Department = item.Product.Department
                });
            }

            return saleProducts;
        }

        private void OnVeiwModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_manualSaleVM.CartIsEmpty))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
