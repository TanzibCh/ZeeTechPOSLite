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
using System.Linq;
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
        private readonly BankingViewModel _bankingViewModel;

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

        private DepartmantModel _selectedDepartment;

        public DepartmantModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
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

        private int _editDepartment;

        public int EditDepartment
        {
            get { return _editDepartment; }
            set
            {
                _editDepartment = value;
                OnPropertyChanged(nameof(EditDepartment));
            }
        }

        #endregion

        #region Command Properties

        public ICommand CloseCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand CancelEditProductCoomand { get; }
        public ICommand UpdateEditProductCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand VoidSaleCommand { get; }

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
            UpdateEditProductCommand = new UpdateEditProductCommand(this);
            SaveChangesCommand = new SaveChangesCommand(this);
            VoidSaleCommand = new VoidSaleCommand(this);

            LoadSale();
            LoadSoldProducts();
        }

        #endregion

        #region Methods

        // Void selected Sale
        public void VoidSale()
        {
            _salesData.ChangeSaleActiveStatus(Id, _saleStore.SelectedSale.IsActive);
        }

        // Save canges made to the sale
        public void SaveChanges()
        {
            _salesData.UpdateSale(CreateSaleForUpdate(), CreateProductListForUpdate());
        }

        // Create a new list to products, which will be used to update the database
        private List<SaleProductModel> CreateProductListForUpdate()
        {
            List<SaleProductModel> saleProducts = new List<SaleProductModel>();

            foreach (SaleProductDisplayModel product in SaleProducts)
            {
                saleProducts.Add(new SaleProductModel
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Department = product.Department,
                    SalePrice = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(product.SalePrice)),
                    ProductCost = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(product.ProductCost)),
                    Total = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(product.Total)),
                    QuantitySold = product.QuantitySold
                });
            }

            return saleProducts;
        }

        private SaleModel CreateSaleForUpdate()
        {
            SaleModel sale = new SaleModel
            {
                Id = Id,
                Card = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(Card)),
                Cash = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(Cash)),
                Credit = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(Credit)),
                SaleTotal = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(SaleTotal)),
                Tax = _currencyHelper.ConvertDecimalToInt(_currencyHelper.CalculateTax(_currencyHelper.ConvertCurrencyStringToDecimal(SaleTotal))),
                TotalCost = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(TotalCost)),
                Profit = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertCurrencyStringToDecimal(TotalProfit))
            };

            return sale;
        }

        private DepartmantModel FindDepartment(string name)
        {
            return Departments.Where(x => x.DepartmentName == name).FirstOrDefault();
        }

        public void SetupProductToEdit()
        {
            EditName = SelectedProduct.ProductName;
            EditDescription = SelectedProduct.ProductDescription;
            EditCost = SelectedProduct.ProductCost;
            EditPrice = SelectedProduct.SalePrice;
            EditQuantity = SelectedProduct.QuantitySold;
            SelectedDepartment = FindDepartment(SelectedProduct.Department);
        }

        public void ClearEditProduct()
        {
            EditName = null;
            EditDescription = null;
            EditCost = null;
            EditPrice = null;
            EditQuantity = 0;
            SelectedDepartment = null;
        }

        public void UpdateSoldProduct()
        {
            

            SaleProducts.Add(new SaleProductDisplayModel
            {
                Id = SelectedProduct.Id,
                ProductId = SelectedProduct.ProductId,
                SaleId = SelectedProduct.SaleId,
                ProductName = EditName,
                ProductDescription = EditDescription,
                Department = SelectedDepartment.DepartmentName,
                SalePrice = EditPrice,
                ProductCost = EditCost,
                QuantitySold = EditQuantity,
                Total = _currencyHelper.ConvertDecimalToCurrencyString(
                EditQuantity * _currencyHelper.ConvertCurrencyStringToDecimal(EditPrice))
            });

            SaleProducts.Remove(SelectedProduct);
        }

        private void LoadSale()
        {
            // insert data to all the fields
            TotalCost = _saleStore.SelectedSale.TotalCost;
            SaleTotal = _saleStore.SelectedSale.SaleTotal;
            Card = _saleStore.SelectedSale.Card;
            Cash = _saleStore.SelectedSale.Cash;
            Credit = _saleStore.SelectedSale.Credit;
            TotalProfit = CalculateProfit();

        }

        public void CalculateSaleCurrencies()
        {
            TotalCost = _currencyHelper.ConvertDecimalToCurrencyString(CalculateSaleTotalCost());
            SaleTotal = _currencyHelper.ConvertDecimalToCurrencyString(CalculateSaleTotal());
            TotalProfit = CalculateProfit();
        }

        private decimal CalculateSaleTotal()
        {
            decimal total = 0;

            foreach (SaleProductDisplayModel product in SaleProducts)
            {
                total += _currencyHelper.ConvertCurrencyStringToDecimal(product.Total);
            }

            return total;
        }

        private decimal CalculateSaleTotalCost()
        {
            decimal total = 0;

            foreach (var product in SaleProducts)
            {
                total += _currencyHelper.ConvertCurrencyStringToDecimal(product.ProductCost) * product.QuantitySold;
            }

            return total;
        }

        private string CalculateProfit()
        {
            decimal total = _currencyHelper.ConvertCurrencyStringToDecimal(SaleTotal);
            decimal totalCost = _currencyHelper.ConvertCurrencyStringToDecimal(TotalCost);
            return _currencyHelper.ConvertDecimalToCurrencyString(total - totalCost);
        }

        private void LoadSoldProducts()
        {
            List<SaleProductModel> products = _saleProductData.GetSaleProductBySaleId(Id);

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
                    ProductCost = _currencyHelper.ConvertIntToCurrencyString(item.ProductCost),
                    SalePrice = _currencyHelper.ConvertIntToCurrencyString(item.SalePrice),
                    QuantitySold = item.QuantitySold,
                    Total = _currencyHelper.ConvertIntToCurrencyString(item.Total),
                    Department = item.Department
                });

                SaleProducts = new ObservableCollection<SaleProductDisplayModel>(displayProducts);
            }
        }
        #endregion
    }
}
