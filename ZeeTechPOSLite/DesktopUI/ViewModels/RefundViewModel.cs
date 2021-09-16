using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Commands;
using DesktopUI.Commands.RefundCommands;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class RefundViewModel : ViewModelBase
    {
        #region Private Properties

        private readonly CurrencyHelper _cHelper = new CurrencyHelper();
        private readonly SaleProductData _saleProductData = new SaleProductData();
        private readonly SalesData _salesData = new SalesData();
        private readonly SaleStore _saleStore;

        #endregion Private Properties

        #region Field Properties

        private decimal _productTotal;
        private int _quantity;
        private decimal _refundTotal;
        private decimal _restockingCharge;
        private decimal _saleTotal;

        public bool AddQuantityEnabled { get; set; } = true;
        public bool RemoveQuantityEnabled { get; set; } = true;
        public bool QuantityEnabled { get; set; } = true;
        public bool AddButtonEnabled { get; set; } = true;

        private bool _cardPayment;

        public bool CardPayment
        {
            get { return _cardPayment; }
            set { _cardPayment = value; }
        }

        private bool _cashPayment;

        public bool CashPayment
        {
            get { return _cashPayment; }
            set { _cashPayment = value; }
        }

        public decimal ProductTotal
        {
            get { return _productTotal; }
            set
            {
                _productTotal = value;
                OnPropertyChanged(nameof(ProductTotal));
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                CheckQuantity();
                ProductTotal = Math.Round(CalculateProductTotal(SelectedSaleProduct, Quantity), 2);
            }
        }

        public decimal RefundTotal
        {
            get { return _refundTotal; }
            set
            {
                _refundTotal = value;
                OnPropertyChanged(nameof(RefundTotal));
            }
        }

        public decimal RestockingCharge
        {
            get { return _restockingCharge; }
            set
            {
                _restockingCharge = value;
                OnPropertyChanged(nameof(RestockingCharge));
                ProductTotal = CalculateProductTotal(SelectedSaleProduct, Quantity);
            }
        }

        public int SaleId => _saleStore.SelectedSale.Id;

        public decimal SaleTotal
        {
            get { return _saleTotal; }
            set
            {
                _saleTotal = value;
                OnPropertyChanged(nameof(SaleTotal));
            }
        }

        #endregion Field Properties

        #region Commands

        public ICommand AddAllCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand AddQuantityCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand CreditCommand { get; }
        public ICommand GetProductInfoCommand { get; }
        public ICommand RefundAllCommand { get; }
        public ICommand RefundCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand RemoveQuantityCommand { get; }
        public ICommand UndoCommand { get; }

        #endregion Commands

        #region List Properties

        private ObservableCollection<SaleProductDisplayModel> _refundProducts;
        private ObservableCollection<SaleProductDisplayModel> _saleProducts;

        private SaleProductDisplayModel _selectedRefundProduct;

        private SaleProductDisplayModel _selectedSaleProduct;

        public ObservableCollection<SaleProductDisplayModel> RefundProducts
        {
            get { return _refundProducts; }
            set
            {
                _refundProducts = value;
                OnPropertyChanged(nameof(RefundProducts));
            }
        }

        public ObservableCollection<SaleProductDisplayModel> SaleProducts
        {
            get { return _saleProducts; }
            set
            {
                _saleProducts = value;
                OnPropertyChanged(nameof(SaleProducts));
            }
        }

        public SaleProductDisplayModel SelectedRefundProduct
        {
            get { return _selectedRefundProduct; }
            set
            {
                _selectedRefundProduct = value;
                OnPropertyChanged(nameof(SelectedRefundProduct));
            }
        }

        public SaleProductDisplayModel SelectedSaleProduct
        {
            get { return _selectedSaleProduct; }
            set
            {
                _selectedSaleProduct = value;
                OnPropertyChanged(nameof(SelectedSaleProduct));
                SelectSaleProduct();
            }
        }

        #endregion List Properties

        #region Constructor

        public RefundViewModel(INavigationService closeModalNavigationService, SaleStore saleStore)
        {
            _saleStore = saleStore;
            RefundProducts = new ObservableCollection<SaleProductDisplayModel>();

            // Commands
            GetProductInfoCommand = new GetProductInfoCommand(this);
            AddCommand = new AddCommand(this);
            AddAllCommand = new AddAllCommand(this);
            RemoveCommand = new RemoveCommand(this);
            CloseCommand = new CloseModalCommand(closeModalNavigationService);
            RefundCommand = new CreateRefundCommand(this);
            AddQuantityCommand = new AddQuantityCommand(this);
            RemoveQuantityCommand = new RemoveQuantityCommand(this);
            UndoCommand = new UndoChangesCommand(this);

            GetAllProductsInSale();
        }

        #endregion Constructor

        #region Methods

        // Adds all the sale products to the refund list to process the refund
        public void AddAllSaleProducts()
        {
            if (RestockingCharge == 0m)
            {
                MessageBoxResult result = MessageBox.Show("Is there a restocking charge?", "", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // focus on RestockingCharge property/Field
                    return;
                }
            }

            // Empty out Refund List before adding all the products to the list
            RefundProducts.Clear();

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
                    Total = _cHelper.ConvertDecimalToString(CalculateProductTotal(product, product.QuantitySold)),
                    Department = product.Department
                });
            }

            RefundTotal = CalculateTotalRefund();
            ToggleAddFields();
        }

        // enables and disables the edit fields
        private void ToggleAddFields()
        {
            if (AddQuantityEnabled && RemoveQuantityEnabled &&
                AddButtonEnabled && AddQuantityEnabled)
            {
                AddQuantityEnabled = false;
                RemoveQuantityEnabled = false;
                QuantityEnabled = false;
                AddButtonEnabled = false;
            }
            else
            {
                AddQuantityEnabled = true;
                RemoveQuantityEnabled = true;
                QuantityEnabled = true;
                AddButtonEnabled = true;
            }
        }

        // Method for add qty command
        public void AddQuantity()
        {
            if (SelectedSaleProduct?.QuantitySold > Quantity)
            {
                Quantity += 1;
            }
        }

        // Add item to RefundProducts List
        public void AddToRefundList()
        {
            if (SelectedSaleProduct != null)
            {
                SaleProductDisplayModel refundProduct =
                    RefundProducts.FirstOrDefault(p => p.Id == SelectedSaleProduct.Id);

                if (refundProduct != null)
                {
                    refundProduct.QuantitySold = QuantityAddition(refundProduct.QuantitySold);
                    refundProduct.Total = _cHelper.ConvertDecimalToString(
                        CalculateProductTotal(SelectedSaleProduct, refundProduct.QuantitySold));

                    CollectionViewSource.GetDefaultView(RefundProducts).Refresh();
                    RefundTotal = CalculateTotalRefund();
                }
                else
                {
                    SaleProductDisplayModel product = new SaleProductDisplayModel
                    {
                        Id = SelectedSaleProduct.Id,
                        SaleId = SelectedSaleProduct.SaleId,
                        ProductId = SelectedSaleProduct.ProductId,
                        ProductName = SelectedSaleProduct.ProductName,
                        ProductDescription = SelectedSaleProduct.ProductDescription,
                        ProductCost = SelectedSaleProduct.ProductCost,
                        SalePrice = SelectedSaleProduct.SalePrice,
                        QuantitySold = Quantity,
                        Total = _cHelper.ConvertDecimalToString(ProductTotal),
                        Department = SelectedSaleProduct.Department
                    };

                    RefundProducts.Add(product);
                    RefundTotal = CalculateTotalRefund();
                }
            }
        }

        // Removes an item form the RefundProducts List
        public void RemoveFromRefundList()
        {
            if (SelectedRefundProduct != null)
            {
                SaleProductDisplayModel refundProduct =
                    RefundProducts.FirstOrDefault(p => p.Id == SelectedRefundProduct.Id);

                if (refundProduct != null)
                {
                    refundProduct.QuantitySold = QuantitySubruction(refundProduct.QuantitySold);
                    refundProduct.Total = _cHelper.ConvertDecimalToString(
                        CalculateProductTotal(SelectedRefundProduct, refundProduct.QuantitySold));

                    if (refundProduct.QuantitySold == 0)
                    {
                        RefundProducts.Remove(refundProduct);
                    }

                    CollectionViewSource.GetDefaultView(RefundProducts).Refresh();
                    RefundTotal = CalculateTotalRefund();
                }
            }
        }

        // Deducts quantity from the Refund list by the amount in the Quantitt field
        private int QuantitySubruction(int refundProductQuantity)
        {
            int newQuantity = refundProductQuantity - Quantity;

            if (newQuantity < 0)
            {
                newQuantity = 0;
            }

            return newQuantity;
        }

        // Checks if the Selected refund products qty is more than the sale product qty
        // if greater then adds then returns sale product qty.
        // Otherwise returns Quantity + refund product qty.
        private int QuantityAddition(int refundProductQuantity)
        {
            int newQuantity = Quantity + refundProductQuantity;

            if (newQuantity > SelectedSaleProduct.QuantitySold)
            {
                newQuantity = SelectedSaleProduct.QuantitySold;
            }

            return newQuantity;
        }

        //On compleation Create Note/Voucher and print
        public void CreateCredit()
        {
            // save to database as a new credit/voucher
        }

        // On compleation Create Refund and print
        public void CreateRefund()
        {
            if (CardPayment == false && CashPayment == false)
            {
                MessageBox.Show("Please select a method of payment.");

                return;
            }

            //
            MessageBox.Show("Refund created");
            // save to database as a new sale, with nagative value
            //_salesData.SaveSale(, CreateProductForRefund);
            // of the amount refunded.
            // In the description of the product add Invoice no. of the
            // original Invoice the refund is made from.
        }

        // Method for removing qty command
        public void RemoveQuantity()
        {
            if (Quantity > 1)
            {
                Quantity -= 1;
            }
        }

        // Get Information of the Selected product
        public void SelectSaleProduct()
        {
            if (Quantity > SelectedSaleProduct.QuantitySold)
            {
                Quantity = SelectedSaleProduct.QuantitySold;
            }
            else
            {
                Quantity = 1;
            }

            CalculateProductTotal(SelectedSaleProduct, Quantity);
        }

        // Undo all changes made
        public void UndoChanges()
        {
            RefundProducts.Clear();
            ClearEditFields();
        }

        // Calculates Selected sale product Total amount to refund
        private decimal CalculateProductTotal(SaleProductDisplayModel product, int quantity)
        {
            decimal output = 0m;
            if (product != null)
            {
                decimal total = Convert.ToDecimal(quantity) * _cHelper.ConvertStringToDecimal(product.SalePrice);
                decimal totalRestockingCharge = Convert.ToDecimal(quantity) * GetProdctRestockingCharge(product);

                output = total - totalRestockingCharge;
            }
            return output;
        }

        // Calculates the Total amount to refund
        private decimal CalculateTotalRefund()
        {
            decimal total = 0;

            foreach (SaleProductDisplayModel product in RefundProducts)
            {
                total += _cHelper.ConvertStringToDecimal(product.Total);
            }

            return total;
        }

        // Checks if the Quantity is more than the selected products quantity
        private void CheckQuantity()
        {
            if (Quantity > SelectedSaleProduct?.QuantitySold)
            {
                Quantity = SelectedSaleProduct.QuantitySold;
            }
            else if (Quantity <= 0)
            {
                Quantity = 1;
            }
        }

        // Clears Editable Fields
        private void ClearEditFields()
        {
            Quantity = 0;
            RestockingCharge = 0m;
            ProductTotal = 0m;
        }

        // Creats new product to use in refund
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

        // Calculate the amount to deduct after restocking charge
        private decimal GetProdctRestockingCharge(SaleProductDisplayModel product)
        {
            decimal percentage = RestockingCharge / 100m;

            return _cHelper.ConvertStringToDecimal(product.SalePrice) * percentage;
        }

        // Remove selected item from RefundProducts list
        private void RemoveProductFromRefundProducts()
        {
            RefundProducts.Remove(SelectedRefundProduct);
            ClearEditFields();
        }

        private void CreateSale()
        {
            SaleModel sale = new SaleModel();
            //{
            //    Card = _cHelper.ConvertStringToInt(CardPayment),
            //    Cash = _cHelper.ConvertStringToInt(CashPayment),
            //    Credit = _cHelper.ConvertStringToInt(CreditPayment),
            //    SaleTotal = _cHelper.ConvertStringToInt(CartTotal),
            //    Tax = _cHelper.ConvertStringToInt(Tax),
            //    TotalCost = _cHelper.ConvertDecimalToInt(CalculateTotalCartCost()),
            //    Profit = _cHelper.ConvertDecimalToInt(CalculateCartProfit()),
            //    CashOnly = cashOnly
            //};

            //return sale;
        }

        public void ClearSelectedSale()
        {
            _saleStore.SelectedSale = null;
        }

        #endregion Methods
    }
}