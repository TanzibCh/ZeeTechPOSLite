using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Commands;
using DesktopUI.Commands.BankingCommands;
using DesktopUI.Commands.EditSaleCommands;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class EditSaleViewModel : ViewModelBase
    {
        #region Private properties

        private SalesData _salesData = new SalesData();
        private SaleProductData _saleProductData = new SaleProductData();
        private CurrencyHelper _currencyHelper = new CurrencyHelper();
        private readonly SaleStore _saleStore;

        #endregion

        #region Sales info Display properties

        public int Id => _saleStore.SelectedSale.Id;

        public int InvoiceNo => _saleStore.SelectedSale.InvoiceNo;

        public string InvoiceDate => _saleStore.SelectedSale.SaleDate;

        public string InvoiceTime => _saleStore.SelectedSale.SaleTime;

        private string _totalCost;

        public string TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        private string _totalProfit;

        public string TotalProfit
        {
            get { return _totalProfit; }
            set
            {
                _totalProfit = value;
                OnPropertyChanged(nameof(TotalProfit));
            }
        }

        private string _saleTotal;

        public string SaleTotal
        {
            get { return _saleTotal; }
            set 
            { 
                _saleTotal = value;
                OnPropertyChanged(nameof(SaleTotal));
            }
        }

        private string _card;

        public string Card
        {
            get { return _card; }
            set
            {
                _card = value;
                OnPropertyChanged(nameof(Card));
            }
        }

        private string _cash;

        public string Cash
        {
            get { return _cash; }
            set 
            {
                _cash = value;
                OnPropertyChanged(nameof(Cash));
            }
        }

        private string _credit;

        public string Credit
        {
            get { return _credit; }
            set 
            {
                _credit = value;
                OnPropertyChanged(nameof(Credit));
            }
        }

        #endregion

        #region Sale Products Display properties

        private BindingList<SaleProductDisplayModel> _saleProducts;

        public BindingList<SaleProductDisplayModel> SaleProducts
        {
            get { return _saleProducts; }
            set
            {
                _saleProducts = value;
                OnPropertyChanged(nameof(SaleProducts));

            }
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string _productDescription;

        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }

        private int _quantitySold;

        public int QuantitySold
        {
            get { return _quantitySold; }
            set 
            {
                _quantitySold = value;
                OnPropertyChanged(nameof(QuantitySold));
                CalculateProductTotal();
            }
        }

        private string _productCost;

        public string ProductCost
        {
            get { return _productCost; }
            set
            {
                _productCost = value;
                OnPropertyChanged(nameof(ProductCost));
            }
        }

        private string _salePrice;

        public string SalePrice
        {
            get { return _salePrice; }
            set 
            { 
                _salePrice = value;
                OnPropertyChanged(nameof(SalePrice));
                CalculateProductTotal();
            }
        }

        public string Total { get; set; }

        private SaleProductDisplayModel _selectedProduct;

        public SaleProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        #endregion

        #region Edit Product Properties

        private ObservableCollection<DepartmantModel> _departments;

        public ObservableCollection<DepartmantModel> Departments
        {
            get { return _departments; }
            set 
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private string _editName;

        public string EditName
        {
            get { return _editName; }
            set
            {
                _editName = value;
                OnPropertyChanged(nameof(EditName));
            }
        }

        private string _editDescription;

        public string EditDescription
        {
            get { return _editDescription; }
            set
            {
                _editDescription = value;
                OnPropertyChanged(nameof(EditDescription));
            }
        }

        private string _editCost;

        public string EditCost
        {
            get { return _editCost; }
            set
            {
                _editCost = value;
                OnPropertyChanged(nameof(EditCost));
            }
        }

        private string _editPrice;

        public string EditPrice
        {
            get { return _editPrice; }
            set
            {
                _editPrice = value;
                OnPropertyChanged(nameof(EditPrice));
            }
        }

        private int _editQuantity;

        public int EditQuantity
        {
            get { return _editQuantity; }
            set
            {
                _editQuantity = value;
                OnPropertyChanged(nameof(EditQuantity));
            }
        }

        public int SelectedProductIndex { get; }

        #endregion

        #region Command Properties

        public ICommand CloseCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand CancelEditProductCoomand { get; }
        public ICommand UpdateEditProductCommand { get; }

        #endregion

        #region Constructors

        // Default Constructor
        public EditSaleViewModel(INavigationService closeModalNavigationService, SaleStore saleStore)
        {
            _saleStore = saleStore;

            Departments = new ObservableCollection<DepartmantModel>()
            {
                new DepartmantModel(){ DepartmentName = "Mobile"},
                new DepartmantModel(){ DepartmentName = "Computer"},
                new DepartmantModel(){ DepartmentName = "Camera"},
                new DepartmantModel(){ DepartmentName = "Home"},
                new DepartmantModel(){ DepartmentName = "AV"},
                new DepartmantModel(){ DepartmentName = "Repair"}
            };
            
            // Commands
            CloseCommand = new CloseModalCommand(closeModalNavigationService);
            EditProductCommand = new EditProductCommand(this);
            CancelEditProductCoomand = new CancelEditProductCommand(this);

            LoadSale();
            LoadSoldProducts();
        }

        #endregion

        #region Methods 

        public void SetupProductToEdit()
        {
            int index = SaleProducts.IndexOf(SelectedProduct);

            EditName = SelectedProduct.ProductName;
            EditDescription = SelectedProduct.ProductDescription;
            EditCost = SelectedProduct.ProductCost;
            EditPrice = SelectedProduct.SalePrice;
            EditQuantity = SelectedProduct.QuantitySold;
        }

        public void CancelEditProduct()
        {
            EditName = null;
            EditDescription = null;
            EditCost = null;
            EditPrice = null;
            EditQuantity = 0;
        }

        public void UpdateSoldProduct()
        {
            int index = SaleProducts.IndexOf(SelectedProduct);

            SaleProducts[index].ProductName = ProductName;
            SaleProducts[index].ProductDescription = ProductDescription;
            SaleProducts[index].SalePrice = SalePrice;
            SaleProducts[index].ProductCost = ProductCost;
            SaleProducts[index].QuantitySold = QuantitySold;
            SaleProducts[index].Total = _currencyHelper.ConvertDecimalToCurrencyString(
                QuantitySold * _currencyHelper.ConvertCurrencyStringToDecimal(SalePrice));
        }

        private void CalculateProductTotal()
        {
            decimal quantity = Convert.ToDecimal(SelectedProduct.QuantitySold);
            decimal salePrice = _currencyHelper.ConvertCurrencyStringToDecimal(SelectedProduct.SalePrice);

            SelectedProduct.Total = _currencyHelper.ConvertDecimalToCurrencyString(quantity * salePrice);

            var test = SaleProducts.IndexOf(SelectedProduct);
        }

        private void LoadSale()
        {
            // insert data to all the fields
            _totalCost = _saleStore.SelectedSale.TotalCost;
            _totalProfit = CalculateProfit();
            _saleTotal = _saleStore.SelectedSale.SaleTotal;
            _card = _saleStore.SelectedSale.Card;
            _cash = _saleStore.SelectedSale.Cash;
            _credit = _saleStore.SelectedSale.Credit;
        }


        private string CalculateProfit()
        {
            decimal total = _currencyHelper.ConvertCurrencyStringToDecimal(_saleStore.SelectedSale.SaleTotal);
            decimal totalCost = _currencyHelper.ConvertCurrencyStringToDecimal(_saleStore.SelectedSale.TotalCost);
            return _currencyHelper.ConvertDecimalToCurrencyString(total - totalCost);
        }

        private void LoadSoldProducts()
        {
            List<SaleProductModel> products = _saleProductData.GetSaleProductBySaleId(Id);

            List<SaleProductDisplayModel> displayProducts = new List<SaleProductDisplayModel>();

            foreach (var item in products)
            {
                displayProducts.Add(new SaleProductDisplayModel
                {
                    Id = item.Id,
                    SaleId = item.SaleId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductDescription = item.ProductDescription,
                    ProductCost = _currencyHelper.ConvertIntToCurrencyString(item.ProductCost),
                    SalePrice = _currencyHelper.ConvertIntToCurrencyString(item.SalePrice),
                    QuantitySold = item.QuantitySold,
                    Total = _currencyHelper.ConvertIntToCurrencyString(item.Total),
                    Department = item.Department
                });

                SaleProducts = new BindingList<SaleProductDisplayModel>(displayProducts);
            }
        }
        #endregion
    }
}
