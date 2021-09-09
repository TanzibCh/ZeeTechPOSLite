using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Commands;
using DesktopUI.Commands.EditSaleCommands;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class RefundViewModel : ViewModelBase
    {
        #region Private Properties

        private readonly SaleStore _saleStore;
        private readonly SaleProductData _saleProductData;
        private readonly CurrencyHelper _cHelper;
        private readonly SalesData _salesData;

        #endregion

        #region Public properties

        public int SaleId => _saleStore.SelectedSale.Id;

        private decimal _saleTotal;

        public decimal SaleTotal
        {
            get { return _saleTotal; }
            set
            {
                _saleTotal = value;
                OnPropertyChanged(nameof(SaleTotal));
            }
        }

        private decimal _restockingCharge;

        public decimal RestockingCharge
        {
            get { return _restockingCharge; }
            set
            {
                _restockingCharge = value;
                OnPropertyChanged(nameof(RestockingCharge));
            }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private decimal _productTotal;

        public decimal ProductTotal
        {
            get { return _productTotal; }
            set
            {
                _productTotal = value;
                OnPropertyChanged(nameof(ProductTotal));
            }
        }


        private decimal _refundTotal;

        public decimal RefundTotal
        {
            get { return _refundTotal; }
            set
            {
                _refundTotal = value;
                OnPropertyChanged(nameof(RefundTotal));
            }
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand CreditCommand { get; }
        public ICommand RefundCommand { get; }
        public ICommand AddQuantityCommand { get; }
        public ICommand RemoveQuantityCommand { get; }
        public ICommand RefundAllCommand { get; }

        #endregion

        #region List Properties

        private ObservableCollection<SaleProductDisplayModel> _saleProducts;

        public ObservableCollection<SaleProductDisplayModel> SaleProducts
        {
            get { return _saleProducts; }
            set
            {
                _saleProducts = value;
                OnPropertyChanged(nameof(SaleProducts));
            }
        }

        private ObservableCollection<SaleProductDisplayModel> _refundProducts;

        public ObservableCollection<SaleProductDisplayModel> RefundProducts
        {
            get { return _refundProducts; }
            set
            {
                _refundProducts = value;
                OnPropertyChanged(nameof(RefundProducts));
            }
        }

        private SaleProductDisplayModel _selectedSaleProduct;

        public SaleProductDisplayModel SelectedSaleProduct
        {
            get { return _selectedSaleProduct; }
            set
            {
                _selectedSaleProduct = value;
                OnPropertyChanged(nameof(SelectedSaleProduct));
            }
        }

        private SaleProductDisplayModel _selectedRefundProduct;

        public SaleProductDisplayModel SelectedRefundProduct
        {
            get { return _selectedRefundProduct; }
            set
            {
                _selectedRefundProduct = value;
                OnPropertyChanged(nameof(SelectedRefundProduct));
            }
        }

        #endregion

        #region Constructors

        public RefundViewModel(INavigationService closeModalNavigationService, SaleStore saleStore)
        {
            _saleStore = saleStore;

            // Commands
            CloseCommand = new CloseModalCommand(closeModalNavigationService);
            RefundCommand = new RefundCommand(closeModalNavigationService);
        }

        #endregion

        #region Methods

        // gets all the Products in the selected sale and generates the SaleProduct List
        private void GetAllProductsInSale()
        {
            List<SaleProductModel> products = _saleProductData.GetSaleProductBySaleId(SaleId);

            List<SaleProductDisplayModel> displayProducts = new List<SaleProductDisplayModel>();

            foreach (SaleProductModel item in products)
            {
                displayProducts.Add(new SaleProductDisplayModel
                {
                    Id = item.Id,
                    SaleId = item.SaleId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductDescription = item.ProductDescription,
                    ProductCost = _cHelper.ConvertIntToCurrencyString(item.ProductCost),
                    SalePrice = _cHelper.ConvertIntToCurrencyString(item.SalePrice),
                    QuantitySold = item.QuantitySold,
                    Total = _cHelper.ConvertIntToCurrencyString(item.Total),
                    Department = item.Department
                });

                SaleProducts = new ObservableCollection<SaleProductDisplayModel>(displayProducts);
            }
        }

        // Method for add qty command
        public void AddQuantity()
        {
            int currentQuantity = Quantity;

            int soldQuantity = SelectedSaleProduct.QuantitySold;

            if (soldQuantity > currentQuantity)
            {
                Quantity += 1;
            }
        }

        // Method for removing qty command
        public void RemoveQuantity()
        {
            int currentQuantity = Quantity;

            int soldQuantity = SelectedSaleProduct.QuantitySold;

            if (currentQuantity >= 1)
            {
                Quantity -= 1;
            }
        }

        // Get Information of the Selected product
        private void SelectSaleProduct()
        {
            if (Quantity < SelectedSaleProduct.QuantitySold)
            {
                Quantity += 1;
            }

            CalculateProductTotal();
        }

        // Calculate the amount to deduct after restocking charge
        private decimal GetProdctRestockingCharge()
        {
            decimal percentage = RestockingCharge / 100m;

            return _cHelper.ConvertStringToDecimal(SelectedSaleProduct.SalePrice) * percentage;
        }

        private void CalculateProductTotal()
        {
            decimal total = Quantity * _cHelper.ConvertStringToDecimal(SelectedSaleProduct.SalePrice);
            decimal totalRestckCharge = Quantity * GetProdctRestockingCharge();

            ProductTotal = total - totalRestckCharge;
        }

        // Add item to RefundProducts List
        private void AddToRefundList()
        {
            var product = new SaleProductDisplayModel
            {
                Id = SelectedSaleProduct.Id,
                SaleId = SelectedSaleProduct.SaleId,
                ProductId = SelectedSaleProduct.ProductId,
                ProductName = SelectedSaleProduct.ProductName,
                ProductDescription = SelectedSaleProduct.ProductDescription,
                ProductCost = SelectedSaleProduct.ProductCost,
                SalePrice = SelectedSaleProduct.SalePrice,
                QuantitySold = SelectedSaleProduct.QuantitySold,
                Total = _cHelper.ConvertDecimalToString(ProductTotal),
                Department = SelectedSaleProduct.Department
            };

            RefundProducts.Add(product);
        }

        // Remove selected item from RefundProducts list
        private void RemoveProductFromRefundProducts()
        {
            RefundProducts.Remove(SelectedRefundProduct);
            ClearEditFields();
        }

        // Clears Editable Fields
        private void ClearEditFields()
        {
            Quantity = 0;
            RestockingCharge = 0m;
            ProductTotal = 0m;
        }

        // Undo all changes made
        private void UndoChanges()
        {
            RefundProducts.Clear();
            ClearEditFields();
            SelectedSaleProduct = null;
        }

        public void SelectAll()
        {
            if (RestockingCharge == 0m)
            {
                MessageBoxResult result = MessageBox.Show("Is there a restocking charge?", "", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // focus on RestockingCharge property/Field
                }
            }

            foreach (SaleProductDisplayModel product in SaleProducts)
            {
                RefundProducts.Add(new SaleProductDisplayModel
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

        }

        // On compleation Create Note/Voucher and print
        public void CreateCredit()
        {
            // save to database as a new credit/voucher
        }

        // On compleation Create Refund and print
        public void CreateRefund()
        {
            // save to database as a new sale, with nagative value
            _salesData.SaveSale(, CreateProductForRefund);
            // of the amount refunded.
            // In the description of the product add Invoice no. of the
            // original Invoice the refund is made from.
        }

        private SaleModel CreateSale()
        {
            SaleModel sale = new SaleModel
            {
                Card = _cHelper.ConvertStringToInt(CardPayment),
                Cash = _cHelper.ConvertStringToInt(CashPayment),
                Credit = _cHelper.ConvertStringToInt(CreditPayment),
                SaleTotal = _cHelper.ConvertStringToInt(CartTotal),
                Tax = _cHelper.ConvertStringToInt(Tax),
                TotalCost = _cHelper.ConvertDecimalToInt(CalculateTotalCartCost()),
                Profit = _cHelper.ConvertDecimalToInt(CalculateCartProfit()),
                CashOnly = cashOnly
            };

            return sale;
        }

        private List<SaleProductModel> CreateProductForRefund()
        {
            List<SaleProductModel> refundProduct = new List<SaleProductModel>();

            refundProduct.Add(new SaleProductModel
            {
                ProductId = -1,
                ProductName = $"Refund ref: Invoice no. {_saleStore.SelectedSale.InvoiceNo}",
                ProductCost = 0,
                SalePrice = _cHelper.ConvertDecimalToInt(RefundTotal),
                QuantitySold = 1,
                Department = "Refund"
            });

            return refundProduct;
        }
        #endregion

    }
}
