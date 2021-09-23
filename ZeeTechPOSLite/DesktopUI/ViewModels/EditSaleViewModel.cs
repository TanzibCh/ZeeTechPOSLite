using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Commands;
using DesktopUI.Commands.EditSaleCommands;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class EditSaleViewModel : ViewModelBase
    {
        #region Private properties

        //private readonly BankingViewModel _bankingViewModel;
        private readonly SaleStore _saleStore;

        private CurrencyHelper _currencyHelper = new CurrencyHelper();
        private SaleProductData _saleProductData = new SaleProductData();
        private SalesData _salesData = new SalesData();

        #endregion Private properties

        #region Sales info Display properties

        private string _card;
        private string _cash;
        private string _credit;
        private string _saleTotal;
        private string _totalCost;
        private string _totalProfit;

        public string Card
        {
            get { return _card; }
            set
            {
                _card = value;
                OnPropertyChanged(nameof(Card));
            }
        }

        public string Cash
        {
            get { return _cash; }
            set
            {
                _cash = value;
                OnPropertyChanged(nameof(Cash));
            }
        }

        public string Credit
        {
            get { return _credit; }
            set
            {
                _credit = value;
                OnPropertyChanged(nameof(Credit));
            }
        }

        public int Id => _saleStore.SelectedSale.Id;

        public string InvoiceDate => _saleStore.SelectedSale.SaleDate;
        public int InvoiceNo => _saleStore.SelectedSale.InvoiceNo;
        public string InvoiceTime => _saleStore.SelectedSale.SaleTime;

        public string SaleTotal
        {
            get { return _saleTotal; }
            set
            {
                _saleTotal = value;
                OnPropertyChanged(nameof(SaleTotal));
            }
        }

        public string TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        public string TotalProfit
        {
            get { return _totalProfit; }
            set
            {
                _totalProfit = value;
                OnPropertyChanged(nameof(TotalProfit));
            }
        }

        #endregion Sales info Display properties

        #region Sale Products Display properties

        private ObservableCollection<SaleProductDisplayModel> _saleProducts;

        private SaleProductDisplayModel _selectedProduct;

        public ObservableCollection<SaleProductDisplayModel> SaleProducts
        {
            get { return _saleProducts; }
            set
            {
                _saleProducts = value;
                OnPropertyChanged(nameof(SaleProducts));
            }
        }

        public SaleProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        #endregion Sale Products Display properties

        #region Edit Product Properties

        private ObservableCollection<DepartmantModel> _departments;

        private string _editCost;

        private int _editDepartment;

        private string _editDescription;

        private string _editName;

        private string _editPrice;

        private int _editQuantity;

        private DepartmantModel _selectedDepartment;

        public ObservableCollection<DepartmantModel> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        public string EditCost
        {
            get { return _editCost; }
            set
            {
                _editCost = value;
                OnPropertyChanged(nameof(EditCost));
            }
        }

        public int EditDepartment
        {
            get { return _editDepartment; }
            set
            {
                _editDepartment = value;
                OnPropertyChanged(nameof(EditDepartment));
            }
        }

        public string EditDescription
        {
            get { return _editDescription; }
            set
            {
                _editDescription = value;
                OnPropertyChanged(nameof(EditDescription));
            }
        }

        public string EditName
        {
            get { return _editName; }
            set
            {
                _editName = value;
                OnPropertyChanged(nameof(EditName));
            }
        }

        public string EditPrice
        {
            get { return _editPrice; }
            set
            {
                _editPrice = value;
                OnPropertyChanged(nameof(EditPrice));
            }
        }

        public int EditQuantity
        {
            get { return _editQuantity; }
            set
            {
                _editQuantity = value;
                OnPropertyChanged(nameof(EditQuantity));
            }
        }

        public DepartmantModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }

        #endregion Edit Product Properties

        #region Command Properties

        public ICommand CancelEditProductCoomand { get; }
        public ICommand CloseCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand RefundCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand UpdateEditProductCommand { get; }
        public ICommand VoidSaleCommand { get; }

        #endregion Command Properties

        #region Constructors

        // Default Constructor
        public EditSaleViewModel(INavigationService closeNavigationService, INavigationService refundNavigationService, SaleStore saleStore)
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
            CloseCommand = new CloseModalCommand(closeNavigationService);
            EditProductCommand = new EditProductCommand(this);
            CancelEditProductCoomand = new CancelEditProductCommand(this);
            UpdateEditProductCommand = new UpdateEditProductCommand(this);
            SaveChangesCommand = new SaveChangesCommand(this);
            VoidSaleCommand = new VoidSaleCommand(this);
            RefundCommand = new RefundCommand(refundNavigationService);

            LoadSale();
            LoadSoldProducts();
        }

        #endregion Constructors

        #region Methods

        public void CalculateSaleCurrencies()
        {
            TotalCost = _currencyHelper.ConvertDecimalToString(CalculateSaleTotalCost());
            SaleTotal = _currencyHelper.ConvertDecimalToString(CalculateSaleTotal());
            TotalProfit = CalculateProfit();
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

        // Save canges made to the sale
        public void SaveChanges()
        {
            _salesData.UpdateSale(CreateSaleForUpdate(), CreateProductListForUpdate());
        }

        public void SetupProductToEdit()
        {
            if (SelectedProduct != null)
            {
                EditName = SelectedProduct.ProductName;
                EditDescription = SelectedProduct.ProductDescription;
                EditCost = SelectedProduct.ProductCost;
                EditPrice = SelectedProduct.SalePrice;
                EditQuantity = SelectedProduct.QuantitySold;
                SelectedDepartment = FindDepartment(SelectedProduct.Department);
            }
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
                Total = _currencyHelper.ConvertDecimalToString(
                EditQuantity * _currencyHelper.ConvertStringToDecimal(EditPrice))
            });

            SaleProducts.Remove(SelectedProduct);
        }

        // Void selected Sale
        public void VoidSale()
        {
            _salesData.ChangeSaleActiveStatus(Id, _saleStore.SelectedSale.IsActive);
        }

        private string CalculateProfit()
        {
            decimal total = _currencyHelper.ConvertStringToDecimal(SaleTotal);
            decimal totalCost = _currencyHelper.ConvertStringToDecimal(TotalCost);
            return _currencyHelper.ConvertDecimalToString(total - totalCost);
        }

        private decimal CalculateSaleTotal()
        {
            decimal total = 0;

            foreach (SaleProductDisplayModel product in SaleProducts)
            {
                total += _currencyHelper.ConvertStringToDecimal(product.Total);
            }

            return total;
        }

        private decimal CalculateSaleTotalCost()
        {
            decimal total = 0;

            foreach (var product in SaleProducts)
            {
                total += _currencyHelper.ConvertStringToDecimal(product.ProductCost) * product.QuantitySold;
            }

            return total;
        }

        // Create a new list of products, which will be used to update the database
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
                    SalePrice = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(product.SalePrice)),
                    ProductCost = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(product.ProductCost)),
                    Total = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(product.Total)),
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
                Card = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(Card)),
                Cash = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(Cash)),
                Credit = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(Credit)),
                SaleTotal = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(SaleTotal)),
                Tax = _currencyHelper.ConvertDecimalToInt(_currencyHelper.CalculateTax(_currencyHelper.ConvertStringToDecimal(SaleTotal))),
                TotalCost = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(TotalCost)),
                Profit = _currencyHelper.ConvertDecimalToInt(_currencyHelper.ConvertStringToDecimal(TotalProfit))
            };

            return sale;
        }

        private DepartmantModel FindDepartment(string name)
        {
            return Departments.Where(x => x.DepartmentName == name).FirstOrDefault();
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

        #endregion Methods
    }
}